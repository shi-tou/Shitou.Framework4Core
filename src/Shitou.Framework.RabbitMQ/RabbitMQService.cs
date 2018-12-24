using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.RabbitMQ
{
    public class RabbitMQService
    {
        /// <summary>
        /// 消息回调委托
        /// </summary>
        /// <param name="deliveryTag">消息的index</param>
        /// <param name="routingKey">路由键</param>
        /// <param name="body">消息内容</param>
        public delegate void RabbitMQCallback(ulong deliveryTag, string routingKey, string body);
        /// <summary>
        /// 发送消息信道
        /// </summary>
        private IModel _sendChannel { get; set; }
        /// <summary>
        /// 接收消息信道
        /// </summary>
        private IModel _receiveChannel { get; set; }
        /// <summary>
        /// 发送消息标记属性
        /// </summary>
        private IBasicProperties _sendProperties { get; set; }
        /// <summary>
        /// 订阅时指定RoutingKey的Callback Event列表
        /// </summary>
        private Dictionary<string, List<RabbitMQCallback>> _routingKeyCallbackList = new Dictionary<string, List<RabbitMQCallback>>();
        /// <summary>
        /// 所有RoutingKey的Callback Event列表
        /// </summary>
        private List<RabbitMQCallback> _allKeyCallbackList = new List<RabbitMQCallback>();

        private RabbitMQConfig _rabbitMQConfig;
        /// <summary>
        /// 日志
        /// </summary>
        private ILogger _logger;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public RabbitMQService(ILogger logger)
        {
            _logger = logger;
            try
            {
                _rabbitMQConfig = RabbitMQConfig.Instance;
                Init();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载rabbitmqsetting.json文件失败");
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public RabbitMQService()
        {
            _rabbitMQConfig = RabbitMQConfig.Instance;
            Init();
        }

        /// <summary>
        /// 初始化Rabbit服务
        /// </summary>
        public void Init()
        {
            if (_rabbitMQConfig == null)
            {
                _logger?.LogError("RabbitMQService.Init 失败，未加载RabbitMQ配置信息");
                return;
            }
            _logger?.LogInformation("RabbitMQService.Init()");
            try
            {
                //实例化RabbitMQ连接工厂
                ConnectionFactory factory = new ConnectionFactory
                {
                    HostName = _rabbitMQConfig.ConnectionFactory.HostName,
                    UserName = _rabbitMQConfig.ConnectionFactory.UserName,
                    Password = _rabbitMQConfig.ConnectionFactory.Password,
                    RequestedHeartbeat= (ushort)_rabbitMQConfig.ConnectionFactory.RequestedHeartbeat,
                    AutomaticRecoveryEnabled= _rabbitMQConfig.ConnectionFactory.AutomaticRecoveryEnabled
                };
                //建立RabbitMQ连接
                IConnection connection = factory.CreateConnection();            
                connection.CallbackException += Connection_CallbackException;//异常回调
                connection.ConnectionBlocked += Connection_ConnectionBlocked;//
                connection.ConnectionUnblocked += Connection_ConnectionUnblocked;
                connection.ConnectionShutdown += Connection_ConnectionShutdown;//连接断开事件

                _sendChannel = connection.CreateModel();
                _sendProperties = _sendChannel.CreateBasicProperties();
                _sendProperties.Persistent = true;
                _receiveChannel = connection.CreateModel();


                //根据配置信息为[发送/接收channel]初始化交换机及队列
                foreach (RabbitMQExchange exchange in _rabbitMQConfig.ExchangeList)
                {
                    //为信道声明一个交换机
                    _sendChannel.ExchangeDeclare(exchange.Name, exchange.Type);
                    _receiveChannel.ExchangeDeclare(exchange.Name, exchange.Type);

                    foreach (RabbitMQQueue queue in exchange.QueueList)
                    {
                        //为信道声明队列
                        if (queue.Type.Trim() == QueueType.Send)
                        {
                            _sendChannel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, null);
                            _sendChannel.QueueBind(queue.Name, exchange.Name, queue.RoutingKey);
                        }
                        else
                        {
                            _receiveChannel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, null);
                            _receiveChannel.QueueBind(queue.Name, exchange.Name, queue.RoutingKey);
                        }
                    }
                }
                //设置prefetchCount为1，告知RabbitMQ，在未收到消费端的消息确认时，不再分发消息，确保当消费端处于忙碌状态时，不再分配任务。
                _receiveChannel.BasicQos(0, 1, false);
                //构造消费者实例
                var consumer = new EventingBasicConsumer(_receiveChannel);
                //绑定消息接收后的事件委托
                consumer.Received += Consumer_Received;
                //启动消费者
                foreach (RabbitMQExchange exchange in _rabbitMQConfig.ExchangeList)
                {
                    foreach (RabbitMQQueue queue in exchange.QueueList)
                    {
                        if (queue.Type == QueueType.Receive)
                        {
                            _receiveChannel.BasicConsume(queue.Name, false, consumer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "RabbitMQService.Init： 初始化Rabbit服务失败");
            }
        }
        
        /// <summary>
        /// 消息者接收消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            string receivedBody = Encoding.UTF8.GetString(e.Body);
            _logger?.LogInformation(string.Format("接收到RabbitMQ消息{0}", receivedBody));

            if (_routingKeyCallbackList.Count > 0 && _routingKeyCallbackList.ContainsKey(e.RoutingKey))
            {
                //获取指定RoutingKey的回调事件
                List<RabbitMQCallback> actionList = _routingKeyCallbackList[e.RoutingKey];
                if (actionList.Count == 0)
                    return;
                //执行回调
                foreach (var action in actionList)
                {
                    try
                    {
                        action(e.DeliveryTag, e.RoutingKey, receivedBody);
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex, string.Format("RabbitMQService.Consumer_Received 异常：DeliveryTag->{0},RoutingKey->{1},Body->{2}",
                            e.DeliveryTag, e.RoutingKey, e.Body));
                    }
                }
            }

            if (_allKeyCallbackList.Count > 0)
            {
                foreach (var action in _allKeyCallbackList)
                {
                    try
                    {
                        action(e.DeliveryTag, e.RoutingKey, receivedBody);
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex, string.Format("RabbitMQService.Consumer_Received 异常：DeliveryTag->{0},RoutingKey->{1},Body->{2}",
                             e.DeliveryTag, e.RoutingKey, e.Body));
                    }
                }
            }
        }

        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="routingKey"></param>
        /// <param name="body"></param>
        public bool Send(string exchangeName, string routingKey, string body)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(body);
                _sendChannel.BasicPublish(exchangeName, routingKey, _sendProperties, bytes);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "RabbitMQService.Send：消息发送异常");
                return false;
            }
        }

        /// <summary>
        /// 订阅消息(所有消息)
        /// </summary>
        /// <param name="callback"></param>
        public bool Subscribe(RabbitMQCallback callback)
        {
            if (callback == null)
            {
                _logger?.LogError("RabbitMQService.Subscribe：callback为空");
                throw new ArgumentNullException("callback");
            }
            if (_allKeyCallbackList.Contains(callback))
            {
                return true;
            }
            try
            {
                lock (_allKeyCallbackList)
                {
                    if (_allKeyCallbackList.Contains(callback) == false)
                    {
                        _allKeyCallbackList.Add(callback);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex, "RabbitMQService.Subscribe：订阅消息(所有消息)失败。");
                return false;
            }
        }

        /// <summary>
        /// 订阅消息(指定routingKey)
        /// </summary>
        /// <param name="routingKey"></param>
        /// <param name="callback"></param>
        public bool Subscribe(string routingKey, RabbitMQCallback callback)
        {
            if (string.IsNullOrEmpty(routingKey))
            {
                _logger?.LogError("RabbitMQService.Subscribe：routingKey为空");
                throw new ArgumentNullException("routingKey");
            }

            if (callback == null)
            {
                _logger?.LogError("RabbitMQService.Subscribe：callback为空");
                throw new ArgumentNullException("callback");
            }

            if (_routingKeyCallbackList.ContainsKey(routingKey) == false)
            {
                lock (_routingKeyCallbackList)
                {
                    if (_routingKeyCallbackList.ContainsKey(routingKey) == false)
                    {
                        _routingKeyCallbackList.Add(routingKey, new List<RabbitMQCallback>());
                    }
                }
            }
            List<RabbitMQCallback> actionList = _routingKeyCallbackList[routingKey];
            if (actionList.Contains(callback) == false)
            {
                lock (actionList)
                {
                    if (actionList.Contains(callback) == false)
                    {
                        actionList.Add(callback);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 消息应答
        /// </summary>
        /// <param name="deliveryTag"></param>
        /// <param name="multiple"></param>
        public bool Ack(ulong deliveryTag, bool multiple)
        {
            try
            {
                _receiveChannel.BasicAck(deliveryTag, multiple);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "RabbitMQService.Ack：消息应答异常");
                return false;
            }
        }

        #region ConnectionEvent异常处理
        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger?.LogError(string.Format("Connection_ConnectionShutdown,ReplyCode:{0},ReplyText:{1}", e.ReplyCode, e.ReplyText));
        }

        private void Connection_ConnectionUnblocked(object sender, EventArgs e)
        {
            _logger?.LogError("Connection_ConnectionUnblocked");
        }

        private void Connection_ConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            _logger?.LogError("Connection_ConnectionBlocked:" + e.Reason);
        }

        private void Connection_CallbackException(object sender, CallbackExceptionEventArgs e)
        {
            _logger?.LogError(e.Exception, "Connection_CallbackException");
        }
        #endregion
    }
}
