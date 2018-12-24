using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shitou.Framework.SignalR
{
    /// <summary>
    /// ChatHub
    /// </summary>
    public class MessageHub: Hub
    {
        #region override method
        /// <summary>
        /// 每当有新客户端连接到 Hub 时调用。 
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            //通知上线
            Clients.All.SendAsync("NoticeOnline", $"{Context.ConnectionId}上线了！ ");
            return base.OnConnectedAsync();
        }
        /// <summary>
        /// 每当有客户端与 Hub 断开连接时调用。 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            //通知下线
            Clients.All.SendAsync("NoticeOffline", $"{Context.ConnectionId}下线了！ ");
            return base.OnDisconnectedAsync(exception);
        }
        #endregion

        #region biz method
        /// <summary>
        /// 初始化客户端连接
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <returns></returns>
        public async Task InitConnection(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        #endregion
    }
}
