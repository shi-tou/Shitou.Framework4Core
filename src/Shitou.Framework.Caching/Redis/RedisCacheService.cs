/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：石头小神
*命名空间：Shitou.Framework.Caching.Redis
*文件名：  RedisCacheService
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-6 13:56:07
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-6 13:56:07
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Caching.Redis
{
    /// <summary>
    /// 基于Redis的缓存服务
    /// </summary>
    public class RedisCacheService : IRedisCacheService
    {
        private static ConnectionMultiplexer _conn;

        public RedisCacheService()
        {
            _conn = ConnectionMultiplexer.Connect(RedisConfig.Instance.ToString());
        }

        public IDatabase GetDatabase()
        {
            try
            {
                return _conn.GetDatabase();
            }
            catch
            {
                _conn = ConnectionMultiplexer.Connect(RedisConfig.Instance.ToString());
                return _conn.GetDatabase();
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            try
            {
                key = MergeKey(key);
                return GetDatabase().StringGet(key);
            }
            catch
            {
                return "";
            }
            
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
                key = MergeKey(key);
                return GetDatabase().StringSet(key, value);
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
                key = MergeKey(key);
                return GetDatabase().StringSet(key, value, TimeSpan.FromSeconds(seconds));
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
            key = MergeKey(key);
            return GetDatabase().KeyDelete(key);
        }

        /// <summary>
        /// 合并缓存key前缀
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string MergeKey(string key)
        {
            if (string.IsNullOrEmpty(RedisConfig.Instance.CacheKeyPrefix))
            {
                return key;
            }
            return RedisConfig.Instance.CacheKeyPrefix + key;
        }
    }
}
