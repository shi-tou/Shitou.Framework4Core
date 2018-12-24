/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：石头小神
*命名空间：Shitou.Framework.Caching.Redis
*文件名：  RedisExtensions
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-6 14:33:07
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-6 14:33:07
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Caching.Memory
{
    public static class MemoryExtensions
    {
        /// <summary>
        /// 注册Memory缓存服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cacheKeyPrefix">缓存前缀</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomMemoryCache(this IServiceCollection services, string cacheKeyPrefix = "")
        {
            MemoryCacheConfig.CacheKeyPrefix = cacheKeyPrefix;
            //MemoryCacheService依赖于.net core内置的缓存服务, 这一行不可少
            services.AddMemoryCache();
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            services.AddSingleton<ICacheService, MemoryCacheService>();
            return services;
        }
    }
}
