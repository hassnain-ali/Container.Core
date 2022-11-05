namespace Container.Core.Infrastructure;

/// <summary>
/// 
/// </summary>
public interface ITypeFinder
{
    /// <summary>
    /// 
    /// </summary>
    AppDomain App { get; }
    /// <summary>
    /// 
    /// </summary>
    IList<string> AssemblyNames { get; set; }
    /// <summary>
    /// 
    /// </summary>
    string AssemblyRestrictToLoadingPattern { get; set; }
    /// <summary>
    /// 
    /// </summary>
    string AssemblySkipLoadingPattern { get; set; }
    /// <summary>
    /// 
    /// </summary>
    bool LoadAppDomainAssemblies { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="assignTypeFrom"></param>
    /// <param name="onlyConcreteClasses"></param>
    /// <returns></returns>
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="assignTypeFrom"></param>
    /// <param name="assemblies"></param>
    /// <param name="onlyConcreteClasses"></param>
    /// <returns></returns>
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="onlyConcreteClasses"></param>
    /// <returns></returns>
    IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assemblies"></param>
    /// <param name="onlyConcreteClasses"></param>
    /// <returns></returns>
    IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IList<Assembly> GetAssemblies();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="assemblyFullName"></param>
    /// <returns></returns>
    bool Matches(string assemblyFullName);
}
