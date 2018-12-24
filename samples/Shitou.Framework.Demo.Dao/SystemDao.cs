
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Dapper;

using Shitou.Framework.ORM;
using Shitou.Framework.Demo.DataContract.Response;
using Shitou.Framework.Demo.DataContract.Request;
using Shitou.Framework.Demo.Model;

namespace Shitou.Framework.Demo.Dao
{
    public class SystemDao : ISystemDao
    {
        public IAdoTemplate AdoTemplate { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnection">数据库链接</param>
        /// <param name="sqlGenerator"> sq语句构造器</param>
        public SystemDao(IAdoTemplate adoTemplate)
        {
            AdoTemplate = adoTemplate;
        }

        #region 用户管理
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Pager<GetUserListResponse> GetUserList(GetUserListRequest request)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"select a.*,b.RoleName,c.RealName as CreateUser from T_User a
                            left join T_Role b on b.ID=a.RoleID
                            left join T_User c on c.ID=a.CreateBy
                            where 1=1 ");
            var param = new DynamicParameters();

           
            if (!string.IsNullOrEmpty(request.UserName))
            {
                sbSql.Append(" and a.UserName like ?UserName");
                param.Add("UserName", "%" + request.UserName + "%");
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                sbSql.Append(" and a.RealName like ?RealName");
                param.Add("RealName", "%" + request.Name + "%");
            }
            request.OrderBy = "a.CreateTime desc";
            return AdoTemplate.GetPagedList<GetUserListResponse>(sbSql.ToString(), param, request.PageIndex, request.PageSize, request.OrderBy);
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Pager<GetRoleListResponse> GetRoleList(GetRoleListRequest request)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"select a.*,b.RealName as CreateUser from T_Role a
                            left join T_User b on b.ID=a.CreateBy");
            var param = new DynamicParameters();
            
            if (!string.IsNullOrEmpty(request.RoleName))
            {
                sbSql.Append(" and RoleName like ?RoleName");
                param.Add("RoleName", "%" + request.RoleName + "%");
            }
            request.OrderBy = "a.CreateTime desc";
            return AdoTemplate.GetPagedList<GetRoleListResponse>(sbSql.ToString(), param, request.PageIndex, request.PageSize, request.OrderBy);
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<AuthInfo> GetUserAuth(string userID)
        {
            StringBuilder sbSql = new StringBuilder();
            var param = new DynamicParameters();
            sbSql.Append(@"select c.* from T_Role_Auth a
                            inner join T_User b on b.RoleID=a.RoleID
                            inner join T_Auth c on c.ID=a.AuthID
                            where b.ID=@UserID");
            param.Add("UserID", userID);
            return AdoTemplate.GetList<AuthInfo, DynamicParameters>(sbSql.ToString(), param);
        }
        #endregion

       
    }
}
