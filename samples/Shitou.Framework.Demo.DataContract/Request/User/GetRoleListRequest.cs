using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shitou.Framework.Demo.DataContract;

namespace Shitou.Framework.Demo.DataContract.Request
{
    /// <summary>
    /// 查询角色列表请求
    /// </summary>
    public class GetRoleListRequest : PageRequest
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}