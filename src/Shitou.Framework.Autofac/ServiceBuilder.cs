/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：石头小神
*命名空间：Shitou.Framework.Autofac
*文件名：  ServiceBuilder
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-5 10:36:45
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-5 10:36:45
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shitou.Framework.Autofac
{
    /// <summary>
    /// 服务构建器
    /// </summary>
    public static class ServiceBuilder
    {
        /// <summary>
        /// 服务集合。
        /// </summary>
        private static ContainerBuilder _builder { get; set; }

        static ServiceBuilder()
        {
            _builder = new ContainerBuilder();
        }
        /// <summary>
        /// 注册所有继承了IService接口类服务
        /// </summary>
        public static void AddService()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList()
               .Where(
                   assembly =>
                       assembly.GetTypes().FirstOrDefault(type => type.GetInterfaces().Contains(typeof(IService))) !=
                       null
               );

            // RegisterAssemblyTypes 注册程序集
            var enumerable = assemblies as Assembly[] ?? assemblies.ToArray();
            if (enumerable.Any())
            {
                _builder.RegisterAssemblyTypes(enumerable)
                    .Where(type => type.GetInterfaces().Contains(typeof(IService))).AsSelf().InstancePerDependency();
            }

            // 把容器装入到微软默认的依赖注入容器中
            ServiceLoader.Current = _builder.Build();
        }

        /// <summary>
        /// 单例模式，每次调用，都会使用同一个实例化的对象；每次都用同一个对象；
        /// 示例：ServiceBuilder.AddSingleton<IxxxService,xxxService>();
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public static void AddSingleton<TInterface, TImplement>()
        {
            _builder.RegisterType<TImplement>().As<TInterface>().SingleInstance();
        }
        public static void AddSingleton<TImplement>()
        {
            _builder.RegisterType<TImplement>().SingleInstance();
        }

        /// <summary>
        /// 默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象；
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public static void AddTransient<TInterface, TImplement>()
        {
            _builder.RegisterType<TImplement>().As<TInterface>().InstancePerDependency();
        }
        public static void AddTransient<TImplement>()
        {
            _builder.RegisterType<TImplement>().InstancePerDependency();
        }
        /// <summary>
        /// 同一个Lifetime生成的对象是同一个实例
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public static void AddScoped<TInterface, TImplement>()
        {
            _builder.RegisterType<TImplement>().As<TInterface>().InstancePerLifetimeScope();
        }
        public static void AddScoped<TImplement>()
        {
            _builder.RegisterType<TImplement>().InstancePerLifetimeScope();
        }
    }
}
