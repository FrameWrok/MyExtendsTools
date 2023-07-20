using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SchemaExplorer;

namespace SchemaExplorer
{
    public static partial class DatabaseSchemaExtends
    {
        /// <summary>
        /// 获取操作写入数据库的数据库操作符
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public static string GetExecuteDataBase(this DatabaseSchema database)
        {
            switch (database.FullName.ToLower())
            {
                case "usedcar":
                    return "Tools.GetInt(DataBaseOperator.UsedCarWrite.ExecuteScalar(sql, CommandType.Text, parList.ToArray()), 0)";
                case "usedcarlog":
                    return "Tools.GetInt(DataBaseOperator.UsedCarLogWrite.ExecuteScalar(sql, CommandType.Text, parList.ToArray()), 0)";
                default: return "";
            }
        }
    }
}
