using System;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISubscriptionPlanRepository _planRepository;
        private readonly IInvoiceGateway _invoiceGateway;
        private readonly IRenewalRequestValidator _validator;
        private readonly IDiscountCalculator _discountCalculator;
        private readonly ISupportFeeCalculator _supportFeeCalculator;
        private readonly IPaymentFeeCalculator _paymentFeeCalculator;
        private readonly ITaxRateProvider _taxRateProvider;
        private readonly IRenewalInvoiceFactory _invoiceFactory;
        private readonly IRenewalEmailService _emailService;

        public SubscriptionRenewalService()
            : this(
                new CustomerRepository(),
                new SubscriptionPlanRepository(),
                new LegacyBillingGatewayAdapter(),
                new RenewalRequestValidator(),
                new DiscountCalculator(),
                new SupportFeeCalculator(),
                new PaymentFeeCalculator(),
                new TaxRateProvider(),
                new RenewalInvoiceFactory(),
                new RenewalEmailService())
        {
        }

        public SubscriptionRenewalService(
            ICustomerRepository customerRepository,
            ISubscriptionPlanRepository planRepository,
            IInvoiceGateway invoiceGateway,
            IRenewalRequestValidator validator,
            IDiscountCalculator discountCalculator,
            ISupportFeeCalculator supportFeeCalculator,
            IPaymentFeeCalculator paymentFeeCalculator,
            ITaxRateProvider taxRateProvider,
            IRenewalInvoiceFactory invoiceFactory,
            IRenewalEmailService emailService)
        {
            _customerRepository = customerRepository;
            _planRepository = planRepository;
            _invoiceGateway = invoiceGateway;
            _validator = validator;
            _discountCalculator = discountCalculator;
            _supportFeeCalculator = supportFeeCalculator;
            _paymentFeeCalculator = paymentFeeCalculator;
            _taxRateProvider = taxRateProvider;
            _invoiceFactory = invoiceFactory;
            _emailService = emailService;
        }

        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            var request = _validator.ValidateAndCreate(
                customerId,
                planCode,
                seatCount,
                paymentMethod,
                includePremiumSupport,
                useLoyaltyPoints);

            var customer = _customerRepository.GetById(request.CustomerId);
            var plan = _planRepository.GetByCode(request.PlanCode);

            if (!customer.IsActive)
            {
                throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
            }

            decimal baseAmount = (plan.MonthlyPricePerSeat * request.SeatCount * 12m) + plan.SetupFee;

            var discountResult = _discountCalculator.Calculate(
                customer,
                plan,
                baseAmount,
                request.SeatCount,
                request.UseLoyaltyPoints);

            decimal subtotalAfterDiscount = baseAmount - discountResult.Amount;
            if (subtotalAfterDiscount < 300m)
            {
                subtotalAfterDiscount = 300m;
                discountResult.AppendNote("minimum discounted subtotal applied;");
            }

            decimal supportFee = _supportFeeCalculator.Calculate(request.IncludePremiumSupport, request.PlanCode);

            if (request.IncludePremiumSupport)
            {
                discountResult.AppendNote("premium support included;");
            }

            decimal paymentBase = subtotalAfterDiscount + supportFee;
            decimal paymentFee = _paymentFeeCalculator.Calculate(request.PaymentMethod, paymentBase);

            discountResult.AppendNote(_paymentFeeCalculator.GetPaymentNote(request.PaymentMethod));

            decimal taxRate = _taxRateProvider.GetTaxRate(customer.Country);
            decimal taxBase = subtotalAfterDiscount + supportFee + paymentFee;
            decimal taxAmount = taxBase * taxRate;
            decimal finalAmount = taxBase + taxAmount;

            if (finalAmount < 500m)
            {
                finalAmount = 500m;
                discountResult.AppendNote("minimum invoice amount applied;");
            }

            var invoice = _invoiceFactory.Create(
                customer,
                request,
                baseAmount,
                discountResult.Amount,
                supportFee,
                paymentFee,
                taxAmount,
                finalAmount,
                discountResult.Notes);

            _invoiceGateway.SaveInvoice(invoice);

            _emailService.SendRenewalInvoiceEmail(customer, invoice, request.PlanCode, _invoiceGateway);

            return invoice;
        }
    }
}