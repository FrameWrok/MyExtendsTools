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
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlString E09D2380503C4238E31EEB62C293DF4(string str, string hostname)
        {
            ///车牌号加密
            return new SqlString(DES3.PlateNumEncode(str));
        }
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlString E09D2380503C42383C73F1301ADC9BA(string str, string hostname)//, string logconnectionstring)
        {
            //EencrypinfoDecodeLogDal.AddDecodeLog(hostname, logconnectionstring, 3, str);
            ///车牌号解密
            return new SqlString(DES3.PlateNumDecode(str));
        }
    }
}
