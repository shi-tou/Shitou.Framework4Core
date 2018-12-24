using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shitou.Framework.ORM
{
    /// <summary>
    /// 数据库连接对象委托(通过委托可实现不同用户登录，连接不同数据库)
    /// </summary>
    /// <param name="connName"></param>
    /// <returns></returns>
    public delegate IDbConnection DelegateDbConnectionSetting(string connName);
    /// <summary>
    /// 数据库连接配置信息
    /// </summary>
    public class DbConnectionSetting
    {
        public static DelegateDbConnectionSetting DbConnection { get; set; }
    }

    /// <summary>
    /// 数据库连接名称(可做为数据库连接缓存key,如cookie,session,redis等)
    /// </summary>
    public class DbConnectionNameConst
    {
        public const string MySql = "Mysql";
        public const string SqlServer = "SqlServer";
        public const string Sqlite = "Sqlite";
    }

}
