using System.Text;
using Dapper;
using Shitou.Framework.ORM;
using Shitou.Framework.Demo.DataContract.Response;
using Shitou.Framework.Demo.DataContract.Request;

namespace Shitou.Framework.Demo.Repository
{
    public class GoodsRepository : SQLServerRepository, IGoodsRepository
    {
        public GoodsRepository(string connString)
           : base(connString) { }

        #region 商品管理
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedList<GetGoodsListResponse> GetGoodsList(GetGoodsListRequest request)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"select a.*,b.GoodsTypeName from T_Goods a
                            left join T_Goods_Type b on b.ID=a.GoodsTypeID
                            where 1=1 ");
            var param = new DynamicParameters();
            if (!string.IsNullOrEmpty(request.GoodsName))
            {
                sbSql.Append(" and a.GoodsName like ?GoodsName");
                param.Add("GoodsName", "%" + request.GoodsName + "%");
            }
            if (!string.IsNullOrEmpty(request.GoodsTypeID))
            {
                sbSql.Append(" and a.GoodsTypeID = ?GoodsTypeID");
                param.Add("GoodsTypeID", request.GoodsTypeID);
            }
            request.OrderBy = "a.CreateTime desc";
            return GetPagedList<GetGoodsListResponse>(sbSql.ToString(), param, request.PageIndex, request.PageSize, request.OrderBy);
        }

        #endregion

    }
}
