
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shitou.Framework.Demo.DataContract;

namespace Shitou.Framework.Demo.DataContract.Request
{
    /// <summary>
    /// 查询用户列表请求
    /// </summary>
    public class GetUserListRequest : PageRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
    }
}