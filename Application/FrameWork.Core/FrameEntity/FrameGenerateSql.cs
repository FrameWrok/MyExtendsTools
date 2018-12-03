using System;
using System.Collections.Generic;
using System.Text;

namespace FrameWork.Core.FrameEntity
{
    /// <summary>
    /// 该类负责生成框架统一的sql语句
    /// </summary>
    public static partial class FrameGenerateSql
    {
        /// <summary>
        /// 生成插入数据的sql语句
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="list">要插入的列的列表</param>
        /// <param name="frameDataBaseType">数据库类型</param>
        /// <returns>返回生成的插入的sql语句</returns>
        internal static string GenerateInsertSql(string tableName, Dictionary<string, object> list, FrameData.DataBaseType frameDataBaseType)
        {
            StringBuilder insertTableSql = new StringBuilder("INSERT INTO " + GetTableName(tableName) + " (");
            StringBuilder insertValuesSql = new StringBuilder(" VALUES ( ");
            foreach (KeyValuePair<string, object> column in list)
            {
                insertTableSql.Append(" " + column.Key + ",");
                insertValuesSql.Append(" " + FrameData.FrameDataBase.CreateParameterName(column.Key, frameDataBaseType) + ",");
            }

            return insertTableSql.ToString().TrimEnd(',') + " ) " + insertValuesSql.ToString().TrimEnd(',') + " )";
        }

        /// <summary>
        /// 生成修改传入表的的sql语句
        /// </summary>
        /// <param name="tableName">要修改的表的名称</param>
        /// <param name="primaryKeyName">主键列的列名称</param>
        /// <param name="list">要更新的列的列表</param>
        /// <param name="frameDataBaseType">数据库类型</param>
        /// <returns>返回生成的更新表的sql语句</returns>
        internal static string GenerateUpdateSql(string tableName, string primaryKeyName, Dictionary<string, object> list, FrameData.DataBaseType frameDataBaseType)
        {
            StringBuilder updateTableSql = new StringBuilder(" UPDATE " + GetTableName(tableName) + " SET ");
            foreach (string column in list.Keys)
            {
                if (!column.Compare(primaryKeyName, true))
                    updateTableSql.Append(column + " = " + FrameData.FrameDataBase.CreateParameterName(column, frameDataBaseType) + ",");
            }

            return updateTableSql.Remove(updateTableSql.Length - 1, 1).Append(" WHERE " + primaryKeyName + " = " + FrameData.FrameDataBase.CreateParameterName(primaryKeyName, frameDataBaseType)).ToString();
        }

        /// <summary>
        /// 生成删除该实体的sql语句
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="primaryKeyName">主键列名称</param>
        /// <param name="frameDataBaseType">数据库类型</param>
        /// <returns>返回生成的删除数据的sql语句</returns>
        internal static string GenerateDeleteSql(string tableName, string primaryKeyName, FrameData.DataBaseType frameDataBaseType)
        {
            StringBuilder delTableSql = new StringBuilder(" DELETE FROM " + GetTableName(tableName) + " WHERE " + primaryKeyName + " = " + FrameData.FrameDataBase.CreateParameterName(primaryKeyName, frameDataBaseType));
            return delTableSql.ToString();
        }
    }

    /// <summary>
    /// 该类负责生成框架统一的sql语句
    /// </summary>
    public static partial class FrameGenerateSql
    {
        /// <summary>
        /// 由实体类型名称获取表名称
        /// </summary>
        /// <param name="typeName">实体类型全称</param>
        /// <returns>表名称</returns>
        private static string GetTableName(string typeName)
        {
            string[] typeArray = typeName.Split('.');
            return "[" + typeArray[typeArray.Length - 1] + "]";
        }
    }
}
