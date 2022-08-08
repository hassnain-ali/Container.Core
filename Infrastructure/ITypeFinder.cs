namespace Container.Core.Infrastructure;

public interface ITypeFinder
{
    AppDomain App { get; }
    IList<string> AssemblyNames { get; set; }
    string AssemblyRestrictToLoadingPattern { get; set; }
    string AssemblySkipLoadingPattern { get; set; }
    bool LoadAppDomainAssemblies { get; set; }

    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);
    IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    IList<Assembly> GetAssemblies();
    bool Matches(string assemblyFullName);
}
