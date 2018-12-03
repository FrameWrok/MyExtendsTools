/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

namespace SmartExtends.Frame
{
    /// <summary>
    /// 实体与数据库映射方式枚举，如实体映射到数据库表，可通过字段，特性标识，属性映射
    /// </summary>
    public enum FrameMappingType
    {
        /// <summary>
        /// 按框架特性映射, 不区分公有与私有
        /// </summary>
        FrameAttribute,

        /// <summary>
        /// 按框架特性映射, 公有
        /// </summary>
        FrameAttributePublic,

        /// <summary>
        /// 按框架特性映射, 私有
        /// </summary>
        FrameAttributePrivate,

        /// <summary>
        /// 按XMLElement映射, 不区分公有与私有
        /// </summary>
        XmlElement,

        /// <summary>
        /// 按XMLElement映射，公有
        /// </summary>
        XmlElementPublic,

        /// <summary>
        /// 按XMLElement映射，私有
        /// </summary>
        XmlElementPrivate,

        /// <summary>
        /// 按字段映射，字段可以是公有也可以是私有
        /// </summary>
        Field,

        /// <summary>
        /// 按字段映射，字段是公有
        /// </summary>
        FieldPublic,

        /// <summary>
        /// 按字段映射，字段是私有
        /// </summary>
        FieldPrivate,

        /// <summary>
        /// 按属性映射，属性可以是公有也可以是私有
        /// </summary>
        Property,

        /// <summary>
        /// 按属性映射，属性是公有
        /// </summary>
        PropertyPublic,

        /// <summary>
        /// 按属性映射，属性是私有
        /// </summary>
        ProertyPrivate
    }
}
