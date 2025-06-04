using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Resend.Client.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Resend.Client;

///<inheritdoc cref="IResendHttpClient"/>
public sealed class ResendHttpClient : IResendHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;

    private const string _prodUri = "https://api.resend.com";

    public ResendHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(ResendHttpClient), () =>
        {
            var apiKey = _config.GetValueStrict<string>("Resend:ApiKey");

            var options = new HttpClientOptions
            {
                BaseAddress = _prodUri,
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {"Authorization", $"Bearer {apiKey}"}
                }
            };
            return options;
        }, cancellationToken: cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(nameof(ResendHttpClient));
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(nameof(ResendHttpClient));
    }
}