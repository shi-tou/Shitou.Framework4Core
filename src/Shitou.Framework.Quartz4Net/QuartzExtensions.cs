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

namespace Shitou.Framework.Quartz4Net
{
    public static class QuartzExtensions
    {
        /// <summary>
        /// Quartz配置文件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path"></param>
        /// <param name="optional"></param>
        /// <param name="reloadOnChange"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddQuartzFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            builder.AddJsonFile(path, optional, reloadOnChange);
            IConfigurationRoot configuration = builder.Build();
            QuartzConfig.Instance = configuration.GetSection("QuartzConfig").Get<QuartzConfig>();
            return builder;
        }

        /// <summary>
        /// 注册Quartz服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services)
        {
            //services.AddSingleton<AliyunOssService>();
            return services;
        }
    }
}
