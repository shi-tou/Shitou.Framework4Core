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
    public interface IRedisCacheService : ICacheService
    {
        
    }
}
