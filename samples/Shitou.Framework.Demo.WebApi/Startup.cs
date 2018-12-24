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

namespace Shitou.Framework.Demo.WebApi
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
            services.AddMvc().AddJsonOptions(options =>
            {
                //对 JSON 数据使用混合大小写。跟属性名同样的大小写输出
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //对 JSON 数据使用混合大小写。驼峰式, 适用于javascript 首字母小写形式.
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            });
            //add swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info
                {
                    Title = "Shitou.Framework.Demo.WebApi",
                    Version = "1.0",
                    Description = "A simple example of ASP.NET Core Web API - V1.0",
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
                config.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Shitou.Framework.Demo.WebApi.xml"), true);
                config.OperationFilter<AddAuthTokenHeaderParameter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseTokenAuthorize();
            app.UseMvc();
            //user Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo.WebApi-v1");
            });
        }
    }
}
