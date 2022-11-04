using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

namespace Container.Core.Swagger.ConfigureOptions;

/// <summary>
/// 
/// </summary>
public class ConfigureApiVersioningOptions :
    IConfigureOptions<ApiVersioningOptions>,
    IConfigureOptions<ApiExplorerOptions>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public void Configure(ApiVersioningOptions options)
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public void Configure(ApiExplorerOptions options) =>
        // Version format: 'v'major[.minor][-status]
        options.GroupNameFormat = "'v'VVV";
}
