

using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace Shitou.Framework.Log4net
{
    public static class Log4NetExtensions
    {
        private static ILoggerRepository _loggerRepository { get; set; }

        public static IHostingEnvironment ConfigureLog4Net(this IHostingEnvironment env, string configFileRelativePath = "log4net.config")
        {
            _loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(_loggerRepository, new FileInfo(Path.Combine(env.ContentRootPath, configFileRelativePath)));
            return env;
        }
        /// <summary>
        /// Ìí¼Ólog4net·þÎñ
        /// </summary>
        /// <param name="loggerFactory"></param>
        public static IServiceCollection AddLog4Net(this IServiceCollection services)
        {
            services.AddLogging(loggerBuilder =>
            {
                loggerBuilder.AddProvider(new Log4netProvider(_loggerRepository));
            });
            return services;
        }
    }
}
