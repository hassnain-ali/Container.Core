using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Container.Core.Swagger.ConfigureOptions;

/// <summary>
/// 
/// </summary>
public class ConfigureSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions>
{
    private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="apiVersionDescriptionProvider"></param>
    public ConfigureSwaggerUIOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerUIOptions options)
    {
        // Set the Swagger UI browser document title.
        options.DocumentTitle = AssemblyInformation.Current.Product;
        // Set the Swagger UI to render at '/'.
        options.RoutePrefix = string.Empty;

        options.DisplayOperationId();
        options.DisplayRequestDuration();

        foreach (ApiVersionDescription apiVersionDescription in apiVersionDescriptionProvider
            .ApiVersionDescriptions
            .OrderByDescending(x => x.ApiVersion))
        {
            options.SwaggerEndpoint(
                $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                $"Version {apiVersionDescription.ApiVersion}");
        }
    }
}
