using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Shitou.Framework.SignalR
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            
            //跨域支持策略
            services.AddCors(options =>
            {
                options.AddPolicy("Signalr",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });
            //add SignalR Service
            services.AddSignalR();
            services.AddMvc().AddJsonOptions(options =>
            {
                //对 JSON 数据使用混合大小写。跟属性名同样的大小写输出
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //对 JSON 数据使用混合大小写。驼峰式, 适用于javascript 首字母小写形式.
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            });
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info
                {
                    Title = "Samples API",
                    Version = "1.0",
                    Description = "A simple example ASP.NET Core Web API - V1.0",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "石头小神",
                        Email = string.Empty,
                        Url = "https://www.fxtcn.com/"
                    },
                    License = new License
                    {
                        Name = "石头小神",
                        Url = "https://www.fxtcn.com/"
                    }
                });
                config.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Shitou.Framework.SignalR.xml"), true);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("/message");
            });
            //跨域支持
            app.UseCors("Signalr");
            app.UseMvc();
            app.UseStaticFiles();
            //user Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
