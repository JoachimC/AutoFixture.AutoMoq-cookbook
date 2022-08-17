using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using MELT;
using Moq;
using RichardSzalay.MockHttp;

namespace AutoFixture.AutoMoq_cookbook;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute() : base(
        () =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new DateOnlySpecimenBuilder());
            fixture.Customizations.Add(new TimeOnlySpecimenBuilder());
            fixture.Customize(new AutoMoqCustomization { GenerateDelegates = true });
            fixture.Register(
                (MockHttpMessageHandler mockHttpHandler, Uri baseAddress) =>
                {
                    var httpClient = mockHttpHandler.ToHttpClient();
                    httpClient.BaseAddress = baseAddress;
                    return httpClient;
                });
            fixture.Register((HttpClient httpClient, Mock<IHttpClientFactory> httpClientFactoryMock) =>
            {
                httpClientFactoryMock
                    .Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(httpClient);
                 
                return httpClientFactoryMock.Object;
            });
            fixture.Register<IReadOnlyDictionary<string, decimal>>(
                () =>
                    new Dictionary<string, decimal>
                    {
                        { "123456", 0.05m }
                    });
            fixture.Register(TestLoggerFactory.Create);
            fixture.Customizations.Add(new LoggerSpecimenBuilder());
            return fixture;
        }) { }
}

public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects) { }
}