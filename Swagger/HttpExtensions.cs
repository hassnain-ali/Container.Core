using Container.Core.Swagger.ConfigureOptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Container.Core.Swagger;

/// <summary>
/// 
/// </summary>
public static class HttpExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddVersionApiSwaggerAPI(this IServiceCollection services)
    {
        _ = services.AddCustomConfigureOptions();
        _ = services.AddDistributedMemoryCache();
        _ = services.AddResponseCaching();
        _ = services.AddHsts(o => { });
        _ = services.AddResponseCompression();
        _ = services.AddHealthChecks();
        _ = services.AddApiVersioning();
        _ = services.AddVersionedApiExplorer();
        return services;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    public static void UseVersionedSwaggerAPI(this WebApplication app)
    {
        _ = app.UseForwardedHeaders();
        _ = app.UseResponseCaching();
        _ = app.UseResponseCompression();
        _ = app.MapHealthChecks("/status");
        _ = app.MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false });
    }
    internal static IServiceCollection AddCustomConfigureOptions(this IServiceCollection services) =>
     services
         .ConfigureOptions<ConfigureApiVersioningOptions>()
         //.ConfigureOptions<ConfigureMvcOptions>()
         .ConfigureOptions<ConfigureCorsOptions>()
         .ConfigureOptions<ConfigureHstsOptions>()
         .ConfigureOptions<ConfigureJsonOptions>()
         //.ConfigureOptions<ConfigureRequestLoggingOptions>()
         //.ConfigureOptions<ConfigureResponseCompressionOptions>()
         .ConfigureOptions<ConfigureRouteOptions>()
         .ConfigureOptions<ConfigureSwaggerGenOptions>()
         .ConfigureOptions<ConfigureSwaggerUIOptions>();
    //.ConfigureOptions<ConfigureStaticFileOptions>();
}
