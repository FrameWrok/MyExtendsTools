using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// 枚举说明特性，可方便操作枚举
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumDescriptionAttribute : Attribute
    {
        #region 字段属性
        
        private string displayText;

        /// <summary>
        /// 枚举对应显示的问题
        /// </summary>
        public string DisplayText
        {
            get { return displayText; }
            set { displayText = value; }
        }

        private string code;
        /// <summary>
        /// 自定义枚举的编号
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private int sortNumber;
        /// <summary>
        /// 绑定到数据源时，排列顺序
        /// </summary>
        public int SortNumber
        {
            get { return sortNumber; }
            set { sortNumber = value; }
        }

        private FieldInfo fieldIno;
        /// <summary>
        /// 枚举值
        /// </summary>
        public int EnumValue
        {
            get { return (int)fieldIno.GetValue(null); }
        }
        /// <summary>
        /// 字段名称，枚举的实际名称
        /// </summary>
        public string FieldName
        {
            get { return fieldIno.Name; }
        }
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 对枚举的说明
        /// </summary>
        /// <param name="enumDisplayText">枚举对应的显示文字</param>
        /// <param name="code">枚举自定义编号</param>
        /// <param name="sortNumber">枚举绑定到数据源时的排序</param>
        public EnumDescriptionAttribute(string enumDisplayText, string code, int sortNumber)
        {
            this.DisplayText = enumDisplayText;
            this.SortNumber = sortNumber;
            this.Code = code;
        }

        /// <summary>
        /// 对枚举的说明
        /// </summary>
        /// <param name="enumDisplayText">枚举对应的显示文字</param>
        /// <param name="code">枚举自定义编号</param>
        public EnumDescriptionAttribute(string enumDisplayText, string code)
            : this(enumDisplayText, code, 0)
        {

        }
        /// <summary>
        /// 对枚举的说明
        /// </summary>
        /// <param name="enumDisplayText">枚举对应的显示文字</param>
        /// <param name="sortNumber">枚举绑定到数据源时的排序</param>
        public EnumDescriptionAttribute(string enumDisplayText, int sortNumber)
            : this(enumDisplayText, "", sortNumber)
        {

        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 得到对枚举的描述文本
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static string GetEnumTyoeDescription(Type enumType)
        {
            EnumDescriptionAttribute[] eds = (EnumDescriptionAttribute[])enumType.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (eds.Length != 1) return string.Empty;
            return eds[0].DisplayText;
        }

        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="enumValue">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            Type type = enumValue.GetType();
            FieldInfo f = type.GetField(enumValue.ToString());
            object[] objets = f.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (objets.Length != 1)
                return enumValue.ToString();
            return (objets[0] as EnumDescriptionAttribute).DisplayText;
        }
        #endregion

        /// <summary>
        /// 枚举说明缓存类
        /// </summary>
        private class EnumDescriptionCacheClass
        {
            private Type type;

            public Type Type
            {
                get { return type; }
                set { type = value; }
            }

            private EnumDescriptionAttribute enumTypeDescription;

            public EnumDescriptionAttribute EnumTypeDescription
            {
                get { return enumTypeDescription; }
                set { enumTypeDescription = value; }
            }

            private List<EnumDescriptionAttribute> enumValueList;

            public List<EnumDescriptionAttribute> EnumValueList
            {
                get { return enumValueList; }
                set { enumValueList = value; }
            }
            

        }
    }
}
