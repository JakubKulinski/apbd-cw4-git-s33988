namespace LegacyRenewalApp;

public class SegmentDiscountRule : IDiscountRule
{
    public void Apply(DiscountContext context)
    {
        if (context.Customer.Segment == "Silver")
        {
            context.Result.AddDiscount(context.BaseAmount * 0.05m, "silver discount;");
        }
        else if (context.Customer.Segment == "Gold")
        {
            context.Result.AddDiscount(context.BaseAmount * 0.10m, "gold discount;");
        }
        else if (context.Customer.Segment == "Platinum")
        {
            context.Result.AddDiscount(context.BaseAmount * 0.15m, "platinum discount;");
        }
        else if (context.Customer.Segment == "Education" && context.Plan.IsEducationEligible)
        {
            context.Result.AddDiscount(context.BaseAmount * 0.20m, "education discount;");
        }
    }
}