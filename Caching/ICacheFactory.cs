namespace Container.Core.Caching;

public interface ICacheFactory
{
    void Clear();
    void Dispose();
    T GetOrCreate<T>(CacheKey key, Func<T> acquire);
    Task<T> GetOrCreateAsync<T>(CacheKey key, Func<Task<T>> acquire);
    bool IsSet(CacheKey key);
    bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action);
    void Remove(CacheKey key);
    void Remove(string key);
    void RemoveByPrefix(string prefix);
    void Set(CacheKey key, object data);
}
