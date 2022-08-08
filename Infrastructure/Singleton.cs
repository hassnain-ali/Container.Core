﻿namespace Container.Core.Infrastructure;

public abstract class Singleton<T> : BaseSingleton
{
    private static T instance;

    /// <summary>
    /// The singleton instance for the specified type <typeparamref name="T"/>. Only one instance (at the time) of this object for each type of <typeparamref name="T"/>.
    /// </summary>
    public static T Instance
    {
        get => instance;
        private set
        {
            instance = value;
            AllSingletons[typeof(T)] = value;
        }
    }
    public static T Init(T val)
    {
        return Instance = val;
    }
}
/// <summary>
/// Provides access to all "singletons" stored by <see cref="Singleton{T}"/>.
/// </summary>
public abstract class BaseSingleton
{
    static BaseSingleton()
    {
        AllSingletons = new Dictionary<Type, object>();
    }

    /// <summary>
    /// Dictionary of type to singleton instances.
    /// </summary>
    public static IDictionary<Type, object> AllSingletons { get; }
}