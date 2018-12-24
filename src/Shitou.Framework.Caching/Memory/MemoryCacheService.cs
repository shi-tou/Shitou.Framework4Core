using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Shitou.Framework.Caching.Memory
{
    /// <summary>
    /// 基于内存的缓存服务
    /// </summary>
    public class MemoryCacheService : IMemoryCacheService
    {
        private IMemoryCache _memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            _memoryCache.TryGetValue(key, out string value);
            return value;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            string value = Get(key);
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, string value)
        {
            try
            {
                _memoryCache.Set(key, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public bool Set(string key, string value, int seconds)
        {
            try
            {
                _memoryCache.Set(key, value, new DateTimeOffset(DateTime.Now.AddMonths(seconds)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T value)
        {
            return Set(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, int seconds)
        {
            return Set(key, JsonConvert.SerializeObject(value), seconds);
        }

        /// <summary>
        /// 移除指定key的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            _memoryCache.Remove(key);
            return true;
        }

        /// <summary>
        /// 合并缓存key前缀
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string MergeKey(string key)
        {
            if (string.IsNullOrEmpty(MemoryCacheConfig.CacheKeyPrefix))
            {
                return key;
            }
            return MemoryCacheConfig.CacheKeyPrefix + key;
        }
    }
}
