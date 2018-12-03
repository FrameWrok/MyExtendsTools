/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

namespace System.Data
{
    /// <summary>
    /// Parameter标识方向
    /// </summary>
   public static class FrameParameterDirection
    {
        /// <summary>
        /// Gets 参数是输出参数
        /// </summary>
        public static object Output
        {
            get { return "Proc_Output"; }
        }

        /// <summary>
        /// Gets 参数是返回值参数
        /// </summary>
        public static object ReturnValue
        {
            get { return "Proc_ReturnValue"; }
        }
    }
}
