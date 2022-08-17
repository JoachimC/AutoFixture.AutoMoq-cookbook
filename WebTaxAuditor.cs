using System.Net.Http.Json;

namespace AutoFixture.AutoMoq_cookbook;

public class WebTaxAuditor : ITaxAuditor
{
    private readonly HttpClient _httpClient;
    public WebTaxAuditor(HttpClient httpClient) => _httpClient = httpClient;


    public async Task AuditTaxCalculation(string orderNumber, decimal orderSubtotal, decimal taxRate, decimal tax)
    {
        var requestBody = new{ OrderNumber = orderNumber, TaxRate = taxRate, Tax = tax};
        
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/");
        httpRequestMessage.Content = JsonContent.Create(requestBody);
        
        await _httpClient.SendAsync(httpRequestMessage);
    }
}