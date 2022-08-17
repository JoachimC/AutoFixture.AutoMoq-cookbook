using System.Net;
using AutoFixture.Xunit2;
using RichardSzalay.MockHttp;

namespace AutoFixture.AutoMoq_cookbook;

public class WebTaxAuditorTests
{
    [Theory]
    [AutoMoqData]
    public async Task WHEN_AuditTaxCalculation_THEN_post_to_server(
        string orderNumber, 
        decimal orderSubtotal,
        decimal taxRate,
        decimal tax,
        [Frozen] MockHttpMessageHandler mockHttpMessageHandler,
        WebTaxAuditor sut)
    {
        mockHttpMessageHandler
            .When(HttpMethod.Post, "/")
            .Respond(HttpStatusCode.Created);
        
        await sut.AuditTaxCalculation(orderNumber, orderSubtotal, taxRate, tax);
        
        mockHttpMessageHandler.VerifyNoOutstandingExpectation();
    }

}