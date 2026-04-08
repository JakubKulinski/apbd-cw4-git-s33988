namespace LegacyRenewalApp;

public interface IDiscountCalculator
{
    DiscountCalculationResult Calculate(Customer customer, SubscriptionPlan plan, decimal baseAmount, int seatCount, bool useLoyaltyPoints);
}