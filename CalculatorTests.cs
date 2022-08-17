using FluentAssertions;

namespace AutoFixture.AutoMoq_cookbook;

public class CalculatorTests
{
    [Fact]
    public void WHEN_3_multiply_5_THEN_0()
    {
        var sut = new Calculator();

        var result = sut.Multiply(3, 5);

        result.Should().Be(15);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(2, 2, 4)]
    [InlineData(3, 5, 15)]
    public void WHEN_x_multiply_y_THEN_z(byte x, byte y, int expectedProduct)
    {
        var sut = new Calculator();

        var product = sut.Multiply(x, y);

        product.Should().Be(expectedProduct);
    }
}