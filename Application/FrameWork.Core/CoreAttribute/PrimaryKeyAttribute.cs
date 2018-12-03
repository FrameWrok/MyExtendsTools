/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;

namespace FrameWork.Core.CoreAttribute
{
    /// <summary>    
    /// 设置该字段为主键
    /// 当实体或Model继承自此类，可以自动生成生成增删改的sql
    /// 一个实体或Model只能有一个字段声明该特性，否则会抛出运行时异常
    /// </summary>
    public class PrimaryKeyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the PrimaryKeyAttribute class.
        /// 设置该字段为主键的特性构造函数
        /// </summary>
        /// <param name="primaryKeyColumnName">主键列的名称</param>
        public PrimaryKeyAttribute(string primaryKeyColumnName)
        {
            if (primaryKeyColumnName.IsNullOrEmptyOrBlank())
                throw new ArgumentNullException("primaryKeyColumnName");
            this.primaryKeyName = primaryKeyColumnName;
        }

        /// <summary>
        /// Gets 被设置为主键的列
        /// </summary>
        public string PrimaryKeyName
        {
            get { return this.primaryKeyName; }
        }

        private string primaryKeyName;
    }
}