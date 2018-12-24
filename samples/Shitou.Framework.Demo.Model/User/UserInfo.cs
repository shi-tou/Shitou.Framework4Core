using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shitou.Framework.ORM.Mapper;

namespace Shitou.Framework.Demo.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Table("T_User", "ID")]
    public class UserInfo
    {
        public string ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 是否超级管理员(如果是，则不验证权限)
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
