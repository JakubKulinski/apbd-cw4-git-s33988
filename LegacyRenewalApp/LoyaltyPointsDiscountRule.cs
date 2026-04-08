namespace LegacyRenewalApp;

public class LoyaltyPointsDiscountRule : IDiscountRule
{
    public void Apply(DiscountContext context)
    {
        if (context.UseLoyaltyPoints && context.Customer.LoyaltyPoints > 0)
        {
            int pointsToUse = context.Customer.LoyaltyPoints > 200 ? 200 : context.Customer.LoyaltyPoints;

            context.Result.AddDiscount(pointsToUse, $"loyalty points used: {pointsToUse};");
        }
    }
}