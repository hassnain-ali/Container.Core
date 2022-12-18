namespace AspNetCore.Container.Infrastructure;

/// <summary>
/// 
/// </summary>
public interface IEngine
{
    /// <summary>
    /// 
    /// </summary>
    IServiceProvider? ServiceProvider { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerBuilder"></param>
    /// <param name="provider"></param>
    void RegisterDependencies(ContainerBuilder containerBuilder, IServiceProvider provider);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerBuilder"></param>
    /// <param name="collection"></param>
    void RegisterDependencies(ContainerBuilder containerBuilder, IServiceCollection collection);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    object? Resolve(Type type);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T? Resolve<T>() where T : class;
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IEnumerable<T>? ResolveAll<T>();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    object? ResolveUnregistered(Type type);
}
