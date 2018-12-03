/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using FrameWork.Core.HelperClass;

namespace FrameWork
{
    /// <summary>
    /// 获取资源文件中的信息类
    /// </summary>
    public static class ResourceMSG
    {
        public static LangType FrameLangType = LangType.CH;

        /// <summary>
        /// 根据资源文件中的键值获取资源文件的文字信息
        /// </summary>
        /// <param name="key">资源文件的KEY键值</param>
        /// <returns>资源文件中的信息</returns>
        public static string GetMessage(string key)
        {
            return GetMessage(FrameLangType, key);
        }

        /// <summary>
        /// 获取汉语资源文件中的信息
        /// </summary>
        /// <param name="key">资源文件中的键值</param>
        /// <returns>返回资源文件中的信息</returns>
        public static string GetCHMessage(string key)
        {
            return GetMessage(LangType.CH, key);
        }

        /// <summary>
        /// 返回英文资源文件中的信息
        /// </summary>
        /// <param name="key">资源文件中的键值</param>
        /// <returns>返回资源文件中的信息</returns>
        public static string GetENMessage(string key)
        {
            return GetMessage(LangType.CH, key);
        }

        /// <summary>
        /// 根据语言类型参数以及KEY键值获取资源文件中的消息
        /// </summary>
        /// <param name="langType">语言类型</param>
        /// <param name="key">资源文件的KEY键值</param>
        /// <returns>返回资源文件中的信息</returns>
        private static string GetMessage(LangType langType, string key)
        {
            switch (langType)
            {
                case LangType.CH:
                    {
                        return FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.ResourceManager.GetString(key);
                    }

                case LangType.EN:
                    {
                        return FrameWork.Core.Resources.Exceptions.FrameENExceptionResource.ResourceManager.GetString(key);
                    }

                default:
                    return FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.ResourceManager.GetString(key);
            }
        }
    }
}
