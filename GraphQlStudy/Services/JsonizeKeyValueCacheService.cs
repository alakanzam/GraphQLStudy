using System.Collections.Concurrent;
using System.Collections.Generic;
using GraphQlStudy.Interfaces;
using Newtonsoft.Json;

namespace GraphQlStudy.Services
{
    public class JsonizeKeyValueCacheService<TKey, TValue>: ICacheService<TKey, TValue>
    {
        #region Properties

        /// <summary>
        /// Cache dictionary.
        /// </summary>
        private readonly IDictionary<string, TValue> _cache;

        #endregion

        #region Constructor

        public JsonizeKeyValueCacheService()
        {
            _cache = new ConcurrentDictionary<string, TValue>();
        }

        #endregion

        #region  Methods

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue Get(TKey key)
        {
            if (!HasKey(key))
                return default(TValue);

            var formattedKey = FormatKey(key);
            return _cache[formattedKey];
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void Set(TKey key, TValue value)
        {
            var formattedKey = FormatKey(key);
            if (HasKey(key))
            {
                _cache[formattedKey] = value;
                return;
            }
            
            _cache.Add(formattedKey, value);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool HasKey(TKey key)
        {
            var formattedKey = FormatKey(key);
            if (_cache.ContainsKey(formattedKey))
                return true;

            return false;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="key"></param>
        public virtual void Delete(TKey key)
        {
            if (!HasKey(key))
                return;

            var formattedKey = FormatKey(key);
            _cache.Remove(formattedKey);
        }

        /// <summary>
        /// Find key in cache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual string FormatKey(TKey key)
        {
            return JsonConvert.SerializeObject(key);
        }

        #endregion
    }
}