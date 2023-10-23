using Auto2scSqlExtends.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace Auto2scSqlExtends
{
    public static class E09D2380503C4238
    {
        [Microsoft.SqlServer.Server.SqlFunction(DataAccess = Microsoft.SqlServer.Server.DataAccessKind.Read)]
        public static SqlString Encode(string str, string hostname, string token)
        {
            ///车牌号加密
            return new SqlString(DES3.PlateNumEncode(str, token));
        }
        [Microsoft.SqlServer.Server.SqlFunction(DataAccess = Microsoft.SqlServer.Server.DataAccessKind.Read)]
        public static SqlString Decode(string str, string hostname, string token)//, string logconnectionstring)
        {
            //EencrypinfoDecodeLogDal.AddDecodeLog(hostname, logconnectionstring, 3, str);
            ///车牌号解密
            return new SqlString(DES3.PlateNumDecode(str, token));
        }
    }
}
