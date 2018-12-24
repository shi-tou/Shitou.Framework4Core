using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shitou.Framework.Demo.WebApi
{
    public class TokenAuthorizeMiddleware
    {

        /// <summary>
        /// 管道代理对象
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 权限中间件构造
        /// </summary>
        /// <param name="next">管道代理对象</param>
        /// <param name="permissionResitory">权限仓储对象</param>
        /// <param name="option">权限中间件配置选项</param>
        public TokenAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.ToString().Contains("swagger"))
            {
                var token = context.Request.Headers["x-token"].ToString();
                //验证token(这里只是简单处理，具体需要跟业务相结合)
                if (token != "53A9951D-223E-4DEF-9FF8-7F6EE7877145")
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(
                        JsonConvert.SerializeObject(new ApiResponse { MsgCode = "401", MsgContent = "token认证失败！", }));
                }
            }
            return _next(context);
        }
    }
    /// <summary>
    /// 扩展token中间件
    /// </summary>
    public static class TokenAuthorizeExtensions
    {
        /// <summary>
        /// 引入token验证中间件
        /// </summary>
        /// <param name="builder">扩展类型</param>
        /// <param name="option">权限中间件配置选项</param>
        /// <returns></returns>
        public static IApplicationBuilder UseTokenAuthorize(
              this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenAuthorizeMiddleware>();
        }
    }
}
