using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Container.Core.Swagger.ConfigureOptions;

/// <summary>
/// 
/// </summary>
public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="configuration"></param>
    public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
    {
        this.provider = provider;
        _configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
        options.DescribeAllParametersInCamelCase();
        Microsoft.Extensions.DependencyInjection.AnnotationsSwaggerGenOptionsExtensions.EnableAnnotations(options);

        // Add the XML comment file for this assembly, so its contents can be displayed.
        //options.IncludeXmlCommentsIfExists(typeof(Program).Assembly);

        options.OperationFilter<ApiVersionOperationFilter>();
        options.OperationFilter<ClaimsOperationFilter>();
        options.OperationFilter<ForbiddenResponseOperationFilter>();
        options.OperationFilter<ProblemDetailsOperationFilter>();
        options.OperationFilter<UnauthorizedResponseOperationFilter>();

        // Show a default and example model for JsonPatchDocument<T>.
        options.SchemaFilter<JsonPatchDocumentSchemaFilter>();

        options.OperationFilter<AddRequiredHeaderParameter>();

        foreach (ApiVersionDescription apiVersionDescription in provider.ApiVersionDescriptions)
        {
            OpenApiInfo info = new()
            {
                Title = AssemblyInformation.Current.Product,
                Description = apiVersionDescription.IsDeprecated ?
                    $"{AssemblyInformation.Current.Description} This API version has been deprecated." :
                    AssemblyInformation.Current.Description,
                Version = apiVersionDescription.ApiVersion.ToString(),
            };
            options.SwaggerDoc(apiVersionDescription.GroupName, info);
        }

        //if (AssemblyInformation.Current.EnableOauthSwagger)
        //    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        //    {
        //        Type = SecuritySchemeType.OAuth2,
        //        Flows = new OpenApiOAuthFlows()
        //        {
        //            //AuthorizationCode = new OpenApiOAuthFlow()
        //            //{
        //            //    AuthorizationUrl = new Uri(_configuration["Auth:IdentityServer"] + "/connect/authorize"),
        //            //    TokenUrl = new Uri(_configuration["Auth:IdentityServer"] + "/connect/token"),
        //            //},
        //        }
        //    });
        //if (AssemblyInformation.Current.EnableOauthSwagger)
        //    options.AddSecurityRequirement(new()
        //         {
        //             {
        //                 new OpenApiSecurityScheme
        //                 {
        //                     Reference = new OpenApiReference
        //                     {
        //                         Type = ReferenceType.SecurityScheme,
        //                         Id = "oauth2"
        //                     },
        //                     Scheme = "oauth2",
        //                     Name = "oauth2",
        //                     In = ParameterLocation.Header
        //                 },
        //                 new List<string>()
        //             }
        //         });
    }
}
