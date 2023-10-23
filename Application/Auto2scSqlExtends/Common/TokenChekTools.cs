using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Auto2scSqlExtends.Common
{
    internal static class TokenChekTools
    {
        private const string TOKEN_KEY = "libinglonghengshui";
        internal static bool IsParseToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            DateTime t;
            string token_decode = DES3.DES3CBCDecode(token, TOKEN_KEY, null);
            if (string.IsNullOrEmpty(token_decode)) return false;
            if (DateTime.TryParse(token_decode, out t))
            {
                if (t > DateTime.Now) return true;
            }
            return false;
        }
        internal static bool IsParseHost(string host, string dbconnectionstring)
        {
            using (SqlConnection conn = new SqlConnection(dbconnectionstring))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = "SELECT TOP 1 edca.cid FROM EencrypinfoDecodeCpuAuth AS edca WITH ( NOLOCK ) WHERE edca.hostname = @hostname AND edca.is_del = 0";
                    List<SqlParameter> parameters = new List<SqlParameter>() {
                        new SqlParameter("@hostname",host)
                     };
                    command.Parameters.AddRange(parameters.ToArray());
                    object o = command.ExecuteScalar();
                    if (o != null)
                    {
                        if (int.TryParse(o.ToString(), out int id))
                            if (id > 0) return true;
                    }
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

            }
            return false;
        }
    }
}
