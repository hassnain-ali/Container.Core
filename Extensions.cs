using AspNetCore.Container.Caching;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCore.Container;

/// <summary>
/// 
/// </summary>
public static class Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    [Obsolete("This method is now obsolete. Use AddAspNetCoreCacheFactory Instead.")]
    public static IServiceCollection AddContainerCore(this IServiceCollection services) => services.AddAspNetCoreCacheFactory();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAspNetCoreCacheFactory(this IServiceCollection services)
    {
        _ = services.AddMemoryCache();
        _ = services.AddDistributedMemoryCache();
        services.TryAddSingleton<ICacheFactory, CacheFactory>();

        return services;
    }
}
