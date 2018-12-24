using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shitou.Framework.Demo.Application.Models;
using Shitou.Framework.Demo.Model;
using Shitou.Framework.Demo.Application.Extensions;
using Shitou.Framework.Demo.Service;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Shitou.Framework.Demo.Mvc.Controllers
{
    /// <summary>
    /// BaseController
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// Ajax请求结果
        /// </summary>
        public AjaxResult Result { get; set; }

        /// <summary>
        ///  登录信息
        /// </summary>
        public UserInfo LoginUserInfo
        {
            get
            {
                return JsonConvert.DeserializeObject<UserInfo>(User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.UserData).Value);
            }
           
        }
        /// <summary>
        /// 权限
        /// </summary>
        public List<AuthInfo> LoginUserAuthList
        {
            get
            {
                return JsonConvert.DeserializeObject<List<AuthInfo>>(User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Authentication).Value);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseController()
        {
            Result = new AjaxResult();
        }

        #region 下拉数据
        /// <summary>
        /// 构造性别下拉框数据
        /// </summary>
        /// <returns></returns>
        protected SelectList GetSexForSelect()
        {
            List<TextValue> list = new List<TextValue>
            {
                new TextValue { Value = 0, Text = "请选择" },
                new TextValue { Value = 1, Text = "男" },
                new TextValue { Value = 2, Text = "女" }
            };
            return new SelectList(list, "Value", "Text");
        }
        /// <summary>
        /// 构造权限类别下拉框数据
        /// </summary>
        /// <returns></returns>
        protected SelectList GetAuthTypeForSelect()
        {
            List<TextValue> list = new List<TextValue>
            {
                new TextValue { Value = (int)AuthType.Module, Text = "模块" },
                new TextValue { Value =  (int)AuthType.Page, Text = "页面" },
                new TextValue { Value =  (int)AuthType.Action, Text = "操作" }
            };
            return new SelectList(list, "Value", "Text");
        }

        #endregion

        #region 上传图片
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PicType"></param>
        /// <param name="targetID"></param>
        /// <param name="fileName"></param>
        protected string UploadPic(IFormFile file, string fileName)
        {
            try
            {
                string rootPath = Environment.CurrentDirectory;
                string uploadDir = Path.Combine(rootPath, "upload/" + fileName);
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string relativePath = string.Format("/upload/{0}/{1}.jpg", fileName, Guid.NewGuid().ToString());
                using (var fileStream = new FileStream(rootPath.TrimEnd('/') + relativePath, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }
                return relativePath;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "UploadPic");
                return "";
            }
        }
        #endregion

    }
}