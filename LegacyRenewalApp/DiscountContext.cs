namespace LegacyRenewalApp;

public class DiscountContext
{
    public Customer Customer { get; }
    public SubscriptionPlan Plan { get; }
    public decimal BaseAmount { get; }
    public int SeatCount { get; }
    public bool UseLoyaltyPoints { get; }
    public DiscountCalculationResult Result { get; }

    public DiscountContext(Customer customer, SubscriptionPlan plan, decimal baseAmount, int seatCount, bool useLoyaltyPoints)
    {
        Customer = customer;
        Plan = plan;
        BaseAmount = baseAmount;
        SeatCount = seatCount;
        UseLoyaltyPoints = useLoyaltyPoints;
        Result = new DiscountCalculationResult();
    }
}