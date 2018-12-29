using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.DataContract.Request
{
    public class GetGoodsListRequest : PageRequest
    {
        public string GoodsName { get; set; }

        public string GoodsTypeID { get; set; }
    }
}
