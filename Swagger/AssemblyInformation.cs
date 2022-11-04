namespace Container.Core.Swagger;

/// <summary>
/// 
/// </summary>
public record class AssemblyInformation
{
    /// <summary>
    /// 
    /// </summary>
    public string Product;
    /// <summary>
    /// 
    /// </summary>
    public string Description;
    /// <summary>
    /// 
    /// </summary>
    public string Version;
    /// <summary>
    /// 
    /// </summary>
    public static AssemblyInformation Current { get; private set; }// = new(typeof(AssemblyInformation).Assembly);
    /// <summary>
    /// 
    /// </summary>
    public bool EnableOauthSwagger { get; set; } = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static AssemblyInformation Set(Assembly assembly)
    {
        string p = assembly.GetCustomAttribute<AssemblyProductAttribute>()!.Product;
        string d = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()!.Description;
        string v = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()!.Version;
        Current = new()
        {
            Product = p,
            Description = d,
            Version = v
        };
        return Current;
    }
}