[![](https://img.shields.io/nuget/v/soenneker.resend.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.resend.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.resend.client/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.resend.client/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.resend.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.resend.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.resend.client/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.resend.client/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Resend.Client

Provides a cached `HttpClient` for sending and receiving email and managing Resend domains, contacts, audiences, broadcasts, templates, topics, and API keys.

## Installation

```bash
dotnet add package Soenneker.Resend.Client
```

## Configuration

```json
{
  "Resend": {
    "ApiKey": "re_xxxxxxxxx"
  }
}
```

## Usage

```csharp
using Soenneker.Resend.Client.Abstract;
using Soenneker.Resend.Client.Registrars;

services.AddResendHttpClientAsSingleton();

public sealed class ResendDomainReader
{
    private readonly IResendHttpClient _resend;

    public ResendDomainReader(IResendHttpClient resend)
    {
        _resend = resend;
    }

    public async Task<string> GetDomains(CancellationToken cancellationToken)
    {
        HttpClient client = await _resend.Get(cancellationToken);
        return await client.GetStringAsync("domains", cancellationToken);
    }
}
```

The provider sends `Authorization: Bearer <api-key>` and the `User-Agent` header required by Resend. It targets `https://api.resend.com/` by default. Set `Resend:ClientBaseUrl` for a proxy or compatible endpoint, or `Resend:UserAgent` when your application needs a specific user-agent value.
