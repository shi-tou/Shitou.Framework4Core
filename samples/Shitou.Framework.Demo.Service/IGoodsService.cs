
using Shitou.Framework.Demo.DataContract.Request;
using Shitou.Framework.Demo.DataContract.Response;
using Shitou.Framework.Demo.Model;
using Shitou.Framework.ORM;
using System.Collections.Generic;

namespace Shitou.Framework.Demo.Service
{
    /// <summary>
    /// 业务基础层
    /// </summary>
    public interface IGoodsService : IBaseService
    {
        #region 商品管理
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        PagedList<GetGoodsListResponse> GetGoodsList(GetGoodsListRequest request);
        #endregion

        /// <summary>
        /// 商品类别
        /// </summary>
        /// <returns></returns>
        List<GoodsTypeInfo> GetGoodsTypeList();
    }
}
