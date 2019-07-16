
using Shitou.Framework.Demo.Repository;
using Shitou.Framework.Demo.Service;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Shitou.Framework.Demo.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register The Service Layer
        /// </summary>
        /// <param name="services"></param>
        public static void AddBusinessService(this IServiceCollection services, IConfiguration Configuration)
        {
            string connString = Configuration.GetConnectionString("Mysql");
            //dao
            services.AddSingleton<ISystemRepository>(new SystemRepository(connString));
            services.AddSingleton<IGoodsRepository>(new GoodsRepository(connString));

            //sevice
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IGoodsService, GoodsService>();
        }
    }
}
