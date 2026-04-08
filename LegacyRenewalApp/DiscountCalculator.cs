using System.Collections.Generic;

namespace LegacyRenewalApp;

public class DiscountCalculator : IDiscountCalculator
{
    private readonly IEnumerable<IDiscountRule> _rules;

    public DiscountCalculator()
        : this(new IDiscountRule[]
        {
            new SegmentDiscountRule(),
            new LoyaltyYearsDiscountRule(),
            new SeatCountDiscountRule(),
            new LoyaltyPointsDiscountRule()
        })
    {
    }

    public DiscountCalculator(IEnumerable<IDiscountRule> rules)
    {
        _rules = rules;
    }

    public DiscountCalculationResult Calculate(Customer customer, SubscriptionPlan plan, decimal baseAmount, int seatCount, bool useLoyaltyPoints)
    {
        var context = new DiscountContext(customer, plan, baseAmount, seatCount, useLoyaltyPoints);

        foreach (var rule in _rules)
        {
            rule.Apply(context);
        }

        return context.Result;
    }
}