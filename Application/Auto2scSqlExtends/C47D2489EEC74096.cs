using Auto2scSqlExtends.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace Auto2scSqlExtends
{
    /// <summary>
    /// 身份证号加解密
    /// </summary>
    public static class C47D2489EEC74096
    {
        [Microsoft.SqlServer.Server.SqlFunction(DataAccess = Microsoft.SqlServer.Server.DataAccessKind.Read)]
        public static SqlString Encode(string str, string hostname, string token)
        {
            ///身份证号加密
            return new SqlString(DES3.IdCardEncode(str, token));
        }
        [Microsoft.SqlServer.Server.SqlFunction(DataAccess = Microsoft.SqlServer.Server.DataAccessKind.Read)]
        public static SqlString Decode(string str, string hostname, string token)//, string logconnectionstring)
        {
            //EencrypinfoDecodeLogDal.AddDecodeLog(hostname, logconnectionstring, 2, str);
            ///身份证号解密
            return new SqlString(DES3.IdCardDecode(str, token));
        }
    }
}
