using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.Model.Books
{
    [Table("t_bookstype")]
    public class BookTypeInfo
    {
        public string ID { get; set; }
        public string BookTypeName { get; set; }
    }
}
