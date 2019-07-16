using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shitou.Framework.Demo.Application.Models;
using Shitou.Framework.Demo.DataContract;
using Shitou.Framework.Demo.DataContract.Base;
using Shitou.Framework.Demo.Model;
using Shitou.Framework.Demo.Service;
using Shitou.Framework.Demo.Utils;
using Shitou.Framework.ORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shitou.Framework.Demo.Mvc.Controllers
{
    /// <summary>
    /// 模板管理
    /// </summary>
    public class TemplateController : BaseController
    {
        private IBaseService _baseService;
        public TemplateController(IBaseService baseService)
        {
            _baseService = baseService;
        }
        /// <summary>
        /// 表单模板
        /// </summary>
        /// <returns></returns>
        public IActionResult TableTemplateList(PageRequest request)
        {
            PagedList<TableTemplateInfo> list = _baseService.GetPageList<TableTemplateInfo>(request);
            return View(list);
        }
        /// <summary>
        /// 表单模板添加
        /// </summary>
        /// <returns></returns>
        public ActionResult TableTemplateAdd(string id)
        {
            Dictionary<int, string> dic = EnumUtils.GetDescList<TableTemplateType>();
            List<TextValue> list = new List<TextValue>();
            foreach (var key in dic.Keys)
            {
                list.Add(new TextValue
                {
                    Text = dic[key],
                    Value = key
                });
            }
            ViewData["TemplateTypeList"] = new SelectList(list, "Value", "Text");
            TableTemplateInfo info = _baseService.GetModel<TableTemplateInfo>(new { ID = id });
            return View(info ?? new TableTemplateInfo());
        }
        /// <summary>
        /// 表单模板添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TableTemplateAdd(TableTemplateInfo info)
        {
            if (string.IsNullOrEmpty(info.ID))
            {
                info.ID = StringUtils.GenerateUniqueID();
                info.CreateBy = LoginUserInfo.ID;
                info.CreateTime = DateTime.Now;

                if (_baseService.Insert(info))
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
                info.UpdateBy = LoginUserInfo.ID;
                info.CreateTime = DateTime.Now;
                if (_baseService.Update(info))
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
        /// 表单字段
        /// </summary>
        /// <returns></returns>
        public IActionResult TableFieldList(string id)
        {
            ViewBag.TableTemplateID = id;
            List<TableFieldInfo> list = _baseService.GetList<TableFieldInfo>(new { TableTemplateID = id });
            return View(list);
        }

        /// <summary>
        /// 表单字段添加
        /// </summary>
        /// <returns></returns>
        public ActionResult TableFieldAdd(string templateID, string id)
        {
            ViewBag.TableTemplateID = templateID;
            Dictionary<int, string> dic = EnumUtils.GetDescList<TableFieldType>();
            List<TextValue> list = new List<TextValue>();
            foreach (var key in dic.Keys)
            {
                list.Add(new TextValue
                {
                    Text = dic[key],
                    Value = key
                });
            }
            ViewData["FieldTypeList"] = new SelectList(list, "Value", "Text");
            TableFieldInfo info = _baseService.GetModel<TableFieldInfo>(new { ID = id });
            return View(info ?? new TableFieldInfo());
        }
        /// <summary>
        /// 表单字段添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TableFieldAdd(TableFieldInfo info)
        {
            if (string.IsNullOrEmpty(info.ID))
            {
                info.ID = StringUtils.GenerateUniqueID();
                info.CreateBy = LoginUserInfo.ID;
                info.CreateTime = DateTime.Now;

                if (_baseService.Insert(info))
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
                info.UpdateBy = LoginUserInfo.ID;
                info.CreateTime = DateTime.Now;
                if (_baseService.Update(info))
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
        /// 表单预览
        /// </summary>
        /// <returns></returns>
        public ActionResult TablePreview(string id)
        {
            List<TableFieldInfo> list = _baseService.GetList<TableFieldInfo>(new { TableTemplateID = id });
            return View(list ?? new List<TableFieldInfo>());
        }
    }
}