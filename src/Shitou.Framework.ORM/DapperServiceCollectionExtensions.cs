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
        /// 注册MySql服务(选用于不同用户登录，创建不同的数据源)
        /// 需要在程序启动时，设置数据库连接的委托DbConnectionSetting.DbConnection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySql(this IServiceCollection services)
        {
            if (DbConnectionSetting.DbConnection == null)
            {
                throw new NoNullAllowedException("DbConnectionSetting.DbConnection not allow null,please init it when app start");
            }
            services.AddTransient(_ => { return DbConnectionSetting.DbConnection(DbConnectionNameConst.MySql); });
            services.AddSingleton<ISqlGenerator, MySqlGenerator>();
            services.AddTransient<IAdoTemplate, AdoTemplate>();
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
