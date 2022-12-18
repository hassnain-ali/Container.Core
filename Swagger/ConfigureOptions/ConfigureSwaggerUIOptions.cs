using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AspNetCore.Container.Swagger.ConfigureOptions;

/// <inheritdoc/>
public class ConfigureSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions>
{
    private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

    /// <inheritdoc/>
    public ConfigureSwaggerUIOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }

    /// <inheritdoc/>
    public void Configure(SwaggerUIOptions options)
    {
        // Set the Swagger UI browser document title.
        options.DocumentTitle = AssemblyInformation.Current?.Product;
        // Set the Swagger UI to render at '/'.
        options.RoutePrefix = string.Empty;

        options.DisplayOperationId();
        options.DisplayRequestDuration();

        foreach (ApiVersionDescription apiVersionDescription in apiVersionDescriptionProvider
            .ApiVersionDescriptions
            .OrderByDescending(x => x.ApiVersion))
            options.SwaggerEndpoint(
                $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                $"Version {apiVersionDescription.ApiVersion}");
    }
}
