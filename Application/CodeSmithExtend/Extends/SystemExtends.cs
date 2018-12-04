using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
   public static partial class SystemExtends
    {
        public static Type typeInt16 = typeof(System.Int16);
        public static Type typeInt32 = typeof(System.Int32);
        public static Type typeInt64 = typeof(System.Int64);
        public static Type typeDateTime = typeof(System.DateTime);
        public static Type typeString = typeof(System.String);
        public static Type typestring = typeof(string);
        public static Type typeUInt16 = typeof(System.UInt16);
        public static Type typeUInt32 = typeof(System.UInt32);
        public static Type typeUInt64 = typeof(System.UInt64);
        public static Type typeDecimal = typeof(System.Decimal);
        public static Type typeDouble = typeof(System.Double);
        public static Type typeshort = typeof(short);
        public static Type typeushort = typeof(ushort);
        public static Type typelong = typeof(long);
        public static Type typeulong = typeof(ulong);
        /// <summary>
        /// 由C#的type获取 对应的DbType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static System.Data.DbType GetDbType(this Type type)
        {
            if (type == typeInt16) return Data.DbType.Int16;
            if (type == typeUInt16) return Data.DbType.UInt16;
            if (type == typeInt32) return Data.DbType.Int32;
            if (type == typeUInt32) return Data.DbType.UInt32;
            if (type == typelong) return Data.DbType.Int64;
            if (type == typeInt64) return Data.DbType.Int64;
            if (type == typeUInt64) return Data.DbType.UInt64;
            if (type == typeulong) return Data.DbType.UInt64;
            if (type == typeDecimal) return Data.DbType.Decimal;
            if (type == typeDouble) return Data.DbType.Double;
            if (type == typestring) return Data.DbType.String;
            if (type == typeString) return Data.DbType.String;
            if (type == typeDateTime) return Data.DbType.DateTime;
            if (type == typeshort) return Data.DbType.SByte;
            if (type == typeushort) return Data.DbType.Byte;
            return Data.DbType.String;
        }

        /// <summary>
        /// 由DbType获取对应的C# 的 Type
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static Type GetCSharpType(this System.Data.DbType dbtype)
        {
            switch (dbtype)
            {
                case Data.DbType.Int16: return typeInt16;
                case Data.DbType.UInt16: return typeUInt16;
                case Data.DbType.Int32: return typeInt32;
                case Data.DbType.UInt32: return typeUInt32;
                case Data.DbType.Int64: return typeInt64;
                case Data.DbType.UInt64: return typeUInt64;
                case Data.DbType.Date:
                case Data.DbType.DateTime:
                case Data.DbType.DateTime2: return typeDateTime;
                case Data.DbType.String:
                case Data.DbType.StringFixedLength:
                case Data.DbType.AnsiString:
                case Data.DbType.AnsiStringFixedLength: return typeString;
                case Data.DbType.Decimal: return typeDecimal;
                case Data.DbType.Double: return typeDouble;
                default: return typestring;
            }
        }

        /// <summary>
        /// 由DbType获取对应的Java 的 Type
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static string GetJavaType(this System.Data.DbType dbtype)
        {
            switch (dbtype)
            {
                case Data.DbType.Int16: return "int";
                case Data.DbType.UInt16: return "int";
                case Data.DbType.Int32: return "int";
                case Data.DbType.UInt32: return "int";
                case Data.DbType.Int64: return "long";
                case Data.DbType.UInt64: return "long";
                case Data.DbType.Date:
                case Data.DbType.DateTime:
                case Data.DbType.DateTime2: return "DateTime";
                case Data.DbType.String:
                case Data.DbType.StringFixedLength:
                case Data.DbType.AnsiString:
                case Data.DbType.AnsiStringFixedLength: return "String";
                case Data.DbType.Decimal: return "Float";
                case Data.DbType.Double: return "Float";
                default: return "String";
            }
        }
    }
}
