using Microsoft.AspNetCore.JsonPatch;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Container.Core.Swagger;

///
/// <summary>
///     Shows an example of a Microsoft.AspNetCore.JsonPatch.JsonPatchDocument containing
///     all the different patch operations you can do and a link to http://jsonpatch.com
///     for convenience.
///     </summary>
public class JsonPatchDocumentSchemaFilter : ISchemaFilter
{
    private static readonly OpenApiArray Example = new()
    {
        new OpenApiObject
        {
            ["op"] = new OpenApiString("replace"),
            ["path"] = new OpenApiString("/property"),
            ["value"] = new OpenApiString("New Value")
        },
        new OpenApiObject
        {
            ["op"] = new OpenApiString("add"),
            ["path"] = new OpenApiString("/property"),
            ["value"] = new OpenApiString("New Value")
        },
        new OpenApiObject
        {
            ["op"] = new OpenApiString("remove"),
            ["path"] = new OpenApiString("/property")
        },
        new OpenApiObject
        {
            ["op"] = new OpenApiString("copy"),
            ["from"] = new OpenApiString("/fromProperty"),
            ["path"] = new OpenApiString("/property")
        },
        new OpenApiObject
        {
            ["op"] = new OpenApiString("move"),
            ["from"] = new OpenApiString("/fromProperty"),
            ["path"] = new OpenApiString("/property")
        },
        new OpenApiObject
        {
            ["op"] = new OpenApiString("test"),
            ["path"] = new OpenApiString("/property"),
            ["value"] = new OpenApiString("Has Value")
        },
        new OpenApiObject
        {
            ["op"] = new OpenApiString("test"),
            ["path"] = new OpenApiString("/property"),
            ["value"] = new OpenApiString("Has Value")
        },
        new OpenApiObject
        {
            ["op"] = new OpenApiString("replace"),
            ["path"] = new OpenApiString("/arrayProperty/0"),
            ["value"] = new OpenApiString("Replace First Array Item")
        },
        new OpenApiObject
        {
            ["op"] = new OpenApiString("replace"),
            ["path"] = new OpenApiString("/arrayProperty/-"),
            ["value"] = new OpenApiString("Replace Last Array Item")
        }
    };

    private static readonly OpenApiExternalDocs ExternalDocs = new()
    {
        Description = "JSON Patch Documentation",
        Url = new Uri("http://jsonpatch.com/", UriKind.Absolute)
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(schema, nameof(schema));
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        if (context.Type.GenericTypeArguments.Length != 0 && context.Type.GetGenericTypeDefinition() == typeof(JsonPatchDocument<>))
        {
            schema.Default = Example;
            schema.Example = Example;
            schema.ExternalDocs = ExternalDocs;
        }
    }
}
