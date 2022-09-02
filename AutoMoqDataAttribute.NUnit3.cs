using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using MELT;
using Moq;
using RichardSzalay.MockHttp;

namespace AutoFixture.AutoMoq_cookbook;

public class AutoMoqDataAttributeNUnit3 : AutoDataAttribute
{
    public AutoMoqDataAttributeNUnit3() : base(FixtureFactory.Create) { }
}

public class InlineAutoMoqDataAttributeNUnit3 : InlineAutoDataAttribute
{
    public InlineAutoMoqDataAttributeNUnit3(params object[] objects) : base(FixtureFactory.Create, objects) { }
}

public static class FixtureFactory
{
    public static IFixture Create()
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
        fixture.Register(
            (HttpClient httpClient, Mock<IHttpClientFactory> httpClientFactoryMock) =>
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
    }
}