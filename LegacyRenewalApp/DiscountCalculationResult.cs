namespace LegacyRenewalApp;

public class DiscountCalculationResult
{
    public decimal Amount { get; private set; }
    public string Notes { get; private set; }

    public DiscountCalculationResult()
    {
        Amount = 0m;
        Notes = string.Empty;
    }

    public void AddDiscount(decimal amount, string note)
    {
        Amount += amount;
        AppendNote(note);
    }

    public void AppendNote(string note)
    {
        Notes += note + " ";
    }
}