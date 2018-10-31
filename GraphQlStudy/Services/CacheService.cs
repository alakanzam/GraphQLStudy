using System.Collections.Concurrent;
using System.Collections.Generic;
using GraphQlStudy.Interfaces;

namespace GraphQlStudy.Services
{
    public class CacheService<TKey, TValue>: ICacheService<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _cache;

        public CacheService()
        {
            _cache = new ConcurrentDictionary<TKey, TValue>();
        }

        public TValue Get(TKey key)
        {
            if (!HasKey(key))
                return default(TValue);

            return _cache[key];
        }

        public void Set(TKey key, TValue value)
        {
            if (HasKey(key))
            {
                _cache[key] = value;
                return;
            }

            _cache.Add(key, value);
        }

        public bool HasKey(TKey key)
        {
            if (_cache.ContainsKey(key))
                return true;

            return false;
        }

        public void Delete(TKey key)
        {
            if (!HasKey(key))
                return;

            _cache.Remove(key);
        }
    }
}