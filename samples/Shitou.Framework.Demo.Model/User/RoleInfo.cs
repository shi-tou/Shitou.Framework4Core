using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shitou.Framework.ORM.Mapper;

namespace Shitou.Framework.Demo.Model
{
    /// <summary>
    /// 角色信息
    /// </summary>
    [Table("T_Role")]
    public class RoleInfo
    {
        public string ID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
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
