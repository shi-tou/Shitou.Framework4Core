using Shitou.Framework.Demo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Shitou.Framework.Demo.DataContract.Request;
using Shitou.Framework.ORM;
using Shitou.Framework.Demo.DataContract.Response;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shitou.Framework.Demo.Model;
using Shitou.Framework.Demo.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Shitou.Framework.Demo.Mvc.Controllers
{
    [Authorize]
    public class GoodsController : BaseController
    {
        public IGoodsService GoodsService { get; set; }
        public GoodsController(IGoodsService goodsService)
        {
            GoodsService = goodsService;
        }

        #region 商品管理
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult GoodsList(GetGoodsListRequest request)
        {
            List<GoodsTypeInfo> typeList = GoodsService.GetGoodsTypeList();
            ViewData["GoodsType"] = new SelectList(typeList, "ID", "GoodsTypeName");
            PagedList<GetGoodsListResponse> list = GoodsService.GetGoodsList(request);
            return View(list);
        }
        /// <summary>
        /// 商品添加
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsAdd(string id)
        {
            List<GoodsTypeInfo> typeList = GoodsService.GetGoodsTypeList();
            ViewData["GoodsType"] = new SelectList(typeList, "ID", "GoodsTypeName");
            GoodsInfo info = GoodsService.GetModel<GoodsInfo>(new { ID = id });
            return View(info ?? new GoodsInfo());
        }
        /// <summary>
        /// 商品添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GoodsAdd(GoodsInfo info)
        {
            if (string.IsNullOrEmpty(info.ID))
            {
                info.ID = StringUtils.GenerateUniqueID();
                info.CreateBy = LoginUserInfo.ID;
                info.CreateTime = DateTime.Now;

                if (GoodsService.Insert(info))
                {
                    Result.IsOk = true;
                    Result.Msg = "添加成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "添加失败";
                }
            }
            else
            {
                if (GoodsService.Update(info))
                {
                    Result.IsOk = true;
                    Result.Msg = "更新成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "更新失败";
                }
            }
            return Json(Result);
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult GoodsDelete(string id)
        {
            if (GoodsService.Delete<GoodsInfo>(new { ID = id }))
            {
                Result.IsOk = true;
                Result.Msg = "删除成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "删除失败";
            }
            return Json(Result);
        }
        #endregion

        #region 商品类别管理
        /// <summary>
        /// 商品类别列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult GoodsTypeList()
        {
            List<GoodsTypeInfo> typeList = GoodsService.GetGoodsTypeList();
            ViewData["GoodsType"] = new SelectList(typeList, "ID", "GoodsTypeName");
            return View(typeList);
        }
        /// <summary>
        /// 商品类别添加
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsTypeAdd(string id)
        {
            List<GoodsTypeInfo> typeList = GoodsService.GetGoodsTypeList();
            ViewData["GoodsType"] = new SelectList(typeList, "ID", "GoodsTypeName");
            GoodsTypeInfo info = GoodsService.GetModel<GoodsTypeInfo>(new { ID = id });
            return View(info ?? new GoodsTypeInfo());
        }
        /// <summary>
        /// 商品类别添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GoodsTypeAdd(GoodsTypeInfo info)
        {
            if (string.IsNullOrEmpty(info.ParentID))
            {
                info.ParentID = "0";
            }
            if (string.IsNullOrEmpty(info.ID))
            {
                info.ID = StringUtils.GenerateUniqueID();
                if (GoodsService.Insert(info))
                {
                    Result.IsOk = true;
                    Result.Msg = "添加成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "添加失败";
                }
            }
            else
            {
                if (GoodsService.Update(info))
                {
                    Result.IsOk = true;
                    Result.Msg = "更新成功";
                }
                else
                {
                    Result.IsOk = false;
                    Result.Msg = "更新失败";
                }
            }
            return Json(Result);
        }
        /// <summary>
        /// 删除商品类别
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult GoodsTypeDelete(string id)
        {
            if (GoodsService.GetCount<GoodsTypeInfo>(new { ParentID = id }) > 0)
            {
                Result.IsOk = false;
                Result.Msg = "不可删除,该类别下包含子类别";
                return Json(Result);
            }
            if (GoodsService.GetCount<GoodsInfo>(new { GoodsTypeID = id }) > 0)
            {
                Result.IsOk = false;
                Result.Msg = "不可删除,该类别有关联的商品";
                return Json(Result);
            }
            if (GoodsService.Delete<GoodsTypeInfo>(new { ID = id }))
            {
                Result.IsOk = true;
                Result.Msg = "删除成功";
            }
            else
            {
                Result.IsOk = false;
                Result.Msg = "删除失败";
            }
            return Json(Result);
        }
        #endregion

        
    }
}