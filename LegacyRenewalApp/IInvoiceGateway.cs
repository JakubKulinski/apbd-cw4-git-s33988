namespace LegacyRenewalApp;

public interface IInvoiceGateway
{
    void SaveInvoice(RenewalInvoice invoice);
    void SendEmail(string to, string subject, string body);
}