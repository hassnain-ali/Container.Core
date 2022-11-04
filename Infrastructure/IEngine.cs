namespace Container.Core.Infrastructure;

public interface IEngine
{
    IServiceProvider ServiceProvider { get; set; }
    void RegisterDependencies(ContainerBuilder containerBuilder, IServiceProvider provider);
    void RegisterDependencies(ContainerBuilder containerBuilder, IServiceCollection collection);
    object Resolve(Type type);
    T Resolve<T>() where T : class;
    IEnumerable<T> ResolveAll<T>();
    object ResolveUnregistered(Type type);
}
