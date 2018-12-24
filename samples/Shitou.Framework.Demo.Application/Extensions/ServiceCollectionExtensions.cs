
using Shitou.Framework.Demo.Dao;
using Shitou.Framework.Demo.Service;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;


namespace Shitou.Framework.Demo.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register The Service Layer
        /// </summary>
        /// <param name="services"></param>
        public static void AddBusinessService(this IServiceCollection services)
        {
            //sevice
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<ISystemService, SystemService>();

            //dao
            services.AddTransient<ISystemDao, SystemDao>();
        }
    }
}
