/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System.Globalization;

namespace FrameWork.Core.HelperClass
{
    /// <summary>
    /// 框架默认区域文化信息
    /// </summary>
    public static class FrameDefaultCultureInfo
    {
        private static CultureInfo defaultCultureInfo = new CultureInfo("zh-cn");

        /// <summary>
        /// Gets 获取框架默认区域文化信息
        /// </summary>
        public static CultureInfo DefaultCultureInfo
        {
            get { return FrameDefaultCultureInfo.defaultCultureInfo; }
        }
    }
}
