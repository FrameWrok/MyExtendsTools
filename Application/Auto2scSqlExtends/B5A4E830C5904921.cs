using Auto2scSqlExtends.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace Auto2scSqlExtends
{
    /// <summary>
    /// 手机号加解密
    /// </summary>
    public static class B5A4E830C5904921
    {
        public static readonly SqlString DefaultNull = new SqlString("");

        [Microsoft.SqlServer.Server.SqlFunction(DataAccess = Microsoft.SqlServer.Server.DataAccessKind.Read)]
        public static SqlString Encode(string str, string hostname, string token)
        {
            // 手机号加密
            return new SqlString(DES3.MobileEncode(str, token));
        }
        [Microsoft.SqlServer.Server.SqlFunction(DataAccess = Microsoft.SqlServer.Server.DataAccessKind.Read)]
        public static SqlString Decode(string str, string hostname, string token)//, string logconnectionstring)
        {           
            // 手机号解密            
            return new SqlString(DES3.MobileDecode(str, token));
        }
    }
}
