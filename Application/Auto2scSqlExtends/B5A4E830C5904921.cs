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
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlString B5A4E830C5904921B4A5C0A0F169E43C(string str, string hostname)
        {
            // 手机号加密
            return new SqlString(DES3.MobileEncode(str));
        }
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlString B5A4E830C5904921A47B7D521C69A676(string str, string hostname)//, string logconnectionstring)
        {
            //EencrypinfoDecodeLogDal.AddDecodeLog(hostname, logconnectionstring, 1, str);
            // 手机号解密
            return new SqlString(DES3.MobileDecode(str));
        }
    }
}
