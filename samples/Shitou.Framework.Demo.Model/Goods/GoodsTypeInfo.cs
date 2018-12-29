using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.Model
{
    /// <summary>
    /// 商品类别
    /// </summary>
    [Table("t_goods_type")]
    public class GoodsTypeInfo
    {
        /// <summary>
        /// 类别Id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 类别名
        /// </summary>
        public string GoodsTypeName { get; set; }
        /// <summary>
        /// 类别id
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

    }
}
