using System;

namespace LegacyRenewalApp;

public class PaymentFeeCalculator :  IPaymentFeeCalculator
{
    public decimal Calculate(string normalizedPaymentMethod, decimal amountBase)
    {
        if (normalizedPaymentMethod == "CARD")
        {
            return amountBase * 0.02m;
        }

        if (normalizedPaymentMethod == "BANK_TRANSFER")
        {
            return amountBase * 0.01m;
        }

        if (normalizedPaymentMethod == "PAYPAL")
        {
            return amountBase * 0.035m;
        }

        if (normalizedPaymentMethod == "INVOICE")
        {
            return 0m;
        }

        throw new ArgumentException("Unsupported payment method");
    }

    public string GetPaymentNote(string normalizedPaymentMethod)
    {
        if (normalizedPaymentMethod == "CARD")
        {
            return "card payment fee;";
        }

        if (normalizedPaymentMethod == "BANK_TRANSFER")
        {
            return "bank transfer fee;";
        }

        if (normalizedPaymentMethod == "PAYPAL")
        {
            return "paypal fee;";
        }

        if (normalizedPaymentMethod == "INVOICE")
        {
            return "invoice payment;";
        }

        throw new ArgumentException("Unsupported payment method");
    }
}