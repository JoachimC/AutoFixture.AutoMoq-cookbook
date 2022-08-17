namespace AutoFixture.AutoMoq_cookbook;

public class ConsoleTaxAuditor : ITaxAuditor
{
    public Task AuditTaxCalculation(string orderNumber, decimal orderSubtotal, decimal taxRate, decimal tax)
    {
        Console.WriteLine($"{orderNumber}: rate:{taxRate} tax:{tax}");

        return Task.CompletedTask;
    }
}