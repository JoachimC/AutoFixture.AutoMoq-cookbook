using Microsoft.Extensions.Logging;

namespace AutoFixture.AutoMoq_cookbook;

public class LoggingTaxAuditor : ITaxAuditor
{
    private readonly ILogger _logger;
    public LoggingTaxAuditor(ILogger<LoggingTaxAuditor> logger) => _logger = logger;

    public Task AuditTaxCalculation(string orderNumber, decimal orderSubtotal, decimal taxRate, decimal tax)
    {
        _logger.LogInformation("order number: {OrderNumber} has tax of {Tax} at rate: {TaxRate}", orderNumber, tax, taxRate);
        
        return Task.CompletedTask;
    }
}