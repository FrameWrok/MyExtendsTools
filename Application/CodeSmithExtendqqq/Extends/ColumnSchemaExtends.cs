using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SchemaExplorer
{
    public static partial class ColumnSchemaExtends
    {
        /// <summary>
        /// 获取与数据库字段类型对应的CSharp类型
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetCSharpVariableType(ColumnSchema column)
        {
            switch (column.DataType)
            {
                case DbType.AnsiString: return "string";
                case DbType.AnsiStringFixedLength: return "string";
                case DbType.Binary: return "Nullable<int>";
                case DbType.Byte: return "int";
                case DbType.Guid: return "Guid";
                case DbType.Object: return "object";
                case DbType.String: return "string";
                case DbType.StringFixedLength: return "string";
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
    }
}
