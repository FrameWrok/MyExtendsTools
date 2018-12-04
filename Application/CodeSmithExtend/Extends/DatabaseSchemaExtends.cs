using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SchemaExplorer;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

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
                    return "DataBaseOperator.UsedCar";
                case "usedcarlog":
                    return "DataBaseOperator.UsedCarLog";
                case "usedcardealer":
                    return "DataBaseOperator.UsedCarDealer";
                case "usedcartrader":
                    return "Che168DataBaseOperator.UsedCarTrader";
                default: return "";
            }
        }
        /// <summary>
        /// 根据数据库执行输入的sql获取对应的 TableSchema，其中无法获取数据库中制定字段的长度
        /// </summary>
        /// <param name="database"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static TableSchema GetTableSchemaByScript(this DatabaseSchema database, string sql)
        {
            DataTable dt = new DataTable();
            DbProviderFactory dbFactory = SqlClientFactory.Instance;
            DbConnection dbConnection = dbFactory.CreateConnection();
            dbConnection.ConnectionString = database.ConnectionString;
            using (DbCommand dbcommand = dbConnection.CreateCommand())
            {
                dbcommand.CommandType = CommandType.Text;
                dbcommand.CommandText = " SET ROWCOUNT  1; " + sql;
                dbcommand.CommandTimeout = 180;

                using (DbDataAdapter dataAdapter = dbFactory.CreateDataAdapter())
                {
                    dataAdapter.SelectCommand = dbcommand;
                    dataAdapter.Fill(dt);
                }
            }
            if (dt != null)
            {
                dt.TableName = "QueryList_" + sql.Length;
                var tc = new System.ComponentModel.TypeConverter();
                TableSchema tbs = new TableSchema(database, dt.TableName, "dbo", DateTime.Now);
                foreach (DataColumn column in dt.Columns)
                {
                    tbs.Columns.Add(new ColumnSchema(tbs, column.ColumnName, column.DataType.GetDbType(), column.DataType.ToString(), 0, 0, 0, true));
                }
                return tbs;
            }
            return null;
        }
    }
}
