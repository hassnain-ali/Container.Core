using Container.Core.Caching;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Container.Core;

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
    public static IServiceCollection AddContainerCore(this IServiceCollection services)
    {
        _ = services.AddMemoryCache();
        _ = services.AddDistributedMemoryCache();
        services.TryAddSingleton<ICacheFactory, CacheFactory>();
        
        return services;
    }
}
