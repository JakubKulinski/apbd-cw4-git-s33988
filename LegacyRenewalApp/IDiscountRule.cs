namespace LegacyRenewalApp;

public interface IDiscountRule
{
    void Apply(DiscountContext context);
}