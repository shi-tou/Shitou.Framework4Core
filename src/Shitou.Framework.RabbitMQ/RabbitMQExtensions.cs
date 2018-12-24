/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：石头小神
*命名空间：Shitou.Framework.RabbitMQ
*文件名：  RedisExtensions
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-11 16:33:07
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-11 16:33:07
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        /// <summary>
        /// redis配置文件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path"></param>
        /// <param name="optional"></param>
        /// <param name="reloadOnChange"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddRabbitMQFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            builder.AddJsonFile(path, optional, reloadOnChange);
            IConfigurationRoot configuration = builder.Build();
            RabbitMQConfig.Instance = configuration.GetSection("RabbitMQConfig").Get<RabbitMQConfig>();
            return builder;
        }

        /// <summary>
        /// 注册redis缓存服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<RabbitMQService>();
            return services;
        }
    }
}
