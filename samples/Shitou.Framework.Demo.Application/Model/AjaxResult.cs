using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shitou.Framework.Demo.Application.Models
{ 
    /// <summary>
    /// Ajax请求结果
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 默认true
        /// </summary>
        public bool IsOk = true;
        public string Msg = "操作成功";
        public object Data = null;
    }
}