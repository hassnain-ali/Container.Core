namespace AspNetCore.Container.Infrastructure;

/// <summary>
/// Represents Container engine
/// </summary>
public class Engine : IEngine
{
    #region Fields

    //private ITypeFinder _typeFinder;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets service provider
    /// </summary>
    public IServiceProvider? ServiceProvider { get; set; }

    #endregion

    #region Utilities

    /// <summary>
    /// Get IServiceProvider
    /// </summary>
    /// <returns>IServiceProvider</returns>
    protected IServiceProvider? GetServiceProvider()
    {
        if (ServiceProvider == null)
            return null;

        IHttpContextAccessor? accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
        HttpContext? context = accessor?.HttpContext;
        return context?.RequestServices ?? ServiceProvider;
    }

    /// <summary>
    /// Register dependencies
    /// </summary>
    /// <param name="containerBuilder">Container builder</param>
    /// <param name="provider">Nop configuration parameters</param>
    public virtual void RegisterDependencies(ContainerBuilder containerBuilder, IServiceProvider provider)
    {
        //register engine
        _ = containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();
        _ = containerBuilder.RegisterInstance(provider).AsSelf().SingleInstance();
        ServiceProvider = provider;
    }

    /// <summary>
    /// Register dependencies
    /// </summary>
    /// <param name="containerBuilder">Container builder</param>
    /// <param name="collection">Nop configuration parameters</param>
    public virtual void RegisterDependencies(ContainerBuilder containerBuilder, IServiceCollection collection)
    {
        //register engine
        _ = containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();
        ServiceProvider? provider = collection.BuildServiceProvider();
        _ = containerBuilder.RegisterInstance(provider).AsSelf().SingleInstance();
        ServiceProvider = provider;
    }

    #endregion

    #region Methods


    /// <summary>
    /// Resolve dependency
    /// </summary>
    /// <typeparam name="T">Type of resolved service</typeparam>
    /// <returns>Resolved service</returns>
    public T? Resolve<T>() where T : class => (T?)Resolve(typeof(T));

    /// <summary>
    /// Resolve dependency
    /// </summary>
    /// <param name="type">Type of resolved service</param>
    /// <returns>Resolved service</returns>
    public object? Resolve(Type type)
        => GetServiceProvider()?.GetService(type);


    /// <summary>
    /// Resolve dependencies
    /// </summary>
    /// <typeparam name="T">Type of resolved services</typeparam>
    /// <returns>Collection of resolved services</returns>
    public virtual IEnumerable<T>? ResolveAll<T>() => (IEnumerable<T>?)GetServiceProvider()?.GetServices(typeof(T));

    /// <summary>
    /// Resolve unregistered service
    /// </summary>
    /// <param name="type">Type of service</param>
    /// <returns>Resolved service</returns>
    public virtual object? ResolveUnregistered(Type type)
    {
        Exception? innerException = null;
        foreach (ConstructorInfo constructor in type.GetConstructors())
            try
            {
                //try to resolve constructor parameters
                IEnumerable<object> parameters = constructor.GetParameters().Select(parameter =>
                {
                    object? service = Resolve(parameter.ParameterType);
                    return service ?? throw new Exception("Unknown dependency");
                });

                //all is ok, so create instance
                return Activator.CreateInstance(type, parameters.ToArray());
            }
            catch (Exception ex)
            {
                innerException = ex;
            }

        throw new Exception("No constructor was found that had all the dependencies satisfied.", innerException);
    }

    #endregion

}
