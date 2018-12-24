using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shitou.Framework.ORM.Mapper;

namespace Shitou.Framework.Demo.Model
{
    /// <summary>
    /// 表单字段信息
    /// </summary>
    [Table("t_table_field", "ID")]
    public class TableFieldInfo
    {
        public string ID { get; set; }
        /// <summary>
        /// 所属表单模板ID
        /// </summary>
        public string TableTemplateID { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int FieldType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 所属表单模板ID
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 所属表单模板ID
        /// </summary>
        public string FieldValues { get; set; }
        /// <summary>
        /// 所属表单模板ID
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
