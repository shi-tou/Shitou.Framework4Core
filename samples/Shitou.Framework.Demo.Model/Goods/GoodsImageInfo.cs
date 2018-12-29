using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.Model
{
    /// <summary>
    /// 商品图片
    /// </summary>
    [Table("t_goods_image")]
    public class GoodsImageInfo
    {
        /// <summary>
        /// 类别Id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public string GoodsID { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public bool IsTop { get; set; }
    }
}
