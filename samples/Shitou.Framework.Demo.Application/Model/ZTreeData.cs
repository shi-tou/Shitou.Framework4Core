using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shitou.Framework.Demo.Application.Models
{
    /// <summary>
    /// 用于Ztree插件绑定的数据源
    /// </summary>
    public class ZTreeData
    {
        public string id { get; set; }
        public string pId { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        [JsonProperty("checked")]
        public bool Checked { get; set; }
    }
}