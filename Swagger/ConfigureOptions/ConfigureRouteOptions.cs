using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace Container.Core.Swagger.ConfigureOptions;
/// <summary>
/// Configures custom routing settings which determines how URL's are generated.
/// </summary>
public class ConfigureRouteOptions : IConfigureOptions<RouteOptions>
{
    /// <inheritdoc/>
    public void Configure(RouteOptions options) => options.LowercaseUrls = true;
}
