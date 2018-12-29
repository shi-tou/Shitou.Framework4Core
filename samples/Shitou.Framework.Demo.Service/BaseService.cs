using Shitou.Framework.Demo.DataContract;
using Shitou.Framework.ORM;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Shitou.Framework.Demo.Service
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseService : IBaseService
    {
        private IAdoTemplate adoTemplate { get; set; }
        private ILogger logger { get; set; }
        public BaseService(IAdoTemplate adoTemplate, ILogger<BaseService> logger = null)
        {
            this.adoTemplate = adoTemplate;
            this.logger = logger;
        }

        #region ---insert---
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert<T>(T t)
        {
            try
            {
                return adoTemplate.Insert<T>(t) > 0;
            }
            catch(Exception ex)
            {
                logger?.LogError(ex, "BaseService.Insert->{0}", JsonConvert.SerializeObject(t));
                return false;
            }
            
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert<T>(List<T> listT)
        {
            try
            {
                return adoTemplate.Insert<T>(listT) > 0;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.Insert->{0}", JsonConvert.SerializeObject(listT));
                return false;
            }
        }
        #endregion

        #region ---update---
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<T>(T t)
        {
            
            try
            {
                return adoTemplate.Update<T>(t) > 0;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.Update->{0}:{1}", typeof(T).Name, JsonConvert.SerializeObject(t));
                return false;
            }
        }
        #endregion

        #region ---delete---

        /// <summary>
        /// 删除（物理删除）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Delete<T>(string columnName, object value)
        {
            
            try
            {
                return adoTemplate.Delete<T>(columnName, value) > 0;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.Delete->{0}:{1}={2}", typeof(T).Name, columnName, value);
                return false;
            }
        }
        /// <summary>
        /// 删除（物理删除）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Delete<T>(Hashtable hs)
        {
            try
            {
                return adoTemplate.Delete<T>(hs) > 0;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.Delete->{0}:{1}", typeof(T).Name, JsonConvert.SerializeObject(hs));
                return false;
            }
        }
        #endregion

        #region ---GetModel---

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetModel<T>(string columnName, object value)
        {
            try
            {
                return adoTemplate.GetModel<T>(columnName, value);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetModel->{0}:{1}={2}", typeof(T).Name, columnName, value);
                return default(T);
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetModel<T>(Hashtable hs)
        {
            try
            {
                return adoTemplate.GetModel<T>(hs);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetModel->{0}:{1}", typeof(T).Name, JsonConvert.SerializeObject(hs));
                return default(T);
            }
        }
        #endregion

        #region
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int GetCount<T>(string columnName, object value)
        {
            try
            {
                return adoTemplate.GetCount<T>(columnName, value);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetList->{0}:{1}={2}", typeof(T).Name, columnName, value);
                return 0;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int GetCount<T>(Hashtable hs)
        {
            try
            {
                return adoTemplate.GetCount<T>(hs);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetList->{0}:{1}", typeof(T).Name, JsonConvert.SerializeObject(hs));
                return 0;
            }
        }
        #endregion

        #region ---GetList---
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetList<T>(string columnName, object value)
        {
            try
            {
                return adoTemplate.GetList<T>(columnName, value);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetList->{0}:{1}={2}", typeof(T).Name, columnName, value);
                return default(List<T>);
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetList<T>()
        {
            try
            {
                return adoTemplate.GetList<T>();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetList->{0}", typeof(T).Name);
                return default(List<T>);
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Hashtable hs)
        {
            try
            {
                return adoTemplate.GetList<T>(hs);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetList->{0}:{1}", typeof(T).Name, JsonConvert.SerializeObject(hs));
                return default(List<T>);
            }
        }
        #endregion

        #region ---GetPageList---
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public Pager<T> GetPageList<T>(PageRequest request)
        {
            try
            {
                return adoTemplate.GetPagedList<T>(request.PageIndex, request.PageSize, request.OrderBy);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetPageList->{0}:{1}", typeof(T).Name, JsonConvert.SerializeObject(request));
                return default(Pager<T>);
            }
        }
        #endregion
    }
}
