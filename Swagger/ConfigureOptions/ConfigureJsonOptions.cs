using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Container.Core.Swagger.ConfigureOptions;

/// <summary>
/// 
/// </summary>
public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
{
    private readonly IWebHostEnvironment webHostEnvironment;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="webHostEnvironment"></param>
    public ConfigureJsonOptions(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public void Configure(JsonOptions options)
    {
        JsonSerializerOptions jsonSerializerOptions = options.JsonSerializerOptions;
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

        // Pretty print the JSON in development for easier debugging.
        jsonSerializerOptions.WriteIndented = webHostEnvironment.IsDevelopment() ||
            webHostEnvironment.IsEnvironment(Constants.EnvironmentName.Test);

        //jsonSerializerOptions.AddContext<CustomJsonSerializerContext>();
    }
}
