﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Container.Core.Swagger;

//
// Summary:
//     Adds a 401 Unauthorized response to the Swagger response documentation when the
//     authorization policy contains a Microsoft.AspNetCore.Authorization.Infrastructure.DenyAnonymousAuthorizationRequirement.
public class UnauthorizedResponseOperationFilter : IOperationFilter
{
    private static readonly OpenApiResponse UnauthorizedResponse = new()
    {
        Description = "Unauthorized - The user has not supplied the necessary credentials to access the resource."
    };

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation, nameof(operation));
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        IList<IAuthorizationRequirement> policyRequirements = context.ApiDescription.ActionDescriptor.FilterDescriptors.GetPolicyRequirements();
        if (!operation.Responses.ContainsKey("401") && policyRequirements.OfType<DenyAnonymousAuthorizationRequirement>().Any())
        {
            operation.Responses.Add("401", UnauthorizedResponse);
        }
    }
}
