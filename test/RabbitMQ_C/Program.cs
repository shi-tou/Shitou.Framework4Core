using Shitou.Framework.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace RabbitMQ_C
{
    class Program
    {
        static RabbitMQService rabbitMQService;
        static void Main(string[] args)
        {
            //JObject obj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "rabbitmqsettings.json")));
            //RabbitMQConfig.Instance = obj["RabbitMQConfig"].ToObject<RabbitMQConfig>();
            //rabbitMQService = new RabbitMQService();
            ////订阅TestKey路由的消息
            //var f = rabbitMQService.Subscribe("SendRoutingKey", RabbitMQCallback);
            //Console.WriteLine("start listening...");
            //Console.ReadKey();


            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddRabbitMQFile("rabbitmqsettings.json", optional: true, reloadOnChange: true);

            rabbitMQService = new RabbitMQService();
            //订阅TestKey路由的消息
            var f = rabbitMQService.Subscribe("SendRoutingKey", RabbitMQCallback);
            Console.WriteLine("start listening...");
            Console.ReadKey();
        }

        private static void RabbitMQCallback(ulong deliveryTag, string routingKey, string body)
        {
            rabbitMQService.Ack(deliveryTag, false);
            Console.WriteLine(body);
        }
    }
}
