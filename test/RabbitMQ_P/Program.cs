using Shitou.Framework.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace RabbitMQ_P
{
    class Program
    {
        static RabbitMQService rabbitMQService;
        static void Main(string[] args)
        {
            try
            {
                //JObject obj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "rabbitmqsettings.json")));
                //RabbitMQConfig.Instance = obj["RabbitMQConfig"].ToObject<RabbitMQConfig>();
                //rabbitMQService = new RabbitMQService();
                //string v = "";
                //while (v != "q")
                //{
                //    Console.WriteLine("请输入发送的内容");
                //    v = Console.ReadLine();
                //    var f = rabbitMQService.Send("ExchangeName_A", "SendRoutingKey", v);
                //}
                //Console.ReadKey();


                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddRabbitMQFile("rabbitmqsettings.json", optional: true, reloadOnChange: true);

                rabbitMQService = new RabbitMQService();
                string v = "";
                while (v != "q")
                {
                    Console.WriteLine("请输入发送的内容");
                    v = Console.ReadLine();
                    var f = rabbitMQService.Send("ExchangeName_A", "SendRoutingKey", v);
                }
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
