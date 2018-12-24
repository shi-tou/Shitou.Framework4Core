/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：石头小神
*命名空间：Shitou.Framework.RabbitMQ
*文件名：  RedisExtensions
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-11 15:33:07
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-11 15:33:07
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.RabbitMQ
{
    public class RabbitMQConfig
    {
        public static RabbitMQConfig Instance;

        /// <summary>
        /// Main entry point to the RabbitMQ
        /// </summary>
        public RabbitMQConnectionFactory ConnectionFactory { get; set; }

        /// <summary>
        ///  交换机
        /// </summary>
        public List<RabbitMQExchange> ExchangeList { get; set; }
    }

    /// <summary>
    /// Main entry point to the RabbitMQ
    /// </summary>
    public class RabbitMQConnectionFactory
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 心跳超时时间(秒)
        /// </summary>
        public int RequestedHeartbeat { get; set; }
        /// <summary>
        /// 是否自动重连
        /// </summary>
        public bool AutomaticRecoveryEnabled { get; set; }
    }

    /// <summary>
    /// 交换机Exchange
    /// Exchange用于对消息进行路由，将消息发送到多个队列上。一方面从生产者接收消息，另一方面将消息推送到队列
    /// 常见的Exchange Type
    /// direct：（明确的路由规则：消费端绑定的队列名称必须和消息发布时指定的路由名称一致
    /// topic：（模式匹配的路由规则：支持通配符）
    /// fanout：（消息广播，将消息分发到exchange上绑定的所有队列上
    /// </summary>
    public class RabbitMQExchange
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 交换机类型（direct、topic、fanout）
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 绑定的队列
        /// </summary>
        public List<RabbitMQQueue> QueueList { get; set; }
    }

    /// <summary>
    ///  队列Queue
    /// </summary>
    public class RabbitMQQueue
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 持久化消息
        /// </summary>
        public bool Durable { get; set; }
        /// <summary>
        /// 排他性（connection断了，自动删除队列包括消息）
        /// </summary>
        public bool Exclusive { get; set; }
        /// <summary>
        /// 自动删除（当所有接收都都断开队列时，自动删除包括消息）
        /// </summary>
        public bool AutoDelete { get; set; }
        /// <summary>
        /// 路由键
        /// </summary>
        public string RoutingKey { get; set; }
        /// <summary>
        /// 自定义属性，用于区别声明队列时，是用于发送消息不是接收消息(send/receive)
        /// </summary>
        public string Type { get; set; }

    }

    public class QueueType
    {
        public const string Send = "send";
        public const string Receive = "receive";
    }
}
