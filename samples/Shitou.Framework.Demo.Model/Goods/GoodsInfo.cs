using Shitou.Framework.ORM.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.Model
{
    /// <summary>
    /// 商品
    /// </summary>
    [Table("t_goods")]
    public class GoodsInfo
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 类别id
        /// </summary>
        public string GoodsTypeID { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriginalPrice { get; set; }
        /// <summary>
        /// 当前价
        /// </summary>
        public decimal PresentPrice { get; set; }
        /// <summary>
        /// 券价格
        /// </summary>
        public decimal CouponPrice { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string SummaryText { get; set; }
        /// <summary>
        /// 图文描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 口令
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 平台 1-淘宝 2-京东
        /// </summary>
        public int PlatformType { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
