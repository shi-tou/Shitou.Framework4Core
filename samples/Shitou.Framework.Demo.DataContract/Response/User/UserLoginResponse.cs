using Shitou.Framework.Demo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.DataContract.Response
{
    public class UserLoginResponse
    {
        /// <summary>
        ///  登录结果
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 登录信息
        /// </summary>
        public UserInfo LoginUserInfo { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public List<AuthInfo> AuthList { get; set; }
    }
}
