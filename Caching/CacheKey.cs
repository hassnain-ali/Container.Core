﻿namespace Container.Core.Caching;

/// <summary>
/// 
/// </summary>
public partial class CacheKey
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    protected string _keyFormat = "";

    #endregion

    #region Ctor

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <param name="createCacheKeyParameters"></param>
    /// <param name="keyObjects"></param>
    public CacheKey(CacheKey cacheKey, Func<object, object> createCacheKeyParameters, params object[] keyObjects)
    {
        Init(cacheKey.Key, cacheKey.CacheTime, cacheKey.Prefixes.ToArray());

        if (!keyObjects.Any())
        {
            return;
        }

        Key = string.Format(_keyFormat, keyObjects.Select(createCacheKeyParameters).ToArray());

        for (int i = 0; i < Prefixes.Count; i++)
        {
            Prefixes[i] = string.Format(Prefixes[i], keyObjects.Select(createCacheKeyParameters).ToArray());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <param name="cacheTime"></param>
    /// <param name="prefixes"></param>
    public CacheKey(string cacheKey, int? cacheTime = null, params string[] prefixes)
    {
        Init(cacheKey, cacheTime, prefixes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <param name="prefixes"></param>
    public CacheKey(string cacheKey, params string[] prefixes)
    {
        Init(cacheKey, null, prefixes);
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Init instance of CacheKey
    /// </summary>
    /// <param name="cacheKey">Cache key</param>
    /// <param name="cacheTime">Cache time; set to null to use the default value</param>
    /// <param name="prefixes">Prefixes to remove by prefix functionality</param>
    protected void Init(string cacheKey, int? cacheTime = null, params string[] prefixes)
    {
        Key = cacheKey;

        _keyFormat = cacheKey;

        if (cacheTime.HasValue)
        {
            CacheTime = cacheTime.Value;
        }

        Prefixes.AddRange(prefixes.Where(prefix => !string.IsNullOrEmpty(prefix)));
    }

    #endregion

    /// <summary>
    /// Cache key
    /// </summary>
    public string Key { get; protected set; }

    /// <summary>
    /// Prefixes to remove by prefix functionality
    /// </summary>
    public List<string> Prefixes { get; protected set; } = new();

    /// <summary>
    /// Cache time in minutes
    /// </summary>
    public int CacheTime { get; set; } = 10;
}
