namespace AutoFixture.AutoMoq_cookbook;

public class NoOpTaxAuditor : ITaxAuditor
{
    public Task AuditTaxCalculation(string orderNumber, decimal orderSubtotal, decimal taxRate, decimal tax) => Task.CompletedTask;
}