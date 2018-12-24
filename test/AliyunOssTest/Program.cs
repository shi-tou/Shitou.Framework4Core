using Shitou.Framework.AliyunOss;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AliyunOssTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddAliyunOssFile("OssSettings.json", optional: true, reloadOnChange: true);

            AliyunOssService service = new AliyunOssService();

            var buckets = service.ListBuckets();
            //service.UploadFile("yiho", "eef0ef09-f6a2-4362-99ae-2580d9ff139f", "E:\\test.txt");
            //service.DownloadFile("yiho", "eef0ef09-f6a2-4362-99ae-2580d9ff139f", "E:\\test1.txt");
            //service.UploadFileByMultipart("yiho", "13620E17-976A-4FFF-92E7-8EEBA0E4EFCA", "E:\\bigfile.pdf");
            //service.DownloadFileByMultipart("yiho", "13620E17-976A-4FFF-92E7-8EEBA0E4EFCA", "E:\\bigfile1.pdf");
            Console.ReadKey();
        }
    }
}
