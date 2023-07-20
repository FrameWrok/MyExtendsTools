using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SchemaExplorer
{
    /// <summary>
    /// 列扩展
    /// </summary>
    public static partial class ColumnSchemaExtends
    {
        /// <summary>
        /// 获取与数据库字段类型对应的CSharp类型
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetCSharpVariableType(this ColumnSchema column)
        {
            switch (column.DataType)
            {
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength: return "string";
                case DbType.Binary: return "Nullable<int>";
                case DbType.Byte: return "int";
                case DbType.Guid: return "Guid";
                case DbType.Object: return "object";
                case DbType.Boolean: if (column.AllowDBNull) { return "Nullable<bool>"; } else { return "bool"; };
                case DbType.Currency: if (column.AllowDBNull) { return "Nullable<decimal>"; } else { return "decimal"; };
                case DbType.Decimal: if (column.AllowDBNull) { return "Nullable<decimal>"; } else { return "decimal"; };
                case DbType.Double: if (column.AllowDBNull) { return "Nullable<double>"; } else { return "double"; };
                case DbType.Int16: if (column.AllowDBNull) { return "Nullable<short>"; } else { return "short"; };
                case DbType.UInt16: if (column.AllowDBNull) { return "Nullable<ushort>"; } else { return "ushort"; };
                case DbType.Int32: if (column.AllowDBNull) { return "Nullable<int>"; } else { return "int"; };
                case DbType.UInt32: if (column.AllowDBNull) { return "Nullable<uint>"; } else { return "uint"; };
                case DbType.Int64: if (column.AllowDBNull) { return "Nullable<long>"; } else { return "long"; };
                case DbType.UInt64: if (column.AllowDBNull) { return "Nullable<ulong>"; } else { return "ulong"; };
                case DbType.SByte: if (column.AllowDBNull) { return "Nullable<sbyte>"; } else { return "sbyte"; };
                case DbType.Single: if (column.AllowDBNull) { return "Nullable<float>"; } else { return "float"; };
                case DbType.Date: if (column.AllowDBNull) { return "Nullable<DateTime>"; } else { return "DateTime"; };
                case DbType.DateTime: if (column.AllowDBNull) { return "Nullable<DateTime>"; } else { return "DateTime"; };
                case DbType.Time: if (column.AllowDBNull) { return "Nullable<TimeSpan>"; } else { return "TimeSpan"; };
                case DbType.VarNumeric: if (column.AllowDBNull) { return "Nullable<decimal>"; } else { return "decimal"; };
                default: return "string";
            }
        }
        /// <summary>
        /// 获取与数据库字段类型对应的CSharp类型
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetJavaVariableType(this ColumnSchema column)
        {
            switch (column.DataType)
            {
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength: return "String";
                case DbType.Binary: return "Integer";
                case DbType.Byte: return "byte";
                case DbType.Guid: return "Guid";
                case DbType.Object: return "object";
                case DbType.Boolean: if (column.AllowDBNull) { return "Boolean"; } else { return "boolean"; };
                case DbType.Currency: if (column.AllowDBNull) { return "Float"; } else { return "float"; };
                case DbType.Decimal: if (column.AllowDBNull) { return "Float"; } else { return "float"; };
                case DbType.Double: if (column.AllowDBNull) { return "Double"; } else { return "double"; };
                case DbType.Int16: if (column.AllowDBNull) { return "Short"; } else { return "short"; };
                case DbType.UInt16: if (column.AllowDBNull) { return "Short"; } else { return "short"; };
                case DbType.Int32: if (column.AllowDBNull) { return "Integer"; } else { return "int"; };
                case DbType.UInt32: if (column.AllowDBNull) { return "Integer"; } else { return "int"; };
                case DbType.Int64: if (column.AllowDBNull) { return "Long"; } else { return "long"; };
                case DbType.UInt64: if (column.AllowDBNull) { return "Long"; } else { return "long"; };
                case DbType.SByte: if (column.AllowDBNull) { return "Byte"; } else { return "byte"; };
                case DbType.Single: if (column.AllowDBNull) { return "Float"; } else { return "float"; };
                case DbType.Date: if (column.AllowDBNull) { return "Date"; } else { return "Date"; };
                case DbType.DateTime: if (column.AllowDBNull) { return "Date"; } else { return "Date"; };
                case DbType.Time: if (column.AllowDBNull) { return "Date"; } else { return "Date"; };
                case DbType.VarNumeric: if (column.AllowDBNull) { return "float"; } else { return "float"; };
                default: return "string";
            }
        }
        /// <summary>
        /// 获取Column的数据库数据类型 SqlDbType
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetNativeType(this ColumnSchema column)
        {
            switch (column.NativeType.ToLower())
            {
                case "text":
                    return "SqlDbType.Text";
                case "int":
                case "system.int32":
                    return "SqlDbType.Int";
                case "bigint":
                case "system.int64":
                    return "SqlDbType.BigInt";
                case "decimal":
                    return "SqlDbType.Decimal";
                case "smallint":
                    return "SqlDbType.SmallInt";
                case "binary":
                    return "SqlDbType.Binary";
                case "bit":
                    return "SqlDbType.Bit";
                case "float":
                    return "SqlDbType.Float";
                case "tinyint":
                case "system.byte":
                    return "SqlDbType.TinyInt";
                case "money":
                    return "SqlDbType.Money";
                case "smallmoney":
                    return "SqlDbType.SmallMoney";
                case "timestamp":
                    return "SqlDbType.Timestamp";
                case "datetime":
                case "system.datetime":
                    return "SqlDbType.DateTime";
                case "datetime2":
                    return "SqlDbType.DateTime2";
                case "date":
                    return "SqlDbType.Date";
                case "datetimeoffset":
                    return "SqlDbType.DateTimeOffset";
                case "char":
                    return "SqlDbType.Char";
                case "nvarchar":
                    return "SqlDbType.NVarChar";
                default:
                    return "SqlDbType.VarChar";
            }
        }

        /// <summary>
        /// 获取列的指定长度的说明
        /// </summary>
        /// <param name="column"列></param>
        /// <param name="length">最大长度，传入0则获取全部长度</param>
        /// <returns></returns>
        public static string GetShortDescription(this ColumnSchema column, int length = 4, bool descriptionEmpthShowColumnName = false)
        {
            if (length < 1)
                return column.Description;
            if (column.Description != null && column.Description.Trim().Length > length)
                return column.Description.Substring(0, length);
            if (descriptionEmpthShowColumnName && string.IsNullOrEmpty((column.Description ?? "").Trim()))
                return column.Name;
            return column.Description ?? "";
        }

        /// <summary>
        /// 是否是字符串列
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static bool IsStringColumn(this ColumnSchema column)
        {
            return column.DataType == DbType.AnsiString || column.DataType == DbType.AnsiStringFixedLength || column.DataType == DbType.String || column.DataType == DbType.StringFixedLength;
        }

        /// <summary>
        /// 是否是数字列，如 int,double等
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static bool IsNumberColumn(this ColumnSchema column)
        {
            return column.DataType == DbType.Byte || column.DataType == DbType.Int16 || column.DataType == DbType.Int32 || column.DataType == DbType.Int64 || column.DataType == DbType.Double;
        }

        /// <summary>
        /// 是否是时间列如 Date，DateTime，DateTime2
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static bool IsDateColumn(this ColumnSchema column)
        {
            return column.DataType == DbType.Date || column.DataType == DbType.DateTime || column.DataType == DbType.DateTime2;
        }
        /// <summary>
        /// 获取字段的Input的输入框的最大长度
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static int GetInputMaxLength(this ColumnSchema column)
        {
            switch (column.DataType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                    if (column.Size > 0)
                        return column.Size;
                    else
                        return 50;
                case DbType.UInt16: return 5;
                case DbType.Int16: return 6;
                case DbType.UInt32: return 10;
                case DbType.Int32: return 11;
                case DbType.UInt64: return 19;
                case DbType.Int64: return 20;
                case DbType.Byte: return 3;
                case DbType.Time: return 8;
                case DbType.Date: return 10;
                case DbType.DateTime:
                case DbType.DateTime2: return 19;
                case DbType.Double:
                case DbType.Decimal:
                    {
                        if (column.Size > 0 && column.Scale > 0)
                            return column.Size + column.Scale + 1;
                        return 13;
                    }
                default: return column.Size;
            }
        }
    }
}
