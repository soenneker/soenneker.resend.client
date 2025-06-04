using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Resend.Client.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Resend.Client.Registrars;

/// <summary>
/// A .NET thread-safe singleton HttpClient for GitHub
/// </summary>
public static class ResendHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="ResendHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddResendHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IResendHttpClient, ResendHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ResendHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddResendHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IResendHttpClient, ResendHttpClient>();

        return services;
    }
}
