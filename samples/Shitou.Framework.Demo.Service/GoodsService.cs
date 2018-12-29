
using Shitou.Framework.Demo.Dao;
using Shitou.Framework.Demo.DataContract.Request;
using Shitou.Framework.Demo.DataContract.Response;
using Shitou.Framework.ORM;
using Microsoft.Extensions.Logging;
using Shitou.Framework.Demo.Model;
using System.Collections.Generic;
using System.Linq;

namespace Shitou.Framework.Demo.Service
{
    /// <summary>
    /// 业务基础层
    /// </summary>
    public class GoodsService : BaseService, IGoodsService
    {
        public IGoodsDao GoodsDao { get; set;}
        public ILogger Logger { get; set; }
        public GoodsService(IGoodsDao goodsDao, IAdoTemplate adoTemplate, ILogger<SystemService> logger)
            : base(adoTemplate, logger)
        {
            GoodsDao = goodsDao;
            Logger = logger;
        }

        #region 商品管理

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public Pager<GetGoodsListResponse> GetGoodsList(GetGoodsListRequest request)
        {
            return GoodsDao.GetGoodsList(request);
        }

        #endregion

        #region 商品类别
        /// <summary>
        /// 商品类别
        /// </summary>
        /// <returns></returns>
        public List<GoodsTypeInfo> GetGoodsTypeList()
        {
            List<GoodsTypeInfo> list = GetList<GoodsTypeInfo>();
            int index = 0;
            List<GoodsTypeInfo> typeList = new List<GoodsTypeInfo>();
            CreateGoodsTypeData(list, "0", ref index, ref typeList);
            return typeList;
        }

        /// <summary>
        /// 构造权限下拉框数据
        /// </summary>
        /// <returns></returns>
        private void CreateGoodsTypeData(List<GoodsTypeInfo> types, string parentID, ref int index, ref List<GoodsTypeInfo> typeList)
        {
            var subType = types.Where(a => a.ParentID == parentID);
            foreach (var item in subType)
            {
                item.GoodsTypeName = GetBlank(index) + item.GoodsTypeName;

                typeList.Add(item);
                index++;
                CreateGoodsTypeData(types, item.ID, ref index, ref typeList);
                index--;
            }
        }
        private string GetBlank(int index)
        {
            string blank = "";
            if (index == 0)
                return blank;
            else
            {
                blank += "|";
                for (int i = 0; i < index; i++)
                {
                    blank += "---";
                }
            }
            return blank;
        }
        #endregion
    }
}
