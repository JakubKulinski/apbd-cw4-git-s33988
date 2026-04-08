namespace LegacyRenewalApp;

public interface IPaymentFeeCalculator
{
    decimal Calculate(string normalizedPaymentMethod, decimal amountBase);
    string GetPaymentNote(string normalizedPaymentMethod);
}