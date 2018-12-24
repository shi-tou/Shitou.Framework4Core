
using Shitou.Framework.Demo.Dao;
using Shitou.Framework.Demo.DataContract.Base;
using Shitou.Framework.Demo.DataContract.Request;
using Shitou.Framework.Demo.DataContract.Response;
using Shitou.Framework.Demo.Model;
using Shitou.Framework.Demo.Utils;
using Shitou.Framework.ORM;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Shitou.Framework.Demo.Service
{
    /// <summary>
    /// 业务基础层
    /// </summary>
    public class SystemService : BaseService, ISystemService
    {
        public IAdoTemplate AdoTemplate { get; set; }
        public ISystemDao SystemDao { get; set;}
        public ILogger Logger { get; set; }
        public SystemService(ISystemDao systemDao, IAdoTemplate adoTemplate, ILogger<SystemService> logger)
            : base(adoTemplate, logger)
        {
            SystemDao = systemDao;
            AdoTemplate = adoTemplate;
            Logger = logger;
        }

        #region 用户管理

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public UserLoginResponse UserLogin(UserLoginRequest request)
        {
            UserLoginResponse response = new UserLoginResponse();
            UserInfo info = GetModel<UserInfo>("UserName", request.UserName);
            if (info == null)
            {
                response.Result = RT.User_NotExist_UserName;
                return response;
            }
            if (request.Password != RSADEncrypt.Decrypt(info.Password))
            {
                response.Result = RT.User_Error_Password;
                return response;
            }
            //权限
            if (info.IsAdmin)
            {
                response.AuthList = GetList<AuthInfo>();
            }
            else
            {
                response.AuthList = GetUserAuth(info.ID);
            }
            //登录成功
            response.Result = RT.Success;
            response.LoginUserInfo = info;
            return response;
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public Pager<GetUserListResponse> GetUserList(GetUserListRequest request)
        {
            return SystemDao.GetUserList(request);
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public Pager<GetRoleListResponse> GetRoleList(GetRoleListRequest request)
        {
            return SystemDao.GetRoleList(request);
        }
        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<AuthInfo> GetUserAuth(string userID)
        {
            return SystemDao.GetUserAuth(userID);
        }
        #endregion

    }
}
