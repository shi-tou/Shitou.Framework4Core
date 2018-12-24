using System;
using System.Collections.Generic;

namespace Shitou.Framework.Caching
{
    public interface ICacheService
    {

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set(string key, string value);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        bool Set(string key, string value, int seconds);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value, int seconds);

        /// <summary>
        /// 移除指定key的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);
    }
}
