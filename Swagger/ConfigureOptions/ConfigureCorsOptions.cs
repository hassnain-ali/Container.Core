using Container.Core.Swagger.Constants;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace Container.Core.Swagger.ConfigureOptions;
/// <summary>
/// Configures cross-origin resource sharing (CORS) policies.
/// See https://docs.asp.net/en/latest/security/cors.html.
/// </summary>
public class ConfigureCorsOptions : IConfigureOptions<CorsOptions>
{
    /// <inheritdoc/>
    public void Configure(CorsOptions options) =>
        // Create named CORS policies here which you can consume using application.UseCors("PolicyName")
        // or a [EnableCors("PolicyName")] attribute on your controller or action.
        options.AddPolicy(
            CorsPolicyName.AllowAny,
            x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
}
