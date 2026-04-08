namespace LegacyRenewalApp;

public class SeatCountDiscountRule : IDiscountRule
{
    public void Apply(DiscountContext context)
    {
        if (context.SeatCount >= 50)
        {
            context.Result.AddDiscount(context.BaseAmount * 0.12m, "large team discount;");
        }
        else if (context.SeatCount >= 20)
        {
            context.Result.AddDiscount(context.BaseAmount * 0.08m, "medium team discount;");
        }
        else if (context.SeatCount >= 10)
        {
            context.Result.AddDiscount(context.BaseAmount * 0.04m, "small team discount;");
        }
    }
}