using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Shitou.Framework.SignalR.Controllers
{
    /// <summary>
    /// ChatController
    /// </summary>
    [Produces("application/json")]
    [Route("api/Message")]
    [EnableCors("SignalrCore")]
    public class ChatController : Controller
    {
        private IHubContext<MessageHub> hubContext;
        /// <summary>
        /// 构造注入hubContext
        /// </summary>
        /// <param name="hubContext"></param>
        public ChatController(IHubContext<MessageHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="msgInfo"></param>
        /// <returns></returns>
        [HttpPost, Route("Send")]
        public IActionResult Send([FromBody] MsgInfo msgInfo)
        {
            IClientProxy clientProxy = null;
            if (!string.IsNullOrEmpty(msgInfo.GroupName))
            {
                clientProxy = hubContext.Clients.Group(msgInfo.GroupName);
            }
            else
            {
                clientProxy = hubContext.Clients.All;
            }
            clientProxy.SendAsync("Received", JsonConvert.SerializeObject(msgInfo));
            return Ok();
        }
    }
}