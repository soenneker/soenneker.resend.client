using Soenneker.Resend.Client.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Resend.Client.Tests;

[Collection("Collection")]
public sealed class ResendHttpClientTests : FixturedUnitTest
{
    private readonly IResendHttpClient _httpclient;

    public ResendHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IResendHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
