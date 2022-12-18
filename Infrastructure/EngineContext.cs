namespace AspNetCore.Container.Infrastructure;

/// <summary>
/// Provides access to the singleton instance of the Nop engine.
/// </summary>
public class EngineContext : Singleton<IEngine>
{
    #region Methods

    /// <summary>
    /// Create a static instance of the Nop engine.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static IEngine? Create() =>
        Instance ?? Init(new Engine());

    /// <summary>
    /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
    /// </summary>
    /// <param name="engine">The engine to use.</param>
    /// <remarks>Only use this method if you know what you're doing.</remarks>
    public static void Replace(IEngine engine) => Init(engine);

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton Nop engine used to access Nop services.
    /// </summary>
    public static IEngine? Current
    {
        get
        {
            if (Instance == null)
                _ = Create();

            return Instance;
        }
    }

    #endregion
}
