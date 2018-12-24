using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shitou.Framework.ORM.Mapper;

namespace Shitou.Framework.Demo.Model
{
    /// <summary>
    /// 权限信息
    /// </summary>
    [Table("T_Role_Auth")]
    public class RoleAuth
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public string AuthID { get; set; }
    }
}
