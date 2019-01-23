using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using Shitou.Framework.Demo.Dao;
using Shitou.Framework.Demo.Model.Books;
using Shitou.Framework.Demo.Service;
using Shitou.Framework.Demo.Utils;
using Shitou.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace SyncBooksTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();
            //注入
            services.AddMySql(new MySqlConnection(Configuration.GetConnectionString("Mysql")));
            //add bussiness service
            services.AddBusinessService();
            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IBaseService baseService = serviceProvider.GetService<IBaseService>();

            string connectionStr = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionStr);
            IMongoDatabase mongoDatabase = client.GetDatabase("books");

            var collection = mongoDatabase.GetCollection<Mongo_BookInfo>("book_info");
            var filter = new BsonDocument();
            List<Mongo_BookInfo> mongo_booklist = collection.Find(filter).ToList();
            try
            {
                DateTime nowTime = DateTime.Now;
                //提取分类
                List<string> bookTypeList = mongo_booklist.Select(a => a.book_type_name).Distinct().ToList();
                List<BookTypeInfo> bookTypeInfos = (from m in bookTypeList
                                                    select new BookTypeInfo
                                                    {
                                                        ID = StringUtils.GenerateUniqueID(),
                                                        BookTypeName = m
                                                    }).ToList();
                Dictionary<string, string> dicType = bookTypeInfos.ToDictionary(k => k.BookTypeName, k => k.ID);
                //提取书
                List<BookInfo> bookInfos = (from m in mongo_booklist
                                            select new BookInfo
                                            {
                                                ID = StringUtils.GenerateUniqueID(),
                                                BookName = m.book_name,
                                                Author = m.book_author,
                                                BookTypeID = dicType[m.book_type_name],
                                                ImageUrl = m.book_image_url,
                                                Summary = m.book_summary,
                                                WordCount = m.book_word_count,
                                                SourceSiteName = m.source_site_name,
                                                SourceSiteUrl = m.source_site_url,
                                                CreateTime = nowTime
                                            }).ToList();
                //baseService.Insert(bookTypeInfos);
                //baseService.Insert(bookInfos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register The Service Layer
        /// </summary>
        /// <param name="services"></param>
        public static void AddBusinessService(this IServiceCollection services)
        {
            //sevice
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IGoodsService, GoodsService>();

            //dao
            services.AddTransient<ISystemDao, SystemDao>();
            services.AddTransient<IGoodsDao, GoodsDao>();
        }
    }

    public class Mongo_BookInfo
    {
        public ObjectId _id { get; set; }
        public string book_id { get; set; }
        public string book_name { get; set; }
        public string book_author { get; set; }
        public string book_type_name { get; set; }
        public string book_image_url { get; set; }
        public string book_summary { get; set; }
        public string book_summary_url { get; set; }
        public string book_word_count { get; set; }
        public string book_chapter_url { get; set; }
        public string source_site_name { get; set; }
        public string source_site_url { get; set; }
    }
}
