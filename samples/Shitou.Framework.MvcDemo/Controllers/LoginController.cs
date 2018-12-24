 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Shitou.Framework.Demo.Application.Extensions;
using Shitou.Framework.Demo.DataContract.Base;
using Shitou.Framework.Demo.DataContract.Request;
using Shitou.Framework.Demo.DataContract.Response;
using Shitou.Framework.Demo.Model;
using Shitou.Framework.Demo.Service;
using Shitou.Framework.ORM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Shitou.Framework.Demo.Mvc.Controllers
{
    [Route("Login")]
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        public ISystemService SystemService { get; set; }
        public LoginController(ISystemService systemService)
        {
            SystemService = systemService;
        }

        public IActionResult Index(string t)
        {
            HttpContext.SignOutAsync();
            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Index")]
        public IActionResult Index(UserLoginRequest request)
        {
            UserLoginResponse loginResponse = SystemService.UserLogin(request);
            if (loginResponse.Result == RT.Success)
            {
                Result.IsOk = true;
                Result.Msg = "登录成功！";

                var claimIdentity = new ClaimsIdentity("Cookie");
                claimIdentity.AddClaim(new Claim(ClaimTypes.Sid, loginResponse.LoginUserInfo.ID.ToString()));
                claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginResponse.LoginUserInfo.ID.ToString()));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, loginResponse.LoginUserInfo.UserName));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Authentication, JsonConvert.SerializeObject(loginResponse.AuthList)));
                claimIdentity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(loginResponse.LoginUserInfo)));

                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                // 在上面注册AddAuthentication时，指定了默认的Scheme，在这里便可以不再指定Scheme。
                HttpContext.SignInAsync(claimsPrincipal);
            }
            else if (loginResponse.Result == RT.User_NotExist_UserName)
            {
                Result.IsOk = false;
                Result.Msg = "用户名不存在！";
            }
            else if (loginResponse.Result == RT.User_Error_Password)
            {
                Result.IsOk = false;
                Result.Msg = "密码不正确！";
            }
            return Json(Result);
        }
    }
}