namespace AutoFixture.AutoMoq_cookbook;

public interface ITaxAuditor
{
    Task AuditTaxCalculation(string orderNumber, decimal orderSubtotal, decimal taxRate, decimal tax);
}