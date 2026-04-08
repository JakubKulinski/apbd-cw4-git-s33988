namespace LegacyRenewalApp;

public interface IRenewalEmailService
{
    void SendRenewalInvoiceEmail(Customer customer, RenewalInvoice invoice, string normalizedPlanCode, IInvoiceGateway gateway);
}