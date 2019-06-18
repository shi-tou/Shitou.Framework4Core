using Shitou.Framework.ORM.Generator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shitou.Framework.ORM
{
    public static class DapperServiceCollectionExtensions
    {
        /// <summary>
        /// 注册SqlServer服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IDbConnection connection)
        {
            services.AddTransient(_ => { return connection; });
            services.AddSingleton<ISqlGenerator, SqlServerGenerator>();
            services.AddSingleton<IAdoTemplate, AdoTemplate>();
            return services;
        }
        /// <summary>
        /// 注册MySql服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySql(this IServiceCollection services, IDbConnection connection)
        {
            services.AddTransient(_ => { return connection; });
            services.AddSingleton<ISqlGenerator, MySqlGenerator>();
            services.AddSingleton<IAdoTemplate, AdoTemplate>();
            return services;
        }
        /// <summary>
        /// 注册Sqlite服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlite(this IServiceCollection services, IDbConnection connection)
        {
            services.AddTransient(_ => { return connection; });
            services.AddSingleton<ISqlGenerator, SqliteGenerator>();
            services.AddTransient<IAdoTemplate, AdoTemplate>();
            return services;
        }
    }
}
