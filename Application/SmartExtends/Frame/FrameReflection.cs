/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing;
using System.Data;
using System.Collections;
using SmartExtends.Frame;

namespace System
{
    /// <summary>
    /// 智能反射类，提供智能反射相关功能
    /// </summary>
    public static partial class FrameReflection
    {
        #region 获得类型的成员

        /// <summary>
        /// 根据映射方式取某个实体或匿名类型的属性集合
        /// </summary>
        /// <param name="type">实体类型或匿名类型</param>
        /// <param name="frameMappingType">映射方式</param>
        /// <returns>属性数组</returns>
        public static PropertyInfo[] GetPropertyInfos(this Type type, FrameMappingType frameMappingType)
        {
            return type.GetProperties(FrameReflection.GetBindingFlagsByFrameMappingType(frameMappingType));
        }

        /// <summary>
        /// 取某个实体或匿名类型的字段集合
        /// </summary>
        /// <param name="type">实体类型或匿名类型</param>
        /// <param name="frameMappingType">映射类型</param>
        /// <returns>字段数组</returns>
        public static FieldInfo[] GetFieldInfos(this Type type, FrameMappingType frameMappingType)
        {
            return type.GetFields(FrameReflection.GetBindingFlagsByFrameMappingType(frameMappingType));
        }

        /// <summary>
        /// 获取某个实体或匿名类型的由某特性标识的属性集合，包括公共属性和非公共属性
        /// </summary>
        /// <typeparam name="TAttribute">特性的类型</typeparam>
        /// <param name="type">实体或匿名对象的类型</param>
        /// <param name="frameMappingType">应用的反射类型</param>
        /// <returns>属性数组</returns>
        public static PropertyInfo[] GetPropertyInfoByAttribute<TAttribute>(this Type type, FrameMappingType frameMappingType) where TAttribute : Attribute
        {
            List<PropertyInfo> listPropertyinfo = new List<PropertyInfo>();
            foreach (PropertyInfo propertyinfo in GetPropertyInfos(type, frameMappingType))
            {
                TAttribute attribute = System.Attribute.GetCustomAttribute(propertyinfo, typeof(TAttribute), false) as TAttribute;
                if (attribute != null)
                {
                    listPropertyinfo.Add(propertyinfo);
                }
            }

            return listPropertyinfo.ToArray();
        }

        /// <summary>
        /// 获取某个实体或匿名类型的由某特性标识的字段集合，包括公共字段和非公共字段
        /// </summary>
        /// <typeparam name="TAttribute">特性的类型</typeparam>
        /// <param name="type">实体或匿名对象的类型</param>
        /// <param name="frameMappingType">应用的反射方式</param>
        /// <returns>字段数组</returns>
        public static FieldInfo[] GetFieldInfoByAttribute<TAttribute>(this Type type, FrameMappingType frameMappingType) where TAttribute : Attribute
        {
            List<FieldInfo> listFieldInfo = new List<FieldInfo>();
            foreach (FieldInfo fieldInfo in GetFieldInfos(type, frameMappingType))
            {
                TAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(TAttribute), false) as TAttribute;
                if (attribute != null)
                {
                    listFieldInfo.Add(fieldInfo);
                }
            }

            return listFieldInfo.ToArray();
        }

        /// <summary>
        /// 获取某个实体或匿名类型的方法
        /// </summary>
        /// <param name="type">实体或匿名类型</param>
        /// <param name="frameMappingType">映射类型</param>
        /// <returns>方法数组</returns>
        public static MethodInfo[] GetMethodInfoByMappingType(this Type type, FrameMappingType frameMappingType)
        {
            BindingFlags bindingFlags = FrameReflection.GetBindingFlagsByFrameMappingType(frameMappingType);
            return type.GetMethods(bindingFlags);
        }

