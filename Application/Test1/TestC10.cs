using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Test1
{
    internal class TestC10
    {
        public DataTable GetSlowSql()
        {
            string connectionstring = "Server=db-dbaops1-sr0-3308s.mysql.db.corpautohome.com;Port=3308;Database=ms_slow_usedcar; User=ms_slow_usedcar_r;Password=yV_mM6ubcYE2nsnI;";
            //string sql = @"SET SESSION TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;
            //                SELECT* FROM view_mssql_slow_sql_statistics_usedcar WHERE last_execution_time > '2022-03-08';
            //                COMMIT";
            string sql = @" SELECT * FROM view_mssql_slow_sql_statistics_usedcar WHERE last_execution_time > @begintime;";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter() { ParameterName = "@begintime", MySqlDbType = MySqlDbType.DateTime, Value = "2022-03-08" });
            DataSet ds = MySqlHelper.ExecuteDataset(connectionstring, sql,parameters.ToArray());
            return ds.Tables[0];

        }
    }

}
