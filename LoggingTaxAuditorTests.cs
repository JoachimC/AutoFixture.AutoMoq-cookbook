using AutoFixture.Xunit2;
using MELT;
using Microsoft.Extensions.Logging;
using Moq;

namespace AutoFixture.AutoMoq_cookbook;

public class LoggingTaxAuditorTests
{
    [Theory]
    [AutoMoqData]
    public async Task WHEN_AuditTaxCalculation_THEN_log_as_information(
        string orderNumber,
        decimal orderSubtotal,
        decimal taxRate,
        decimal tax,
        [Frozen] ITestLoggerFactory loggerFactory,
        LoggingTaxAuditor sut)
    {
        await sut.AuditTaxCalculation(orderNumber, orderSubtotal, taxRate, tax);

        loggerFactory.VerifyMessageLogged(LogLevel.Information, times: Times.Once());
    }
}