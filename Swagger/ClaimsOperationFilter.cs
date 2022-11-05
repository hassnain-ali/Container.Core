using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Container.Core.Swagger;

/// <summary>
/// Adds claims from any authorization policy's Microsoft.AspNetCore.Authorization.Infrastructure.ClaimsAuthorizationRequirement's.
/// </summary>
public class ClaimsOperationFilter : IOperationFilter
{
    //private const string OAuth2OpenApiReferenceId = "oauth2";

    private static readonly OpenApiSecurityScheme OAuth2OpenApiSecurityScheme = new()
    {
        Reference = new OpenApiReference
        {
            Id = "oauth2",
            Type = ReferenceType.SecurityScheme
        }
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation, nameof(operation));
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        List<string> list = (from x in context.ApiDescription.ActionDescriptor.FilterDescriptors.GetPolicyRequirements().OfType<ClaimsAuthorizationRequirement>()
                             select x.ClaimType).ToList();
        if (list.Any())
        {
            operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            OAuth2OpenApiSecurityScheme,
                            list
                        }
                    }
                };
        }
    }
}
