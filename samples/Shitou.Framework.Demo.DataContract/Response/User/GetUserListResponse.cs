using Shitou.Framework.Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Framework.Demo.DataContract.Response
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public class GetUserListResponse : UserInfo
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
    }
}
