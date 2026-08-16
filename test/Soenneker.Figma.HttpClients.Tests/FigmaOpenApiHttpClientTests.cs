using Soenneker.Figma.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Figma.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class FigmaOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IFigmaOpenApiHttpClient _httpclient;

    public FigmaOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IFigmaOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
