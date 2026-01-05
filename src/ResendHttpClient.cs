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

    private static readonly Uri _prodUri = new("https://api.resend.com", UriKind.Absolute);

    public ResendHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        // No closure: state passed explicitly + static lambda
        return _httpClientCache.Get(nameof(ResendHttpClient), (config: _config, prodUri: _prodUri), static state =>
        {
            var apiKey = state.config.GetValueStrict<string>("Resend:ApiKey");

            return new HttpClientOptions
            {
                BaseAddress = state.prodUri,
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {"Authorization", $"Bearer {apiKey}"}
                }
            };
        }, cancellationToken);
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