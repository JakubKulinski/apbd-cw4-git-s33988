namespace LegacyRenewalApp;

public class RenewalEmailService : IRenewalEmailService
{
    public void SendRenewalInvoiceEmail(Customer customer, RenewalInvoice invoice, string normalizedPlanCode, IInvoiceGateway gateway)
    {
        if (string.IsNullOrWhiteSpace(customer.Email))
        {
            return;
        }

        string subject = "Subscription renewal invoice";
        string body =
            $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
            $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

        gateway.SendEmail(customer.Email, subject, body);
    }
}