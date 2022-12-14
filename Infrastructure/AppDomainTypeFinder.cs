using AspNetCore.Container;

namespace AspNetCore.Container.Infrastructure;

/// <summary>
/// A class that finds types needed by Nop by looping assemblies in the 
/// currently executing AppDomain. Only assemblies whose names matches
/// certain patterns are investigated and an optional list of assemblies
/// referenced by <see cref="AssemblyNames"/> are always investigated.
/// </summary>
public partial class AppDomainTypeFinder : ITypeFinder
{
    #region Fields

    private readonly bool _ignoreReflectionErrors = true;
    /// <summary>
    /// 
    /// </summary>
    protected IContainerFileProvider _fileProvider;

    #endregion

    #region Ctor
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileProvider"></param>
    public AppDomainTypeFinder(IContainerFileProvider? fileProvider = null)
    {
        _fileProvider = (fileProvider ?? AspNetCoreContainerHelper.DefaultFileProvider) ?? throw new ArgumentNullException(nameof(fileProvider));
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Iterates all assemblies in the AppDomain and if it's name matches the configured patterns add it to our list.
    /// </summary>
    /// <param name="addedAssemblyNames"></param>
    /// <param name="assemblies"></param>
    private void AddAssembliesInAppDomain(List<string> addedAssemblyNames, List<Assembly> assemblies)
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            ArgumentNullException.ThrowIfNull(assembly.FullName);

            if (!Matches(assembly.FullName))
                continue;
            if (addedAssemblyNames.Contains(assembly.FullName))
                continue;

            assemblies.Add(assembly);
            addedAssemblyNames.Add(assembly.FullName);
        }
    }

    /// <summary>
    /// Adds specifically configured assemblies.
    /// </summary>
    /// <param name="addedAssemblyNames"></param>
    /// <param name="assemblies"></param>
    protected virtual void AddConfiguredAssemblies(List<string> addedAssemblyNames, List<Assembly> assemblies)
    {
        foreach (string assemblyName in AssemblyNames)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            ArgumentNullException.ThrowIfNull(assembly.FullName);
            if (addedAssemblyNames.Contains(assembly.FullName))
                continue;

            assemblies.Add(assembly);
            addedAssemblyNames.Add(assembly.FullName);
        }
    }

    /// <summary>
    /// Check if a dll is one of the shipped dlls that we know don't need to be investigated.
    /// </summary>
    /// <param name="assemblyFullName">
    /// The name of the assembly to check.
    /// </param>
    /// <returns>
    /// True if the assembly should be loaded into Nop.
    /// </returns>
    public virtual bool Matches(string assemblyFullName) => !Matches(assemblyFullName, AssemblySkipLoadingPattern)
               && Matches(assemblyFullName, AssemblyRestrictToLoadingPattern);

    /// <summary>
    /// Check if a dll is one of the shipped dlls that we know don't need to be investigated.
    /// </summary>
    /// <param name="assemblyFullName">
    /// The assembly name to match.
    /// </param>
    /// <param name="pattern">
    /// The regular expression pattern to match against the assembly name.
    /// </param>
    /// <returns>
    /// True if the pattern matches the assembly name.
    /// </returns>
    protected virtual bool Matches(string assemblyFullName, string pattern) => Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Makes sure matching assemblies in the supplied folder are loaded in the app domain.
    /// </summary>
    /// <param name="directoryPath">
    /// The physical path to a directory containing dlls to load in the app domain.
    /// </param>
    protected virtual void LoadMatchingAssemblies(string directoryPath)
    {
        List<string> loadedAssemblyNames = new();

        foreach (Assembly a in GetAssemblies())
        {
            ArgumentNullException.ThrowIfNull(a.FullName);
            loadedAssemblyNames.Add(a.FullName);
        }

        if (!_fileProvider.DirectoryExists(directoryPath))
            return;

        foreach (string dllPath in _fileProvider.GetFiles(directoryPath, "*.dll"))
            try
            {
                AssemblyName an = AssemblyName.GetAssemblyName(dllPath);
                if (Matches(an.FullName) && !loadedAssemblyNames.Contains(an.FullName))
                    _ = App.Load(an);

                //old loading stuff
                //Assembly a = Assembly.ReflectionOnlyLoadFrom(dllPath);
                //if (Matches(a.FullName) && !loadedAssemblyNames.Contains(a.FullName))
                //{
                //    App.Load(a.FullName);
                //}
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
    }

    /// <summary>
    /// Does type implement generic?
    /// </summary>
    /// <param name="type"></param>
    /// <param name="openGeneric"></param>
    /// <returns></returns>
    protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
    {
        try
        {
            Type genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
            foreach (Type implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
            {
                if (!implementedInterface.IsGenericType)
                    continue;

                if (genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition()))
                    return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Find classes of type
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
    /// <returns>Result</returns>
    public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true) => FindClassesOfType(typeof(T), onlyConcreteClasses);

    /// <summary>
    /// Find classes of type
    /// </summary>
    /// <param name="assignTypeFrom">Assign type from</param>
    /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
    /// <returns>Result</returns>
    /// <returns></returns>
    public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true) => FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);

    /// <summary>
    /// Find classes of type
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="assemblies">Assemblies</param>
    /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
    /// <returns>Result</returns>
    public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true) => FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);

    /// <summary>
    /// Find classes of type
    /// </summary>
    /// <param name="assignTypeFrom">Assign type from</param>
    /// <param name="assemblies">Assemblies</param>
    /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
    /// <returns>Result</returns>
    public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
    {
        List<Type> result = new();
        try
        {
            foreach (Assembly a in assemblies)
            {
                Type[]? types = null;
                try
                {
                    types = a.GetTypes();
                }
                catch
                {
                    //Entity Framework 6 doesn't allow getting types (throws an exception)
                    if (!_ignoreReflectionErrors)
                        throw;
                }

                if (types == null)
                    continue;

                foreach (Type t in types)
                {
                    if (!assignTypeFrom.IsAssignableFrom(t) && (!assignTypeFrom.IsGenericTypeDefinition || !DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                        continue;

                    if (t.IsInterface)
                        continue;

                    if (onlyConcreteClasses)
                        if (t.IsClass && !t.IsAbstract)
                            result.Add(t);
                    else
                        result.Add(t);
                }
            }
        }
        catch (ReflectionTypeLoadException ex)
        {
            string msg = string.Empty;
            foreach (Exception? e in ex.LoaderExceptions)
                msg += e?.Message + Environment.NewLine;

            Exception fail = new(msg, ex);
            Debug.WriteLine(fail.Message, fail);

            throw fail;
        }

        return result;
    }

    /// <summary>
    /// Gets the assemblies related to the current implementation.
    /// </summary>
    /// <returns>A list of assemblies</returns>
    public virtual IList<Assembly> GetAssemblies()
    {
        List<string> addedAssemblyNames = new();
        List<Assembly> assemblies = new();

        if (LoadAppDomainAssemblies)
            AddAssembliesInAppDomain(addedAssemblyNames, assemblies);

        AddConfiguredAssemblies(addedAssemblyNames, assemblies);

        return assemblies;
    }

    #endregion

    #region Properties

    /// <summary>The app domain to look for types in.</summary>
    public virtual AppDomain App => AppDomain.CurrentDomain;

    /// <summary>Gets or sets whether Nop should iterate assemblies in the app domain when loading Nop types. Loading patterns are applied when loading these assemblies.</summary>
    public bool LoadAppDomainAssemblies { get; set; } = true;

    /// <summary>Gets or sets assemblies loaded a startup in addition to those loaded in the AppDomain.</summary>
    public IList<string> AssemblyNames { get; set; } = new List<string>();

    /// <summary>Gets the pattern for dlls that we know don't need to be investigated.</summary>
    public string AssemblySkipLoadingPattern { get; set; } = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";

    /// <summary>Gets or sets the pattern for dll that will be investigated. For ease of use this defaults to match all but to increase performance you might want to configure a pattern that includes assemblies and your own.</summary>
    /// <remarks>If you change this so that Nop assemblies aren't investigated (e.g. by not including something like "^Nop|..." you may break core functionality.</remarks>
    public string AssemblyRestrictToLoadingPattern { get; set; } = ".*";

    #endregion
}
