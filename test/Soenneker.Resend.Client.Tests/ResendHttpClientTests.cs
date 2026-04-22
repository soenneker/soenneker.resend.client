using Soenneker.Resend.Client.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Resend.Client.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class ResendHttpClientTests : HostedUnitTest
{
    private readonly IResendHttpClient _httpclient;

    public ResendHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IResendHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
