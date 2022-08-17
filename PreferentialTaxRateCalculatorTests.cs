using AutoFixture.Xunit2;
using Moq;

namespace AutoFixture.AutoMoq_cookbook;

public class PreferentialTaxRateCalculatorTests
{

    [Theory]
    [InlineAutoMoqData("123456", 10, 0.05, 0.5)]
    [InlineAutoMoqData("654321", 10, 0.1, 1)]
    public void GIVEN_preferential_rates_configured_WHEN_calculate_THEN_tax_is_5_percent(
        string orderNumber,
        decimal orderSubtotal,
        decimal expectedTaxRate,
        decimal expectedTaxAmount,
        [Frozen] Mock<ITaxAuditor> accountingSystemMock,
        PreferentialTaxRateCalculator sut)
    {
        sut.CalculateForOrder(orderNumber, orderSubtotal);
            
        accountingSystemMock
            .Verify(m => m.AuditTaxCalculation(orderNumber, orderSubtotal, expectedTaxRate, expectedTaxAmount));
    }
}