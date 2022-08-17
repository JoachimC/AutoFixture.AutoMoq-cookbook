using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;

namespace AutoFixture.AutoMoq_cookbook;

public class ExternalRateTaxCalculatorTests
{
    [Theory]
    [AutoMoqData]
    public void WHEN_calculateForOrder_THEN_delegate_to_external_calculator(
        string orderNumber,
        decimal orderSubtotal,
        decimal expectedTaxAmount,
        [Frozen] Mock<Func<string, decimal, decimal>> externalCalculatorMock,
        ExternalRateTaxCalculator sut)
    {
        externalCalculatorMock
            .Setup(m => m(orderNumber, orderSubtotal))
            .Returns(expectedTaxAmount);

        var result = sut.CalculateForOrder(orderNumber, orderSubtotal);

        result.Should().Be(expectedTaxAmount);
    }
}