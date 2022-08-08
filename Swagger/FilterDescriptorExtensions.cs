using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Container.Core.Swagger;
//
// Summary:
//     System.Collections.Generic.IList`1 extension methods.
internal static class FilterDescriptorExtensions
{
    //
    // Summary:
    //     Gets the authorization policy requirements.
    //
    // Parameters:
    //   filterDescriptors:
    //     The filter descriptors.
    //
    // Returns:
    //     A collection of authorization policy requirements.
    public static IList<IAuthorizationRequirement> GetPolicyRequirements(this IList<Microsoft.AspNetCore.Mvc.Filters.FilterDescriptor> filterDescriptors)
    {
        ArgumentNullException.ThrowIfNull(filterDescriptors, nameof(filterDescriptors));
        List<IAuthorizationRequirement> list = new();
        for (int num = filterDescriptors.Count - 1; num >= 0; num--)
        {
            Microsoft.AspNetCore.Mvc.Filters.FilterDescriptor filterDescriptor = filterDescriptors[num];
            if (filterDescriptor.Filter is AllowAnonymousFilter)
            {
                break;
            }

            if (filterDescriptor.Filter is AuthorizeFilter authorizeFilter && authorizeFilter.Policy != null)
            {
                list.AddRange(authorizeFilter.Policy!.Requirements);
            }
        }

        return list;
    }
}