        #endregion
    }

    /// <summary>
    /// 智能反射类，提供智能反射相关功能
    /// </summary>
    public static partial class FrameReflection
    {
        private static readonly Type int16type = typeof(System.Int16);
        private static readonly Type int32type = typeof(System.Int32);
        private static readonly Type int64type = typeof(System.Int64);
        private static readonly Type decimaltype = typeof(System.Decimal);
        private static readonly Type doubletype = typeof(System.Double);
        private static readonly Type stringtype = typeof(string);
        #region 数据类型转换

        /// <summary>
        /// 将字符串类型转换为其他数据类型
        /// </summary>
        /// <param name="resources">字符串数据源</param>
        /// <param name="targetType">目标数据类型</param>
        /// <returns>目标数据类型的值</returns>
        public static object ConvertTo(this object resources, Type targetType)
        {
            object result = null;
            if (targetType.IsEnum)
            {
                ////如果目标类型是枚举，则按照枚举的方式解析
                result = Enum.Parse(targetType, resources.ToString(), true);
            }
            else
                if (targetType == typeof(Color))
            {
                ////如果目标是颜色类型，则value当做是颜色值
                result = Color.FromName(resources.ToString());
            }
            else if (targetType.IsGenericType)
            {
                ////如果目标类型是List列表类型，则将Value以‘,’分隔
                if (targetType.GenericTypeArguments[0] == int16type)
                    result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToInt16(q); }).ToList();
                if (targetType.GenericTypeArguments[0] == int32type)
                    result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToInt32(q); }).ToList();
                if (targetType.GenericTypeArguments[0] == int64type)
                    result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToInt64(q); }).ToList();
                if (targetType.GenericTypeArguments[0] == decimaltype)
                    result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToDecimal(q); }).ToList();
                if (targetType.GenericTypeArguments[0] == doubletype)
                    result = resources.ToString().Split(',').ToList().Select(q => { return Convert.ToDouble(q); }).ToList();
                if (targetType.GenericTypeArguments[0] == stringtype)
                    result = resources.ToString().Split(',').ToList();

            }
            else if (targetType == typeof(TimeSpan))
            {
                ////如果目标类型是时间间隔，按照字符到时间间隔的方式解析，时间解析为毫秒
                result = Convert.ToDouble(resources);
            }
            else if (Nullable.GetUnderlyingType(targetType) != null)
            {
                // 可以分配NULL值的目标类型
                result = Convert.ChangeType(resources, Nullable.GetUnderlyingType(targetType));
            }
            else if (targetType == typeof(Boolean))
            {
                result = resources.ToString().ToBoolean();
            }
            else
            {
                // 其他数据类型
                result = Convert.ChangeType(resources, targetType);
            }

            return result;
        }

        /// <summary>
        /// 将源数据类型的数据转换为目标数据类型
        /// </summary>
        /// <typeparam name="TSourcesType">源数据类型</typeparam>
        /// <typeparam name="TtartgetType">目标数据类型</typeparam>
        /// <param name="resources">源数据</param>
        /// <returns>转换后的目标数据</returns>
        public static TtartgetType ConvertTo<TSourcesType, TtartgetType>(TSourcesType resources)
        {
            return (TtartgetType)Convert.ChangeType(resources, typeof(TtartgetType));
        }

        /// <summary>
        /// 将源数据类型的数据转换为目标数据类型
        /// </summary>
        /// <typeparam name="TtartgetType">目标数据类型</typeparam>
        /// <param name="resources">源数据</param>
        /// <returns>转换后的目标数据</returns>
        public static TtartgetType ConvertTo<TtartgetType>(this string resources)
        {
            return (TtartgetType)Convert.ChangeType(resources, typeof(TtartgetType));
        }

        #endregion
    }

    /// <summary>
    /// 智能反射类，提供智能反射相关功能
    /// </summary>
    public static partial class FrameReflection
    {
        /// <summary>
        /// 根据映射类型获取映射方式
        /// </summary>
        /// <param name="frameMappingType">映射类型</param>
        /// <returns>BindingFlags</returns>
        public static BindingFlags GetBindingFlagsByFrameMappingType(FrameMappingType frameMappingType)
        {
            BindingFlags bindingFlags = new BindingFlags();
            switch (frameMappingType)
            {
                case FrameMappingType.FrameAttributePrivate:
                case FrameMappingType.ProertyPrivate:
                case FrameMappingType.FieldPrivate:
                case FrameMappingType.XmlElementPrivate:
                    {
                        bindingFlags = BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Instance;
                    }

                    break;
                case FrameMappingType.FrameAttributePublic:
                case FrameMappingType.PropertyPublic:
                case FrameMappingType.FieldPublic:
                case FrameMappingType.XmlElementPublic:
                    {
                        bindingFlags = BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static;
                    }

                    break;
                case FrameMappingType.FrameAttribute:
                case FrameMappingType.Property:
                case FrameMappingType.Field:
                case FrameMappingType.XmlElement:
                    {
                        bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static;
                    }

                    break;
                default:
                    {
                        bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static;
                    }

                    break;
            }

            return bindingFlags;
        }
    }
}
