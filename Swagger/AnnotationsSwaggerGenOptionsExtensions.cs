using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Container.Core.Swagger;

public static class AnnotationsSwaggerGenOptionsExtensions
{
    //
    // Summary:
    //     Enables Swagger annotations (SwaggerOperationAttribute, SwaggerParameterAttribute
    //     etc.)
    //
    // Parameters:
    //   options:
    //
    //   enableAnnotationsForInheritance:
    //     Enables SwaggerSubType attribute for inheritance
    //
    //   enableAnnotationsForPolymorphism:
    //     Enables SwaggerSubType and SwaggerDiscriminator attributes for polymorphism
    public static void EnableAnnotations(this SwaggerGenOptions options, bool enableAnnotationsForInheritance, bool enableAnnotationsForPolymorphism)
    {
        options.SchemaFilter<AnnotationsSchemaFilter>(Array.Empty<object>());
        options.ParameterFilter<AnnotationsParameterFilter>(Array.Empty<object>());
        options.RequestBodyFilter<AnnotationsRequestBodyFilter>(Array.Empty<object>());
        options.OperationFilter<AnnotationsOperationFilter>(Array.Empty<object>());
        options.DocumentFilter<AnnotationsDocumentFilter>(Array.Empty<object>());
        if (enableAnnotationsForInheritance || enableAnnotationsForPolymorphism)
        {
            options.SelectSubTypesUsing(AnnotationsSubTypesSelector);
            options.SelectDiscriminatorNameUsing(AnnotationsDiscriminatorNameSelector);
            options.SelectDiscriminatorValueUsing(AnnotationsDiscriminatorValueSelector);
            if (enableAnnotationsForInheritance)
            {
                options.UseAllOfForInheritance();
            }

            if (enableAnnotationsForPolymorphism)
            {
                options.UseOneOfForPolymorphism();
            }
        }
    }

    //
    // Summary:
    //     Enables Swagger annotations (SwaggerOperationAttribute, SwaggerParameterAttribute
    //     etc.)
    //
    // Parameters:
    //   options:
    public static void EnableAnnotations(this SwaggerGenOptions options)
        => options.EnableAnnotations(enableAnnotationsForInheritance: false, enableAnnotationsForPolymorphism: false);

    private static IEnumerable<Type> AnnotationsSubTypesSelector(Type type)
    {
        IEnumerable<SwaggerSubTypeAttribute> source = type.GetCustomAttributes(inherit: false).OfType<SwaggerSubTypeAttribute>();
        if (source.Any())
        {
            return source.Select((SwaggerSubTypeAttribute attr) => attr.SubType);
        }

        SwaggerSubTypeAttribute swaggerSubTypesAttribute = type.GetCustomAttributes(inherit: false).OfType<SwaggerSubTypeAttribute>().FirstOrDefault();
        return swaggerSubTypesAttribute != null ? new List<Type>() { swaggerSubTypesAttribute.SubType } : Enumerable.Empty<Type>();
    }

    private static string AnnotationsDiscriminatorNameSelector(Type baseType)
    {
        SwaggerDiscriminatorAttribute swaggerDiscriminatorAttribute = baseType.GetCustomAttributes(inherit: false).OfType<SwaggerDiscriminatorAttribute>().FirstOrDefault();
        return swaggerDiscriminatorAttribute != null
            ? swaggerDiscriminatorAttribute.PropertyName
            : (baseType.GetCustomAttributes(inherit: false).OfType<SwaggerSubTypeAttribute>().FirstOrDefault()?.DiscriminatorValue);
    }

    private static string AnnotationsDiscriminatorValueSelector(Type subType) => subType.BaseType == null
            ? null
            : (subType.BaseType!.GetCustomAttributes(inherit: false).OfType<SwaggerSubTypeAttribute>().FirstOrDefault((SwaggerSubTypeAttribute attr) => attr.SubType == subType)?.DiscriminatorValue);
}
