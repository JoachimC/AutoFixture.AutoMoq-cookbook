using AutoFixture.Xunit2;
using Moq;

namespace AutoFixture.AutoMoq_cookbook;

public class TaxCalculatorTests
{
    [Theory]
    [InlineAutoMoqData(0, 0)]
    [InlineAutoMoqData(0.5, 0.05)]
    [InlineAutoMoqData(1, 0.1)]
    [InlineAutoMoqData(50, 5)]
    public void WHEN_calculate_THEN_tax_is_10_percent(
        decimal orderSubtotal,
        decimal expectedTaxAmount,
        string orderNumber,
        [Frozen] Mock<ITaxAuditor> accountingSystemMock,
        FixedRateTaxCalculator sut)
    {
        sut.CalculateForOrder(orderNumber, orderSubtotal);

        accountingSystemMock
            .Verify(m => m.AuditTaxCalculation(orderNumber, orderSubtotal, 0.1m, expectedTaxAmount));
    }
}