using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FrameWork.Core;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ExtendsToolsForm.Dal
{
    public static class DbSchemaDAL
    {
        public static DataTable GetDbTableColumnSchema(string tablename, string sqlconnection)
        {
            string sql = @"
                            SELECT  tablename = d.name, tableDescription = ISNULL(f.value, ''), [order] = a.colorder, columnName = a.name,
                                    isIdentity = CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN 1
                                                      ELSE 0
                                                 END,
                                    isPrimarykey = CASE WHEN EXISTS ( SELECT 1
                                                                         FROM   sysobjects
                                                                         WHERE  xtype = 'PK' AND parent_obj = a.id AND name IN ( SELECT name
                                                                                                                                 FROM   sysindexes
                                                                                                                                 WHERE  indid IN ( SELECT   indid
                                                                                                                                                   FROM     sysindexkeys
                                                                                                                                                   WHERE    id = a.id AND colid = a.colid ) ) )
                                                           THEN 1
                                                           ELSE 0
                                                      END, dbtype = b.name, occupyByteLength = a.length, [length] = COLUMNPROPERTY(a.id, a.name, 'PRECISION'),
                                    precision = ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0), isAllowNull = CASE WHEN a.isnullable = 1 THEN 1
                                                                                                        ELSE 0
                                                                                                   END, defaultValue =REPLACE(REPLACE(ISNULL(e.text, ''),'(',''),')',''), Description = ISNULL(g.[value], '')
                            FROM    syscolumns a
                                    LEFT JOIN systypes b ON a.xusertype = b.xusertype
                                    INNER JOIN sysobjects d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties'
                                    LEFT JOIN syscomments e ON a.cdefault = e.id
                                    LEFT JOIN sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id
                                    LEFT JOIN sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0
                            WHERE   d.name = @tablename --如果只查询指定表,加上此条件 
                            ORDER BY a.id, a.colorder;";
            DataSet ds = new DataSet();
            DataCommon.DataBaseConnectionString = sqlconnection;
            List<DbParameter> listParam = new List<DbParameter>();
            listParam.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@tablename", SqlValue = tablename, Direction = ParameterDirection.Input, Size = 50 });
            DataCommon.Fill(sqlconnection, sql, CommandType.Text, ds, listParam, FrameWork.Core.FrameData.DataBaseType.SqlServer, "tableschema");
            if (ds.Tables.Count == 0)
                return null;
            return ds.Tables["tableschema"];
        }
        /// <summary>
        /// 获取数据库表列表
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="sqlconnection"></param>
        /// <returns></returns>
        public static DataTable GetTableList(string tablename, string sqlconnection)
        {
            string sql = @"
                        SELECT DISTINCT
                                d.name,  f.value AS [Description]
                        FROM    syscolumns a
                                LEFT JOIN systypes b ON a.xusertype = b.xusertype
                                INNER JOIN sysobjects d ON a.id = d.id AND d.xtype = 'U' AND d.name  <>'dtproperties'
                                LEFT JOIN syscomments e ON a.cdefault = e.id
                                LEFT JOIN sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id
                                LEFT JOIN sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0
	                        WHERE  ISNULL(@tablename,'')='' OR d.name LIKE @tablename+'%' ORDER BY d.name";
            DataSet ds = new DataSet();
            DataCommon.DataBaseConnectionString = sqlconnection;
            List<DbParameter> listParam = new List<DbParameter>();
            listParam.Add(new SqlParameter() { DbType = DbType.String, ParameterName = "@tablename", SqlValue = tablename, Direction = ParameterDirection.Input, Size = 50 });
            DataCommon.Fill(sqlconnection, sql, CommandType.Text, ds, listParam, FrameWork.Core.FrameData.DataBaseType.SqlServer, "tablesList");           
            return ds.Tables["tablesList"];
        }
    }
}
