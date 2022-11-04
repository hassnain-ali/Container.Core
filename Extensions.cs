using Container.Core.Caching;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Container.Core;

public static class Extensions
{
    public static IServiceCollection AddContainerCore(this IServiceCollection services)
    {
        _ = services.AddMemoryCache();
        services.TryAddSingleton<ICacheFactory, CacheFactory>();
        return services;
    }
}
