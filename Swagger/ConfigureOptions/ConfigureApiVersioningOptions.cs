using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

namespace AspNetCore.Container.Swagger.ConfigureOptions;

/// <inheritdoc/>
public class ConfigureApiVersioningOptions :
    IConfigureOptions<ApiVersioningOptions>,
    IConfigureOptions<ApiExplorerOptions>
{
    /// <inheritdoc/>
    public void Configure(ApiVersioningOptions options)
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    }

    /// <inheritdoc/>
    public void Configure(ApiExplorerOptions options) =>
        // Version format: 'v'major[.minor][-status]
        options.GroupNameFormat = "'v'VVV";
}
