using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Container.Core.Swagger;

///
/// <summary>
///     Adds a 403 Forbidden response to the Swagger response documentation when the
///     authorization policy contains a Microsoft.AspNetCore.Authorization.Infrastructure.ClaimsAuthorizationRequirement,
///     Microsoft.AspNetCore.Authorization.Infrastructure.NameAuthorizationRequirement,
///     Microsoft.AspNetCore.Authorization.Infrastructure.OperationAuthorizationRequirement,
///     Microsoft.AspNetCore.Authorization.Infrastructure.RolesAuthorizationRequirement
///     or Microsoft.AspNetCore.Authorization.Infrastructure.AssertionRequirement.
///     </summary>
public class ForbiddenResponseOperationFilter : IOperationFilter
{
    //private const string ForbiddenStatusCode = "403";

    private static readonly OpenApiResponse ForbiddenResponse = new()
    {
        Description = "Forbidden - The user does not have the necessary permissions to access the resource."
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
        IList<IAuthorizationRequirement> policyRequirements = context.ApiDescription.ActionDescriptor.FilterDescriptors.GetPolicyRequirements();
        if (!operation.Responses.ContainsKey("403") && HasAuthorizationRequirement(policyRequirements))
        {
            operation.Responses.Add("403", ForbiddenResponse);
        }
    }

    private static bool HasAuthorizationRequirement(IEnumerable<IAuthorizationRequirement> authorizationRequirements)
    {
        foreach (IAuthorizationRequirement authorizationRequirement in authorizationRequirements)
        {
            if (authorizationRequirement is ClaimsAuthorizationRequirement or NameAuthorizationRequirement or OperationAuthorizationRequirement or RolesAuthorizationRequirement or AssertionRequirement)
            {
                return true;
            }
        }

        return false;
    }
}
