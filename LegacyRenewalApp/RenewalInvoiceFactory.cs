using System;

namespace LegacyRenewalApp;

public class RenewalInvoiceFactory :  IRenewalInvoiceFactory
{
    public RenewalInvoice Create(
        Customer customer,
        RenewalRequest request,
        decimal baseAmount,
        decimal discountAmount,
        decimal supportFee,
        decimal paymentFee,
        decimal taxAmount,
        decimal finalAmount,
        string notes)
    {
        var generatedAt = DateTime.UtcNow;

        return new RenewalInvoice
        {
            InvoiceNumber = $"INV-{generatedAt:yyyyMMdd}-{request.CustomerId}-{request.PlanCode}",
            CustomerName = customer.FullName,
            PlanCode = request.PlanCode,
            PaymentMethod = request.PaymentMethod,
            SeatCount = request.SeatCount,
            BaseAmount = Round(baseAmount),
            DiscountAmount = Round(discountAmount),
            SupportFee = Round(supportFee),
            PaymentFee = Round(paymentFee),
            TaxAmount = Round(taxAmount),
            FinalAmount = Round(finalAmount),
            Notes = notes.Trim(),
            GeneratedAt = generatedAt
        };
    }

    private static decimal Round(decimal value)
    {
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}