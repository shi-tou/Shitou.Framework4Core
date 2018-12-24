
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace Shitou.Framework.Nlog
{
    public static class NLogExtensions
    {
        public static IHostingEnvironment ConfigureNLog(this IHostingEnvironment env, string configFileRelativePath = "nlog.config")
        {
            NLog.LogManager.LoadConfiguration(Path.Combine(env.ContentRootPath, configFileRelativePath));
            return env;
        }
        /// <summary>
        /// Ìí¼Ólog4net·þÎñ
        /// </summary>
        /// <param name="loggerFactory"></param>
        public static IServiceCollection AddNLog(this IServiceCollection services)
        {
            services.AddLogging(loggerBuilder =>
            {
                loggerBuilder.AddProvider(new NLogtProvider());
            });
            return services;
        }
    }
}
