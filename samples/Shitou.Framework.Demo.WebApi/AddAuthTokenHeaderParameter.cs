using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shitou.Framework.Demo.WebApi
{
    /// <summary>
    /// token header
    /// </summary>
    public class AddAuthTokenHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// Swagger OperationFilter
        /// 对swagger请求，添加header参数
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }
            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "x-token",
                In = "header",
                Type = "string",
                Description = "服务端Token认证信息",
                Required = true,
                Default = "53A9951D-223E-4DEF-9FF8-7F6EE7877145"
            });
        }
    }
}
