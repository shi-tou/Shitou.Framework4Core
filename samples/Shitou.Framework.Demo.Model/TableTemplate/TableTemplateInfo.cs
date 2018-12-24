using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shitou.Framework.ORM.Mapper;

namespace Shitou.Framework.Demo.Model
{
    /// <summary>
    /// 表单模板信息
    /// </summary>
    [Table("t_table_template", "ID")]
    public class TableTemplateInfo
    {
        public string ID { get; set; }
        /// <summary>
        /// 表单模板类型
        /// </summary>
        public int TemplateType { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TemplateContent { get; set; }
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
