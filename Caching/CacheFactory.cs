namespace Container.Core.Caching;

/// <summary>
/// 
/// </summary>
public class CacheFactory : IDisposable, ICacheFactory
{
    private bool _disposed;
    private readonly IMemoryCache _memoryCache;
    private static readonly ConcurrentDictionary<string, CancellationTokenSource> _prefixes = new();
    private static CancellationTokenSource _clearToken = new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="memoryCache"></param>
    public CacheFactory(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    #region Utilities

    /// <summary>
    /// Prepare cache entry options for the passed key
    /// </summary>a
    /// <param name="key">Cache key</param>
    /// <returns>Cache entry options</returns>
    private static MemoryCacheEntryOptions PrepareEntryOptions(CacheKey key)
    {
        //set expiration time for the passed cache key
        MemoryCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(key.CacheTime)
        };

        //add tokens to clear cache entries
        _ = options.AddExpirationToken(new CancellationChangeToken(_clearToken.Token));
        foreach (string keyPrefix in key.Prefixes.ToList())
        {
            CancellationTokenSource tokenSource = _prefixes.GetOrAdd(keyPrefix, value: new());
            _ = options.AddExpirationToken(new CancellationChangeToken(tokenSource.Token));
        }

        return options;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Get a cached item. If it's not in the cache yet, then load and cache it
    /// </summary>
    /// <typeparam name="T">Type of cached item</typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="acquire">Function to load item if it's not in the cache yet</param>
    /// <returns>The cached value associated with the specified key</returns>
    public T GetOrCreate<T>(CacheKey key, Func<T> acquire)
    {
        if (key.CacheTime <= 0)
        {
            return acquire();
        }

        T result = _memoryCache.GetOrCreate(key.Key, entry =>
        {
            _ = entry.SetOptions(PrepareEntryOptions(key));

            return acquire();
        });

        //do not cache null value
        if (result == null)
        {
            Remove(key);
        }

        return result;
    }

    /// <summary>
    /// Removes the value with the specified key from the cache
    /// </summary>
    /// <param name="key">Key of cached item</param>
    public void Remove(CacheKey key) => _memoryCache.Remove(key.Key);

    /// <summary>
    /// Get a cached item. If it's not in the cache yet, then load and cache it
    /// </summary>
    /// <typeparam name="T">Type of cached item</typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="acquire">Function to load item if it's not in the cache yet</param>
    /// <returns>The cached value associated with the specified key</returns>
    public async Task<T> GetOrCreateAsync<T>(CacheKey key, Func<Task<T>> acquire)
    {
        if (key.CacheTime <= 0)
        {
            return await acquire();
        }

        T result = await _memoryCache.GetOrCreateAsync(key.Key, async entry =>
        {
            _ = entry.SetOptions(PrepareEntryOptions(key));

            return await acquire();
        });
        //do not cache null value
        if (result is null || IsNullOrEmpty(result))//|| result is new())
        {
            Remove(key);
        }

        return result;
    }

    /// <summary>
    /// Adds the specified key and object to the cache
    /// </summary>
    /// <param name="key">Key of cached item</param>
    /// <param name="data">Value for caching</param>
    public void Set(CacheKey key, object data)
    {
        if (key.CacheTime <= 0 || data == null)
        {
            return;
        }

        _ = _memoryCache.Set(key.Key, data, PrepareEntryOptions(key));
    }

    /// <summary>
    /// Gets a value indicating whether the value associated with the specified key is cached
    /// </summary>
    /// <param name="key">Key of cached item</param>
    /// <returns>True if item already is in cache; otherwise false</returns>
    public bool IsSet(CacheKey key) => _memoryCache.TryGetValue(key.Key, out _);

    /// <summary>
    /// Perform some action with exclusive in-memory lock
    /// </summary>
    /// <param name="key">The key we are locking on</param>
    /// <param name="expirationTime">The time after which the lock will automatically be expired</param>
    /// <param name="action">Action to be performed with locking</param>
    /// <returns>True if lock was acquired and action was performed; otherwise false</returns>
    public bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action)
    {
        //ensure that lock is acquired
        if (IsSet(new(key)))
        {
            return false;
        }

        try
        {
            _ = _memoryCache.Set(key, key, expirationTime);

            //perform action
            action();

            return true;
        }
        finally
        {
            //release lock even if action fails
            Remove(key);
        }
    }

    /// <summary>
    /// Removes the value with the specified key from the cache
    /// </summary>
    /// <param name="key">Key of cached item</param>
    public void Remove(string key) => _memoryCache.Remove(key);

    /// <summary>
    /// Removes items by key prefix
    /// </summary>
    /// <param name="prefix">String key prefix</param>
    public void RemoveByPrefix(string prefix)
    {
        _ = _prefixes.TryRemove(prefix, out CancellationTokenSource tokenSource);
        tokenSource?.Cancel();
        tokenSource?.Dispose();
    }

    /// <summary>
    /// Clear all cache data
    /// </summary>
    public void Clear()
    {
        _clearToken.Cancel();
        _clearToken.Dispose();

        _clearToken = new();

        foreach (string prefix in _prefixes.Keys.ToList())
        {
            _ = _prefixes.TryRemove(prefix, out CancellationTokenSource tokenSource);
            tokenSource?.Dispose();
        }
    }

    private static bool IsNullOrEmpty<T>(T vals)
    {
        try
        {
            return EqualityComparer<T>.Default.Equals(vals, default);
        }
        catch (Exception)
        {

        }
        return false;
    }
    /// <summary>
    /// Dispose cache manager
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _memoryCache.Dispose();
        }

        _disposed = true;
    }

    #endregion
}
