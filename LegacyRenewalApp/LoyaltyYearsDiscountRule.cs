namespace LegacyRenewalApp;

public class LoyaltyYearsDiscountRule : IDiscountRule
{
    public void Apply(DiscountContext context)
    {
        if (context.Customer.YearsWithCompany >= 5)
        {
            context.Result.AddDiscount(context.BaseAmount * 0.07m, "long-term loyalty discount;");
        }
        else if (context.Customer.YearsWithCompany >= 2)
        {
            context.Result.AddDiscount(context.BaseAmount * 0.03m, "basic loyalty discount;");
        }
    }
}