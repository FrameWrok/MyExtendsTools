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

namespace FrameWork.Core.FrameData
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// SqlServer数据库
        /// </summary>
        SqlServer,

        /// <summary>
        /// Oracle数据库
        /// </summary>
        Oracle,

        /// <summary>
        /// Oledb数据库
        /// </summary>
        Oledb,

        /// <summary>
        /// PostgreSQL数据库
        /// </summary>
        PostgreSQL
    }
}