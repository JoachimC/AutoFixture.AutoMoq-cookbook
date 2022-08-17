namespace AutoFixture.AutoMoq_cookbook;

public class FixedRateTaxCalculator
{
    private const decimal TaxRate = 0.1m;

    private readonly ITaxAuditor _taxAuditor;
    public FixedRateTaxCalculator(ITaxAuditor taxAuditor) => _taxAuditor = taxAuditor;

    public decimal CalculateForOrder(string orderNumber, decimal orderSubtotal)
    {
        var taxAmount = orderSubtotal * TaxRate;
        _taxAuditor.AuditTaxCalculation(orderNumber, orderSubtotal, TaxRate, taxAmount);
        return taxAmount;
    }
}