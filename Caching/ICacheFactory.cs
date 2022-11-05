namespace Container.Core.Caching;

/// <summary>
/// 
/// </summary>
public interface ICacheFactory
{
    /// <summary>
    /// Clear all cache data
    /// </summary>
    void Clear();
    /// <summary>
    /// Dispose cache manager
    /// </summary>
    void Dispose();
    /// <summary>
    /// Get a cached item. If it's not in the cache yet, then load and cache it
    /// </summary>
    /// <typeparam name="T">Type of cached item</typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="acquire">Function to load item if it's not in the cache yet</param>
    /// <returns>The cached value associated with the specified key</returns>
    T GetOrCreate<T>(CacheKey key, Func<T> acquire);
    /// <summary>
    /// Get a cached item. If it's not in the cache yet, then load and cache it
    /// </summary>
    /// <typeparam name="T">Type of cached item</typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="acquire">Function to load item if it's not in the cache yet</param>
    /// <returns>The cached value associated with the specified key</returns>
    Task<T> GetOrCreateAsync<T>(CacheKey key, Func<Task<T>> acquire);
    /// <summary>
    /// Gets a value indicating whether the value associated with the specified key is cached
    /// </summary>
    /// <param name="key">Key of cached item</param>
    /// <returns>True if item already is in cache; otherwise false</returns>
    bool IsSet(CacheKey key);
    /// <summary>
    /// Perform some action with exclusive in-memory lock
    /// </summary>
    /// <param name="key">The key we are locking on</param>
    /// <param name="expirationTime">The time after which the lock will automatically be expired</param>
    /// <param name="action">Action to be performed with locking</param>
    /// <returns>True if lock was acquired and action was performed; otherwise false</returns>
    bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action);
    /// <summary>
    /// Removes the value with the specified key from the cache
    /// </summary>
    /// <param name="key">Key of cached item</param>
    void Remove(CacheKey key);
    /// <summary>
    /// Removes the value with the specified key from the cache
    /// </summary>
    /// <param name="key">Key of cached item</param>
    void Remove(string key);
    /// <summary>
    /// Removes items by key prefix
    /// </summary>
    /// <param name="prefix">String key prefix</param>
    void RemoveByPrefix(string prefix);
    /// <summary>
    /// Adds the specified key and object to the cache
    /// </summary>
    /// <param name="key">Key of cached item</param>
    /// <param name="data">Value for caching</param>
    void Set(CacheKey key, object data);
}
