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
        /// 按主键更新
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
        /// <summary>
        /// 按指定条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<T>(T t,object param)
        {
            try
            {
                return adoTemplate.Update<T>(t, param) > 0;
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
        /// 按指定条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Delete<T>()
        {
            try
            {
                return adoTemplate.Delete<T>() > 0;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.Delete->{0}", typeof(T).Name);
                return false;
            }
        }

        /// <summary>
        /// 按指定条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Delete<T>(object param)
        {
            try
            {
                return adoTemplate.Delete<T>(param) > 0;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.Delete->{0}:{1}", typeof(T).Name, JsonConvert.SerializeObject(param));
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
        public T GetModel<T>(object param)
        {
            try
            {
                return adoTemplate.GetModel<T>(param);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetModel->{0}:{1}", typeof(T).Name, JsonConvert.SerializeObject(param));
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
        /// 全表查询
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
        /// 按指定条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetList<T>(object param)
        {
            try
            {
                return adoTemplate.GetList<T>(param);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "BaseService.GetList->{0}:{1}", typeof(T).Name, JsonConvert.SerializeObject(param));
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
