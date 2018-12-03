/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Xml;

namespace System
{
    /// <summary>
    /// xmlNode操作类扩展
    /// </summary>
    public static partial class FrameXmlNodeExtends
    {
        /// <summary>
        ///  获取一个 System.Xml.XmlAttributeCollection，它包含该节点的属性。
        ///  可设置是否区分大小写查询
        /// </summary>
        /// <param name="xmlnode">xmlnode</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="matchCase">属性名称是否区分大小写</param>
        /// <returns>属性值</returns>
        public static string Attributes(this XmlNode xmlnode, string attributeName, bool matchCase)
        {
            if (attributeName.IsNullOrEmptyOrBlank())
                throw new Exception("要查询的属性名称不能为空");
            foreach (XmlAttribute attr in xmlnode.Attributes)
            {
                if (attr.Name == attributeName)
                {
                    return attr.Value;
                }

                if (!matchCase && attr.Name.Compare(attributeName, true))
                    return attr.Value;
            }

            throw new Exception("该节点无" + attributeName + "属性值");
        }

        /// <summary>
        ///  获取一个 System.Xml.XmlAttributeCollection，它包含该节点的属性。
        /// </summary>
        /// <param name="xmlnode">xmlnode</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="defaultValue">如该节点无该属性名称，则返回该值</param>
        /// <returns>属性值</returns>
        public static string Attributes(this XmlNode xmlnode, string attributeName, string defaultValue)
        {
            return Attributes(xmlnode, attributeName, defaultValue, true);
        }

        /// <summary>
        ///  获取一个 System.Xml.XmlAttributeCollection，它包含该节点的属性。
        ///  如无该属性则返回传入的默认值，可设置是否区分大小写查询  
        /// </summary>
        /// <param name="xmlnode">xmlnode</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="defaultValue">如无该属性名称则返回该值</param>
        /// <param name="matchCase">是否区分大小写</param>
        /// <returns>属性值</returns>
        public static string Attributes(this XmlNode xmlnode, string attributeName, string defaultValue, bool matchCase)
        {
            if (attributeName.IsNullOrEmptyOrBlank())
                throw new Exception("要查询的属性名称不能为空");
            foreach (XmlAttribute attr in xmlnode.Attributes)
            {
                if (attr.Name == attributeName)
                {
                    return attr.Value;
                }

                if (!matchCase)
                    if (attr.Name.ToLower() == attributeName.ToLower())
                    {
                        return attr.Value;
                    }
            }

            return defaultValue;
        }

        /// <summary>
        /// 将制定的属性名，属性值添加到该节点的属性集合中
        /// </summary>
        /// <param name="xmlnode">xmlnode</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(this XmlNode xmlnode, string attributeName, string attributeValue)
        {
            XmlAttribute newXmlAttr = xmlnode.OwnerDocument.CreateAttribute(attributeName);
            newXmlAttr.Value = attributeValue;
            xmlnode.Attributes.Append(newXmlAttr);
        }
    }    
}
