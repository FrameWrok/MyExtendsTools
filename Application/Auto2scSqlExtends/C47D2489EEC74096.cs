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
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlString C47D2489EEC74096A4373A2292985050(string str, string hostname)
        {
            ///身份证号加密
            return new SqlString(DES3.IdCardEncode(str));
        }
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlString C47D2489EEC74096BC12822AF8D5F3F3(string str, string hostname)//, string logconnectionstring)
        {
            //EencrypinfoDecodeLogDal.AddDecodeLog(hostname, logconnectionstring, 2, str);
            ///身份证号解密
            return new SqlString(DES3.IdCardDecode(str));
        }
    }
}
