/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork.Core.Resources.Exceptions;

namespace FrameWork.Core.CoreAttribute
{
    /// <summary>
    /// 数据库列与 实体字段 或 属性的映射标识类
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public partial class FrameColumnMappingAttributes : Attribute
    {
        #region 字段或属性

        private string columnName;

        /// <summary>
        /// Gets or sets 映射到 数据库的 列名
        /// </summary>
        public string ColumnName
        {
            get { return this.columnName; }
            set { this.columnName = value; }
        }

        private object defaultVale;

        /// <summary>
        /// Gets or sets 数据库中没有对应的值时，所赋给实体字段或属性的默认值
        /// 如果没有设置默认值，则取字段或属性初始化时的默认值
        /// </summary>
        public object DefaultVale
        {
            get { return this.defaultVale; }
            set { this.defaultVale = value; }
        }

        private bool isPrimaryKey;

        /// <summary>
        /// Gets or sets a value indicating whether 是否为主键.
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return this.isPrimaryKey; }
            set { this.isPrimaryKey = value; }
        }

        #endregion
    }

    /// <summary>
    /// 数据库列与 实体字段 或 属性的映射标识类
    /// </summary>
    public partial class FrameColumnMappingAttributes
    {
        /// <summary>
        /// Initializes a new instance of the FrameColumnMappingAttributes class.
        /// </summary>
        /// <param name="columnName">映射到数据库的列名，不能为空或NULL 或 空格</param>
        public FrameColumnMappingAttributes(string columnName)
            : this(columnName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FrameColumnMappingAttributes class.
        /// </summary>
        /// <param name="columnName">映射到数据库的 列名，不能为空或NULL 或 空格</param>
        /// <param name="isPrimaryKey">是 否 为主键</param>
        public FrameColumnMappingAttributes(string columnName, bool isPrimaryKey)
            : this(columnName, null, isPrimaryKey)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FrameColumnMappingAttributes class.
        /// </summary>
        /// <param name="columnName">映射到数据库的列名，不能为 空 或NULL或空格</param>
        /// <param name="defaultValue">默 认值</param>
        public FrameColumnMappingAttributes(string columnName, object defaultValue)
            : this(columnName, defaultValue, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FrameColumnMappingAttributes class.
        /// </summary>
        /// <param name="columnName">映射到数据库的列名，不能为空或NULL 或 空格</param>
        /// <param name="defaultValue">默 认值</param>
        /// <param name="isPrimaryKey">是 否 为主键</param>
        public FrameColumnMappingAttributes(string columnName, object defaultValue, bool isPrimaryKey)
        {
            if (columnName == null || columnName == string.Empty)
            {
                throw new Exception(FrameCHExceptionResource.__FrameColumnMappingAttributeColumnName_IsNullOrEmptyOrBlank);
            }
            else
            {
                this.columnName = columnName;
                this.defaultVale = defaultValue;
                this.isPrimaryKey = isPrimaryKey;
            }
        }
    }

    /// <summary>
    /// 数据库列与 实体字段 或 属性的映射标识类
    /// </summary>
    public partial class FrameColumnMappingAttributes
    {
        /// <summary>
        /// 比较两个 对象是 否 相等
        /// </summary>
        /// <param name="obj">要比较 的 另一个对象</param>
        /// <returns>返回 比较 结果</returns>
        public override bool Equals(object obj)
        {
            FrameColumnMappingAttributes other = obj as FrameColumnMappingAttributes;
            if (obj == null)
            {
                return false;
            }
            else
            {
                return this.ColumnName == other.ColumnName && this.DefaultVale == other.DefaultVale;
            }
        }

        /// <summary>
        /// 获 得 哈希码
        /// </summary>
        /// <returns>返回该对象的 哈希编码</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// 获得 此特性的 字符串表示
        /// </summary>
        /// <returns>返回 字符 串表示</returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("FrameColumnMappingAttribute");
            if (this.ColumnName != null)
            {
                s.Append(this.ColumnName.GetHashCode().ToString());
            }
            
            if (this.DefaultVale != null)
            {
                s.Append("__");
                s.Append(this.DefaultVale.GetHashCode().ToString());
            }

            return s.ToString();
        }
    }
}
