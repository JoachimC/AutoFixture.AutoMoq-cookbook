using System.Net.Http.Json;

namespace AutoFixture.AutoMoq_cookbook;

public class WebTaxAuditorUsingHttpClientFactory : ITaxAuditor
{
    private readonly IHttpClientFactory _httpClientFactory;
    public WebTaxAuditorUsingHttpClientFactory(IHttpClientFactory httpClientFactoryFactory) => _httpClientFactory = httpClientFactoryFactory;


    public async Task AuditTaxCalculation(string orderNumber, decimal orderSubtotal, decimal taxRate, decimal tax)
    {
        var requestBody = new{ OrderNumber = orderNumber, TaxRate = taxRate, Tax = tax};
        
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/");
        httpRequestMessage.Content = JsonContent.Create(requestBody);

        using var httpClient = _httpClientFactory.CreateClient();
        await httpClient.SendAsync(httpRequestMessage);
    }
}