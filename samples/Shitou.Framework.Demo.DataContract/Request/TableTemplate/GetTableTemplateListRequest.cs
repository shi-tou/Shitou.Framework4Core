using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shitou.Framework.Demo.DataContract;

namespace Shitou.Framework.Demo.DataContract.Request
{
    /// <summary>
    /// 查询表单模板列表请求
    /// </summary>
    public class GetTableTemplateListRequest : PageRequest
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }
    }
}