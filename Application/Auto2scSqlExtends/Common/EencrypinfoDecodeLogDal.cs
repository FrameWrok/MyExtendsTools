using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace Auto2scSqlExtends.Common
{
    public static class EencrypinfoDecodeLogDal
    {
        public static void AddDecodeLog(string hostname, string connectionstring, int encodetype, string encodevalue)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO EencrypinfoDecodeLog(hostname,encodetype,encodevalue)VALUES(@hostname,@encodetype,@encodevalue)";
                    List<SqlParameter> parameters = new List<SqlParameter>() {
                        new SqlParameter("@hostname",hostname),
                        new SqlParameter("@encodetype",encodetype),
                        new SqlParameter("@encodevalue",encodevalue)
                     };
                    command.Parameters.AddRange(parameters.ToArray());
                    command.ExecuteNonQuery();

                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

            }
        }
    }
}
