using System;
using System.IO;
using System.Reflection;

namespace SmartExtends.Resources
{
    /// <summary>
    /// 读取嵌入式资源类
    /// </summary>
    public static class EmbeddedResource
    {
        /// <summary>
        /// 获取当前的类的类型
        /// </summary>
        private static Type type = MethodBase.GetCurrentMethod().DeclaringType;

        /// <summary>
        /// 当前的命名空间
        /// </summary>
        private static string thisNamespace = type.Namespace;

        /// <summary>
        /// 获得当前运行的Assembly：程序集
        /// </summary>
        private static Assembly assembly = Assembly.GetExecutingAssembly();

        /// <summary>
        /// 获取框架程序集嵌入资源的二进制流，便用遵循框架协定及协调框架配置
        /// </summary>
        /// <param name="embeddedFilePath">嵌入的资源文件的路径，为框架程序集根目录的相对路径 如(/file/aa.xml)</param>
        /// <returns>返回嵌入文件的二进制流</returns>
        public static Stream GetEmbeddedFileStream(string embeddedFilePath)
        {
            embeddedFilePath = "." + embeddedFilePath.Replace('/', '.').Replace('\\', '.').TrimStart(new char[] { '.', '/', '\\' });
            ////根据名称空间和文件名生成资源名称
            string resourceName = thisNamespace + embeddedFilePath;
            ////根据资源名称从Assembly中获取此资源的Stream
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            return stream;
        }

        /// <summary>
        /// 获取某个程序集中的特定资源二进制流
        /// </summary>
        /// <param name="embeddedFilePath">嵌入的资源文件的路径，为程序集根目录的相对路径 如(/file/aa.xml)</param>
        /// <param name="assembly">源程序集</param>
        /// <returns>返回嵌入文件的二进制流</returns>
        public static Stream GetEmbeddedFileStream(string embeddedFilePath, Assembly assembly)
        {
            embeddedFilePath = "." + embeddedFilePath.Replace('/', '.').Replace('\\', '.').TrimStart(new char[] { '.', '/', '\\' });
            ////根据名称空间和文件名生成资源名称
            string resourceName = assembly.GetName().Name + embeddedFilePath;
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            return stream;
        }
    }
}
