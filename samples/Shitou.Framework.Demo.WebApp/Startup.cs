using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Shitou.Framework.Demo.Application.Extensions;
using Shitou.Framework.Demo.Application.Filter;
using Shitou.Framework.ORM;

namespace Shitou.Framework.Demo.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
            });
            //add mysql
            services.AddMySql(new MySqlConnection(Configuration.GetConnectionString("Mysql")));
            //add bussiness service
            services.AddBusinessService();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
