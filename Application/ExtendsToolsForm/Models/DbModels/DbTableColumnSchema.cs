using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendsToolsForm.Models.DbModels
{
    /// <summary>
    /// 数据库表接口类
    /// </summary>
    public class DbTableColumnSchema
    {
        #region 自动生成

        /// <summary>
        /// 表名
        /// </summary>
        public string tablename { get; set; }
        /// <summary>
        /// 表说明
        /// </summary>
        public string tableDescription { get; set; }
        /// <summary>
        /// 字段排序
        /// </summary>
        public short order { get; set; }
        /// <summary>
        /// 列明
        /// </summary>
        public string columnName { get; set; }
        /// <summary>
        /// 是否自增
        /// </summary>
        public int isIdentity { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public int isPrimarykey { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string dbtype { get; set; }
        /// <summary>
        /// 占用字节数
        /// </summary>
        public short occupyByteLength { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// 小数长度
        /// </summary>
        public int precision { get; set; }
        /// <summary>
        /// 是否允许为null
        /// </summary>
        public int isAllowNull { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string defaultValue { get; set; }
        /// <summary>
        /// 字段说明
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
