using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.Model.Books
{
    [Table("t_books")]
    public class BookInfo
    {
        public string ID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string BookTypeID { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public string WordCount { get; set; }
        public string SourceSiteName { get; set; }
        public string SourceSiteUrl { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
