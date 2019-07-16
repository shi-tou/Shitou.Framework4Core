/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： 4.0.30319.42000
*公司名称：石头小神
*命名空间：Yiho.Framework.Dao
*文件名：  IYihoDao
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-6-8 17:17:43
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-6-8 17:17:43
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using Shitou.Framework.Demo.DataContract.Request;
using Shitou.Framework.Demo.DataContract.Response;
using Shitou.Framework.Demo.Model;
using Shitou.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.Repository
{
    public interface ISystemRepository:IBaseRepository
    {
        #region 用户管理
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagedList<GetUserListResponse> GetUserList(GetUserListRequest request);
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagedList<GetRoleListResponse> GetRoleList(GetRoleListRequest request);
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<AuthInfo> GetUserAuth(string userID);
        #endregion

    }
}
