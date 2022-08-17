namespace AutoFixture.AutoMoq_cookbook;

public class PreferentialTaxRateCalculator
{
    private const decimal DefaultTaxRate = 0.1m;
    private readonly IReadOnlyDictionary<string, decimal> _preferentialOrderRates;

    private readonly ITaxAuditor _taxAuditor;

    public PreferentialTaxRateCalculator(ITaxAuditor taxAuditor, IReadOnlyDictionary<string, decimal> preferentialOrderRatesTable)
    {
        _taxAuditor = taxAuditor;
        _preferentialOrderRates = preferentialOrderRatesTable;
    }

    public decimal CalculateForOrder(string orderNumber, decimal orderSubtotal)
    {
        var taxRate = _preferentialOrderRates.TryGetValue(orderNumber, out var preferentialRate) ? preferentialRate : DefaultTaxRate;
        var taxAmount = orderSubtotal * taxRate;
        _taxAuditor.AuditTaxCalculation(orderNumber, orderSubtotal, taxRate, taxAmount);
        return taxAmount;
    }
}