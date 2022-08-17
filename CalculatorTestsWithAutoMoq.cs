using FluentAssertions;

namespace AutoFixture.AutoMoq_cookbook;

public class CalculatorTestsWithAutoMoq
{
    [Theory]
    [AutoMoqData]
    public void WHEN_3_multiply_5_THEN_0(Calculator sut)
    {
        var result = sut.Multiply(3, 5);

        result.Should().Be(15);
    }

    [Theory]
    [InlineAutoMoqData(0, 0, 0)]
    [InlineAutoMoqData(0, 1, 0)]
    [InlineAutoMoqData(1, 1, 1)]
    [InlineAutoMoqData(2, 2, 4)]
    [InlineAutoMoqData(3, 5, 15)]
    public void WHEN_x_multiply_y_THEN_z(
        byte x,
        byte y,
        int expectedProduct,
        Calculator sut)
    {
        var result = sut.Multiply(x, y);

        result.Should().Be(expectedProduct);
    }

    [Theory]
    [AutoMoqData]
    public void WHEN_multiply_THEN_result_is_not_less_than_factors(byte x, byte y, Calculator sut)
    {
        var result = sut.Multiply(x, y);

        result.Should().BeGreaterOrEqualTo(x);
        result.Should().BeGreaterOrEqualTo(y);
    }
}