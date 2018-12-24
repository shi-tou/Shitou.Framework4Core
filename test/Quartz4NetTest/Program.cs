using Shitou.Framework.Quartz4Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Quartz4NetTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //JObject obj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "quartzsettings.json")));
            //QuartzConfig.Instance = obj["QuartzConfig"].ToObject<QuartzConfig>();
            //QuartzService.Init();
            //Console.WriteLine("start quartz service...");
            //Console.ReadKey();


            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddQuartzFile("quartzsettings.json", optional: true, reloadOnChange: true);

            QuartzService.Init();
            Console.WriteLine("start quartz service...");
            Console.ReadKey();
        }
    }

}
