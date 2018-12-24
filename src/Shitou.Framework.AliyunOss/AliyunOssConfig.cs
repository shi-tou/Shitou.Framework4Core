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

namespace Shitou.Framework.AliyunOss
{
    public class AliyunOssConfig
    {
        public static AliyunOssConfig Instance;
        /// <summary>
        /// 访问密钥AccessKeyId
        /// </summary
        public string AccessKeyId { get; set; }
        /// <summary>
        /// 访问密钥AccessKeySecret
        /// </summary>
        public string AccessKeySecret { get; set; }
        /// <summary>
        /// Endpoint 表示 OSS 对外服务的访问域名
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// 分块上传、分块下载的大小（单位：K）
        /// </summary>
        public int PartSize { get; set; }
    }
}
