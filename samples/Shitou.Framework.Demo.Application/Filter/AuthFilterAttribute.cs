 using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shitou.Framework.Demo.Application.Extensions;
using Shitou.Framework.Demo.Model;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shitou.Framework.Demo.Application.Models;

namespace Shitou.Framework.Demo.Application.Filter
{
    /// <summary>
    /// 验证用户登录状态及权限
    /// </summary>
    public class AuthFilterAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// do something before the action executes
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {           
            //匿名访问
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                return;

            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
           
            //获取用户登录信息
            UserInfo userInfo = context.HttpContext.Session.Get<UserInfo>("Yiho_LoginUserInfo");
            //登录信息为空（未登录或登录失效）
            if (userInfo == null)
            {
                context.HttpContext.Response.Redirect("/Login");
                return;
            }
            //超级管理员、Home模块不验证用户权限
            if (userInfo.IsAdmin || controller.ToLower() == "home")
            {
                return;
            }
            //获取用户权限
            List<AuthInfo> LoginUserAuthList = context.HttpContext.Session.Get<List<AuthInfo>>("Yiho_LoginUserAuthInfo");
            //用户权限为空
            if (LoginUserAuthList == null)
            {
                if (!context.HttpContext.Request.IsAjax())
                {
                    context.HttpContext.Response.Redirect("/Home/NoAuthTip");
                }
                else
                {
                    context.Result = new JsonResult(new AjaxResult { IsOk = false, Msg = "操作权限不足，请联系管理员！" });
                }
                return;

            }
            //验证权限
            var authInfo = LoginUserAuthList.Where(a => a.AuthCode.ToLower() == string.Format("{0}.{1}", controller, action).ToLower());
            if (authInfo == null || authInfo.Count() == 0)
            {
                if (!context.HttpContext.Request.IsAjax())
                {
                    context.HttpContext.Response.Redirect("/Home/NoAuthTip");
                }
                else
                {
                    context.Result = new JsonResult(new AjaxResult { IsOk = false, Msg = "操作权限不足，请联系管理员！" });
                }
                return;
            }
            //var args = context.ActionArguments;
            //var result = context.Result;
            //ActionArguments - lets you manipulate the inputs to the action.

        }
    }
}

