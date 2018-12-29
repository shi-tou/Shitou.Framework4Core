using Shitou.Framework.Demo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.DataContract.Response
{
    public class GetGoodsListResponse : GoodsInfo
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string GoodsTypeName { get; set; }
    }
}
