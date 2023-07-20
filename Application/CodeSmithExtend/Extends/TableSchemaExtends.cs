using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchemaExplorer
{
    /// <summary>
    /// 对CodeSmithTableSchema的扩展
    /// </summary>
    public static partial class TableSchemaExtends
    {
        /// <summary>
        /// 获取插入的sql，排除主键
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetInsertIntoSql(TableSchema table)
        {
            StringBuilder insertSql = new StringBuilder("INSERT INTO " + table.Name + "( ");
            insertSql.AppendLine(string.Join(",", table.Columns.Where(p => !p.IsPrimaryKeyMember).Select(P => " " + P.Name).ToList()) + ")");
            insertSql.AppendLine("                            VALUES (" + string.Join(",", table.Columns.Where(p => !p.IsPrimaryKeyMember).Select(P => "@" + P.Name).ToList()) + ")");
            insertSql.AppendLine("                            SELECT SCOPE_IDENTITY()");
            return insertSql.ToString().Trim();
        }
        /// <summary>
        /// 获取更新的SQL
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetUpdateSql(this TableSchema table)
        {
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine("UPDATE " + table.Name + " SET");
            updateSql.AppendLine("                            " + string.Join(",", table.Columns.Where(p => !p.IsPrimaryKeyMember).Select(P => P.Name + "=" + "@" + P.Name).ToList()));
            string primaryKey = table.Columns.Where(p => p.IsPrimaryKeyMember).FirstOrDefault().Name;
            updateSql.AppendLine("                            WHERE " + primaryKey + "= @" + primaryKey);
            return updateSql.ToString().Trim();
        }

        /// <summary>
        /// 获取查询详情sql
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetInfoSql(this TableSchema table)
        {
            StringBuilder infosql = new StringBuilder("");
            infosql.AppendLine(" SELECT " + string.Join(",", table.Columns.Select(P => "T." + P.Name).ToList()));
            infosql.AppendLine("                            FROM " + table.Name + " AS T WITH (NOLOCK) ");
            string primaryKey = table.Columns.FirstOrDefault(p => p.IsPrimaryKeyMember).Name;
            infosql.AppendLine("                            WHERE T." + primaryKey + " = @" + primaryKey);
            return infosql.ToString().Trim();
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
