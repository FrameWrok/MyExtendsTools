using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendsToolsForm.Models.DbModels
{
    /// <summary>
    /// 数据库连接Model
    /// </summary>
    public class DbConnectionModel
    {
        public string host { get; set; }
        public string user { get; set; }
        public string pwd { get; set; }
        public string dbname { get; set; }
        public override string ToString()
        {
            return "Data Source={0};Persist Security Info=True;User ID={1};Password={2};Initial Catalog={3};".Formats(host,user,pwd,dbname);
        }
    }
}
