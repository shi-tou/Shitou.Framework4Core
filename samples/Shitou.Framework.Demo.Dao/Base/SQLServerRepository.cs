using Shitou.Framework.ORM;
using Shitou.Framework.ORM.Generator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Shitou.Framework.Demo.Repository
{
    public class SQLServerRepository : BaseRepository, IBaseRepository
    {
        private string _connString { get; set; }
        public SQLServerRepository(string connString)
            : base(connString, new SqlServerGenerator())
        {
            _connString = connString;
        }
        /// <summary>
        /// OpenConnection for SqlServer
        /// </summary>
        /// <returns></returns>
        public override IDbConnection OpenConnection()
        {
            var conn = new SqlConnection(_connString);
            conn.Open();
            return conn;            
        }
    }
}
