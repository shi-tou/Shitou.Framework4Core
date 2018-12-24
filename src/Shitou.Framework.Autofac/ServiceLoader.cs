/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：石头小神
*命名空间：Shitou.Framework.Autofac
*文件名：  ServiceLoader
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-5 10:36:24
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-5 10:36:24
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Autofac
{
    /// <summary>
    /// 服务加载器
    /// </summary>
    public class ServiceLoader
    {
        /// <summary>
        /// 容器
        /// </summary>
        public static IContainer Current { get; set; }

        #region 判断服务实例是否注册

        public static bool IsRegistered<T>()
        {
            return Current.IsRegistered<T>();
        }

        public static bool IsRegistered<T>(string serviceKey)
        {
            return Current.IsRegisteredWithKey<T>(serviceKey);
        }

        public static bool IsRegistered(Type serviceType)
        {
            return Current.IsRegistered(serviceType);
        }

        public static bool IsRegisteredWithKey(string serviceKey, Type serviceType)
        {
            return Current.IsRegisteredWithKey(serviceKey, serviceType);
        }
        #endregion

        #region 获取服务实例

        /// <summary>
        /// 当注册多个实例时，该方法将会返回最后一个注册的实例，
        /// 注册多个实例建议设置serviceKey,  GetService(string key, Type type)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return Current.Resolve<T>();
        }

        public static T GetService<T>(string serviceKey)
        {

            return Current.ResolveKeyed<T>(serviceKey);
        }

        public static object GetService(Type serviceType)
        {
            return Current.Resolve(serviceType);
        }

        public static object GetService(string serviceKey, Type serviceType)
        {
            return Current.ResolveKeyed(serviceKey, serviceType);
        }

        #endregion
    }
}
