using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FrameWork.Core;
using ExtendsToolsForm.Models.DbModels;
using ExtendsToolsForm.Dal;

namespace ExtendsToolsForm.BLL
{
    public static class DbSchemaBLL
    {
        public static List<DbTableColumnSchema> GetDbTableColumnSchema(string tablename, string sqlconnection, out string tableName, out string tableDescription)
        {
            tableName = tablename; tableDescription = "";
            var table = DbSchemaDAL.GetDbTableColumnSchema(tablename, sqlconnection);
            if (table == null || table.Rows.Count == 0)
                return null;
            List<DbTableColumnSchema> columnlist = table.ToList<DbTableColumnSchema>();
            tablename = columnlist.First().tablename;
            tableDescription = columnlist.First().tableDescription;
            return columnlist;
        }
        public static List<DbTableModels> GetDbTablesList(string tablename, string sqlconnection)
        {
            var table= DbSchemaDAL.GetTableList(tablename, sqlconnection);            
            return table.ToList<DbTableModels>();
        }
    }
}
