namespace AspNetCore.Container.Caching;

/// <inheritdoc/>
public class CacheFactory : IDisposable, ICacheFactory
{
    private bool _disposed;
    private readonly IMemoryCache _memoryCache;
    private static readonly ConcurrentDictionary<string, CancellationTokenSource> _prefixes = new();
    private static CancellationTokenSource _clearToken = new();

    /// <inheritdoc/>
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


    /// <inheritdoc/>
    public T? GetOrCreate<T>(CacheKey key, Func<T> acquire)
    {
        if (key.CacheTime <= 0)
            return acquire();
        ArgumentNullException.ThrowIfNull(key.Key);
        var result = _memoryCache.GetOrCreate(key.Key ?? "", entry =>
        {
            _ = entry.SetOptions(PrepareEntryOptions(key));

            return acquire();
        });

        //do not cache null value
        if (result == null)
            Remove(key);

        return result;
    }


    /// <inheritdoc/>
    public void Remove(CacheKey key) => _memoryCache.Remove(key.Key ?? string.Empty);


    /// <inheritdoc/>
    public async Task<T?> GetOrCreateAsync<T>(CacheKey key, Func<Task<T>> acquire)
    {
        if (key.CacheTime <= 0)
            return await acquire();

        T? result = await _memoryCache.GetOrCreateAsync(key.Key ?? string.Empty, async entry =>
        {
            _ = entry.SetOptions(PrepareEntryOptions(key));

            return await acquire();
        });
        //do not cache null value
        if (result is null || IsNullOrEmpty(result))//|| result is new())
            Remove(key);

        return result;
    }


    /// <inheritdoc/>
    public void Set(CacheKey key, object data)
    {
        if (key.CacheTime <= 0 || data == null)
            return;

        _ = _memoryCache.Set(key.Key ?? string.Empty, data, PrepareEntryOptions(key));
    }


    /// <inheritdoc/>
    public bool IsSet(CacheKey key) => _memoryCache.TryGetValue(key.Key ?? string.Empty, out _);


    /// <inheritdoc/>
    public bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action)
    {
        //ensure that lock is acquired
        if (IsSet(new(key)))
            return false;

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


    /// <inheritdoc/>
    public void Remove(string key) => _memoryCache.Remove(key);


    /// <inheritdoc/>
    public void RemoveByPrefix(string prefix)
    {
        _ = _prefixes.TryRemove(prefix, out CancellationTokenSource? tokenSource);
        tokenSource?.Cancel();
        tokenSource?.Dispose();
    }


    /// <inheritdoc/>
    public void Clear()
    {
        _clearToken.Cancel();
        _clearToken.Dispose();

        _clearToken = new();

        foreach (string prefix in _prefixes.Keys.ToList())
        {
            _ = _prefixes.TryRemove(prefix, out CancellationTokenSource? tokenSource);
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

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Protected implementation of Dispose pattern.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            _memoryCache.Dispose();

        _disposed = true;
    }

    #endregion
}
