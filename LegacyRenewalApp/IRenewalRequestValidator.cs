namespace LegacyRenewalApp;

public interface IRenewalRequestValidator
{
    RenewalRequest ValidateAndCreate(int customerId, string planCode, int seatCount, string paymentMethod, bool includePremiumSupport, bool useLoyaltyPoints);
}