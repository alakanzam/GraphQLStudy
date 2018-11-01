using System.Collections.Concurrent;
using System.Collections.Generic;
using GraphQlStudy.Interfaces;

namespace GraphQlStudy.Services
{
    public class BaseKeyValueCacheService<TKey, TValue>: ICacheService<TKey, TValue>
    {
        #region Properties

        /// <summary>
        /// Cache dictionary.
        /// </summary>
        private readonly IDictionary<TKey, TValue> _cache;

        #endregion

        #region Constructor

        public BaseKeyValueCacheService()
        {
            _cache = new ConcurrentDictionary<TKey, TValue>();
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
            if (HasKey(key))
            {
                _cache[key] = value;
                return;
            }

            var formattedKey = FormatKey(key);
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
        protected virtual TKey FormatKey(TKey key)
        {
            return key;
        }

        #endregion
        
    }
}