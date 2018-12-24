/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：石头小神
*命名空间：Shitou.Framework.Caching.Redis
*文件名：  RedisOptions
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-6 11:37:11
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-6 11:37:11
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Caching.Redis
{
    public class RedisConfig
    {
        private static string connString = "{0}:{1},allowAdmin=true,password={2}";
        public static RedisConfig Instance;

        /// <summary>
        /// 主机
        /// </summary
        public string Server { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 默认数据库
        /// </summary>
        public string DefaultDatabase { get; set; }
        /// <summary>
        /// 用于区分各产品的缓存key
        /// </summary>
        public string CacheKeyPrefix { get; set; }

        public override string ToString()
        {
            return string.Format(connString, Server, Port, Password);
        }
    }
}
