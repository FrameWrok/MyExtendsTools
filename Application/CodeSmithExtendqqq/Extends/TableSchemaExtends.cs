using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchemaExplorer
{
    public static partial class TableSchemaExtends
    {
        /// <summary>
        /// 获取插入的sql，排除主键
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetInsertIntoSql(TableSchema table)
        {
            StringBuilder tableSql = new StringBuilder("INSERT INTO " + table.Name + "( ");
            StringBuilder valueSql = new StringBuilder("VALUES ( ");
            foreach (var colume in table.Columns)
            {
                if (!colume.IsPrimaryKeyMember)
                {
                    tableSql.Append(" " + colume.Name + ",");
                    valueSql.Append("@" + colume.Name + ",");
                }
            }
            tableSql = new StringBuilder(tableSql.ToString().Trim(',') + " )");
            tableSql.AppendLine("\r                                                      " + valueSql.ToString().Trim(',') + " )");
            return tableSql.ToString() + "                              SELECT @@IDENTITY";
        }
        /// <summary>
        /// 获取更新的SQL
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetUpdateSql(this TableSchema table)
        {
            StringBuilder updateSql = new StringBuilder("UPDATE " + table.Name + " SET \r");
            string primaryKey = "";
            foreach (var colume in table.Columns)
            {
                if (!colume.IsPrimaryKeyMember)
                    updateSql.Append("                                      " + colume.Name + "= @" + colume.Name + ",\r");
                else
                    primaryKey = colume.Name;
            }
            updateSql = new StringBuilder(updateSql.ToString().Trim().Trim(','));
            updateSql.Append("\r                              WHERE " + primaryKey + " = @" + primaryKey);
            return updateSql.ToString();
        }
        /// <summary>
        /// 物理删除的SQL
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetDeleteSql(this TableSchema table)
        {
            StringBuilder delSql = new StringBuilder("DELETE " + table.Name + " WHERE ");
            List<String> whereList = new List<string>();
            foreach (var item in table.PrimaryKey.MemberColumns)
            {
                whereList.Add(item.Name + "=@" + item.Name);
            }
            delSql.Append(whereList.Aggregate((p, n) => { return p + " AND " + n; }));
            return delSql.ToString();            
        }
    }
}
