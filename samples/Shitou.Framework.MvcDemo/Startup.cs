using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Shitou.Framework.Demo.Application.Extensions;
using Shitou.Framework.Demo.Application.Filter;
using Shitou.Framework.Caching.Redis;
using Shitou.Framework.Log4net;
using Shitou.Framework.ORM;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Pivotal.Discovery.Client;
using Shitou.Framework.Demo.Application.Middleware;
using Shitou.Framework.Demo.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using MySql.Data.MySqlClient;

namespace Shitou.Framework.Demo.Mvc
{
    /// <summary>
    /// 启动类Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                //add redis settings
                .AddRedisFile("Config/RedisSettings.json", optional: true, reloadOnChange: true);
            //configure log4net
            env.ConfigureLog4Net("config/log4net.config");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //add mvc by options
            services.AddMvc(options =>
            {
                //全局请求处理
                options.Filters.Add(typeof(GlobalActionFilterAttribute));
                //全局异常处理
                options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
                //权限验证过滤
                //options.Filters.Add(typeof(AuthFilterAttribute));
            })
            // add Json Format
            .AddJsonOptions(options =>
            {
                //对 JSON 数据使用混合大小写。跟属性名同样的大小写输出
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //对 JSON 数据使用混合大小写。驼峰式, 适用于javascript 首字母小写形式.
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            });
            //添加认证Cookie信息
            services.AddAuthentication(options =>
            {
                //DefaultSignInScheme, DefaultSignOutScheme, DefaultChallengeScheme, DefaultForbidScheme 等都会使用该 Scheme 作为默认值。
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            //用来注册 CookieAuthenticationHandler，由它来完成身份认证的主要逻辑。
            .AddCookie(options =>
            {
                // 在这里可以根据需要添加一些Cookie认证相关的配置，在本次示例中使用默认值就可以了。
                options.LoginPath = "/Login";
            });
            //mvc action上下文，在taghelper(LayuiPagerTagHelper.cs)扩展里用到
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //add session service
            services.AddSession(options =>
            {
                //设置过期时间为20分钟
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });

            //add log4net service
            services.AddLog4Net();
            //add redis service
            services.AddRedisCache();
            //add mysql
            services.AddMySql(new MySqlConnection(Configuration.GetConnectionString("Mysql")));
            //add bussiness service
            services.AddBusinessService();

            //add Steeltoe Discovery Client service
            //services.AddDiscoveryClient(Configuration);
            
            
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor session)
        {
            //Use the Steeltoe Discovery Client service
            //app.UseDiscoveryClient();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePagesWithReExecute("/Home/NoFound");
            //启用Session
            app.UseSession();
            //静态文件
            app.UseStaticFiles();
            //设置根目录下'Upload'文件可静态访问
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Upload")),
            //    RequestPath = new PathString("/Upload")
            //});

            //验证中间件
            app.UseAuthentication();
            app.UseAuth();
            //IP过滤
            //app.UseFilterIP(new FilterIPMiddlewareOption
            //{
            //    Ips = new List<string> { "127.0.0.1", "192.168.0.88" }
            //});
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
