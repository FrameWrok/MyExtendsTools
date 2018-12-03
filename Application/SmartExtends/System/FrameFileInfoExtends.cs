/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

namespace System.IO
{
    /// <summary>
    /// 对FileInfo类的扩展
    /// </summary>
    public static partial class FrameFileInfoExtends
    {
        /// <summary>
        /// 对 fileInfo 的文件对象进行压缩
        /// </summary>
        /// <param name="fileInfo">fileInfo对象</param>
        /// <param name="outputPath">压缩对象解压输出的路径</param>
        /// <param name="passWord">压缩文件所用的密码</param>
        public static void Zip(this FileInfo fileInfo, string outputPath, string passWord)
        {
            FileCompress.Compress(fileInfo.FullName, outputPath, passWord);
        }

        /// <summary>
        /// 对 fileInfo 的文件对象进行压缩
        /// </summary>
        /// <param name="fileInfo">fileInfo对象</param>
        /// <param name="outputPath">压缩文件输出的路径</param>        
        public static void Zip(this FileInfo fileInfo, string outputPath)
        {
            FileCompress.Compress(fileInfo.FullName, outputPath);
        }

        /// <summary>
        /// 对fileInfo 的文件对象进行解压缩
        /// </summary>
        /// <param name="fileInfo">fileInfo对象</param>
        /// <param name="outputPath">压缩对象解压输出的路径</param>
        public static void DeZip(this FileInfo fileInfo, string outputPath)
        {
            FileCompress.DeCompress(fileInfo.FullName, outputPath, 1024);
        }
    }
}
