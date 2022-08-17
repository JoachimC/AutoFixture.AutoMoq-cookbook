namespace AutoFixture.AutoMoq_cookbook;

public class ExternalRateTaxCalculator
{
    private readonly Func<string, decimal, decimal> _externalCalculator;
    public ExternalRateTaxCalculator(Func<string, decimal, decimal> externalCalculator) => _externalCalculator = externalCalculator;

    public decimal CalculateForOrder(string orderNumber, decimal orderSubtotal) => _externalCalculator(orderNumber, orderSubtotal);
}