/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：
 ◆版本：1.0
**********************************************************/

using FrameWork.Core;

namespace System
{
    /// <summary>
    /// 全局唯一标识类
    /// </summary>
    public static class SysGuid
    {
        /// <summary>
        /// 获取全局唯一标识符
        /// </summary>
        public static string NewGuid
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
    }
}
