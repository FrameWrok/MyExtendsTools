/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

namespace System.Xml
{
    /// <summary>
    /// xmlAttribute操作类扩展
    /// </summary>
    public static partial class FrameXmlAttributeCollectionExtends
    {
        /// <summary>
        /// 将指定的属性插入集合，并将其作为集合中的最后一个节点。
        /// </summary>
        /// <param name="xmlAttributeCollection">属性节点集合</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void Add(this XmlAttributeCollection xmlAttributeCollection, string attributeName, string attributeValue)
        {
            XmlAttribute newXmlAttr = xmlAttributeCollection[0].OwnerDocument.CreateAttribute(attributeName);
            newXmlAttr.Value = attributeValue;
            xmlAttributeCollection[0].OwnerElement.Attributes.Append(newXmlAttr);
        }
    }
}
