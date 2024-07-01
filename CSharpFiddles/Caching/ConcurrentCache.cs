using System.Collections.Concurrent;

namespace CSharpFiddles.Caching;

/// <summary>
/// Generic cache which uses ConcurrentDictionary and a value creator supplied by the user.
/// If a matching key exists, the cached result will be fetched, otherwise the value will be created / retrieved.
/// </summary>
/// <typeparam name="T1">Key type.</typeparam>
/// <typeparam name="T2">Value type.</typeparam>
public class ConcurrentCache<T1, T2> where T1 : notnull
{
    private readonly ConcurrentDictionary<T1, T2> concurrentCache = new ();

    public T2 Get(T1 key, Func<T1, T2> valueCreator)
    {
        if (concurrentCache.TryGetValue(key, out var value))
        {
            return value;
        }

        var createdValue = valueCreator.Invoke(key);
        concurrentCache[key] = createdValue;
        return createdValue;
    }
}