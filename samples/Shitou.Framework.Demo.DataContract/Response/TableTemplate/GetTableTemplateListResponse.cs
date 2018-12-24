using Shitou.Framework.Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Framework.Demo.DataContract.Response
{
    /// <summary>
    /// 表单模板列表
    /// </summary>
    public class GetTableTemplateListResponse : TableTemplateInfo
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
    }
}
