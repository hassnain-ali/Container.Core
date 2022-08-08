using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Container.Core.Swagger;

//
// Summary:
//     Shows an example of a Microsoft.AspNetCore.Mvc.ProblemDetails containing errors.
public class ProblemDetailsOperationFilter : IOperationFilter
{
    //private const string StatusCode400 = "400";

    //private const string StatusCode401 = "401";

    //private const string StatusCode403 = "403";

    //private const string StatusCode404 = "404";

    //private const string StatusCode406 = "406";

    //private const string StatusCode409 = "409";

    //private const string StatusCode415 = "415";

    //private const string StatusCode422 = "422";

    //private const string StatusCode500 = "500";

    //
    // Summary:
    //     Gets the 400 Bad Request example of a Microsoft.AspNetCore.Mvc.ProblemDetails
    //     response.
    public static OpenApiObject Status400ProblemDetails
    {
        get;
    } = new OpenApiObject
    {
        ["type"] = new OpenApiString("https://tools.ietf.org/html/rfc7231#section-6.5.1"),
        ["title"] = new OpenApiString("Bad Request"),
        ["status"] = new OpenApiInteger(400),
        ["traceId"] = new OpenApiString("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00"),
        ["errors"] = new OpenApiObject
        {
            ["exampleProperty1"] = new OpenApiArray
                {
                    new OpenApiString("The property field is required")
                }
        }
    };


    //
    // Summary:
    //     Gets the 401 Unauthorized example of a Microsoft.AspNetCore.Mvc.ProblemDetails
    //     response.
    public static OpenApiObject Status401ProblemDetails
    {
        get;
    } = new OpenApiObject
    {
        ["type"] = new OpenApiString("https://tools.ietf.org/html/rfc7235#section-3.1"),
        ["title"] = new OpenApiString("Unauthorized"),
        ["status"] = new OpenApiInteger(401),
        ["traceId"] = new OpenApiString("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00")
    };


    //
    // Summary:
    //     Gets the 403 Forbidden example of a Microsoft.AspNetCore.Mvc.ProblemDetails response.
    public static OpenApiObject Status403ProblemDetails
    {
        get;
    } = new OpenApiObject
    {
        ["type"] = new OpenApiString("https://tools.ietf.org/html/rfc7231#section-6.5.3"),
        ["title"] = new OpenApiString("Forbidden"),
        ["status"] = new OpenApiInteger(403),
        ["traceId"] = new OpenApiString("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00")
    };


    //
    // Summary:
    //     Gets the 404 Not Found example of a Microsoft.AspNetCore.Mvc.ProblemDetails response.
    public static OpenApiObject Status404ProblemDetails
    {
        get;
    } = new OpenApiObject
    {
        ["type"] = new OpenApiString("https://tools.ietf.org/html/rfc7231#section-6.5.4"),
        ["title"] = new OpenApiString("Not Found"),
        ["status"] = new OpenApiInteger(404),
        ["traceId"] = new OpenApiString("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00")
    };


    //
    // Summary:
    //     Gets the 406 Not Acceptable example of a Microsoft.AspNetCore.Mvc.ProblemDetails
    //     response.
    public static OpenApiObject Status406ProblemDetails
    {
        get;
    } = new OpenApiObject
    {
        ["type"] = new OpenApiString("https://tools.ietf.org/html/rfc7231#section-6.5.6"),
        ["title"] = new OpenApiString("Not Acceptable"),
        ["status"] = new OpenApiInteger(406),
        ["traceId"] = new OpenApiString("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00")
    };


    //
    // Summary:
    //     Gets the 409 Conflict example of a Microsoft.AspNetCore.Mvc.ProblemDetails response.
    public static OpenApiObject Status409ProblemDetails
    {
        get;
    } = new OpenApiObject
    {
        ["type"] = new OpenApiString("https://tools.ietf.org/html/rfc7231#section-6.5.8"),
        ["title"] = new OpenApiString("Conflict"),
        ["status"] = new OpenApiInteger(409),
        ["traceId"] = new OpenApiString("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00")
    };


    //
    // Summary:
    //     Gets the 415 Unsupported Media Type example of a Microsoft.AspNetCore.Mvc.ProblemDetails
    //     response.
    public static OpenApiObject Status415ProblemDetails
    {
        get;
    } = new OpenApiObject
    {
        ["type"] = new OpenApiString("https://tools.ietf.org/html/rfc7231#section-6.5.13"),
        ["title"] = new OpenApiString("Unsupported Media Type"),
        ["status"] = new OpenApiInteger(415),
        ["traceId"] = new OpenApiString("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00")
    };


    //
    // Summary:
    //     Gets the 422 Unprocessable Entity example of a Microsoft.AspNetCore.Mvc.ProblemDetails
    //     response.
    public static OpenApiObject Status422ProblemDetails
    {
        get;
    } = new OpenApiObject
    {
        ["type"] = new OpenApiString("https://tools.ietf.org/html/rfc4918#section-11.2"),
        ["title"] = new OpenApiString("Unprocessable Entity"),
        ["status"] = new OpenApiInteger(422),
        ["traceId"] = new OpenApiString("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00")
    };


    //
    // Summary:
    //     Gets the 500 Internal Server Error example of a Microsoft.AspNetCore.Mvc.ProblemDetails
    //     response.
    public static OpenApiObject Status500ProblemDetails
    {
        get;
    } = new OpenApiObject
    {
        ["type"] = new OpenApiString("https://tools.ietf.org/html/rfc7231#section-6.6.1"),
        ["title"] = new OpenApiString("Internal Server Error"),
        ["status"] = new OpenApiInteger(500),
        ["traceId"] = new OpenApiString("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00")
    };


    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation, nameof(operation));
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        foreach (KeyValuePair<string, OpenApiResponse> response in operation.Responses)
        {
            switch (response.Key)
            {
                case "400":
                    SetDefaultAndExample(response.Value, Status400ProblemDetails);
                    break;
                case "401":
                    SetDefaultAndExample(response.Value, Status401ProblemDetails);
                    break;
                case "403":
                    SetDefaultAndExample(response.Value, Status403ProblemDetails);
                    break;
                case "404":
                    SetDefaultAndExample(response.Value, Status404ProblemDetails);
                    break;
                case "406":
                    SetDefaultAndExample(response.Value, Status406ProblemDetails);
                    break;
                case "409":
                    SetDefaultAndExample(response.Value, Status409ProblemDetails);
                    break;
                case "415":
                    SetDefaultAndExample(response.Value, Status415ProblemDetails);
                    break;
                case "422":
                    SetDefaultAndExample(response.Value, Status422ProblemDetails);
                    break;
                case "500":
                    SetDefaultAndExample(response.Value, Status500ProblemDetails);
                    break;
            }
        }
    }

    private static void SetDefaultAndExample(OpenApiResponse value, OpenApiObject status500ProblemDetails)
    {
        if (value.Content != null)
        {
            if (value.Content.TryGetValue("application/problem+json", out OpenApiMediaType value2))
            {
                value2.Example = status500ProblemDetails;
            }

            if (value.Content.TryGetValue("application/problem+xml", out OpenApiMediaType value3))
            {
                value3.Example = status500ProblemDetails;
            }
        }
    }
}
