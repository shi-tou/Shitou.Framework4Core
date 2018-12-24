using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shitou.Framework.SignalR
{
    /// <summary>
    /// 消息
    /// </summary>
    public class MsgInfo
    {
        /// <summary>
        /// 客户端分组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 业务消息类型
        /// </summary>
        public string BizType { get; set; }
        /// <summary>
        /// 业务消息数据
        /// </summary>
        public object MsgContent { get; set; }
    }
}
