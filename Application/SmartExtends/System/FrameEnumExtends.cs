/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2016-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static partial class FrameEnumExtends
    {
        /// <summary>
        /// 获取枚举的 int 值
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int GetValue(this Enum e)
        {
            return Convert.ToInt32(e);
        }

        /// <summary>
        /// 根据 Value 获取对应的枚举值名称
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value">枚举值</param>
        /// <returns>对应的枚举名称</returns>
        public static string GetName(this Enum e, object value)
        {
            return Enum.GetName(e.GetType(), value);
        }

        /// <summary>
        /// 判断枚举值中是否存在指定的枚举
        /// 如 var t =（Type.Delete | Type.Add）
        ///         t.Has(Type.Delete)  ////返回 True
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="type">枚举</param>
        /// <param name="value">要判断的类型</param>
        /// <returns>true 为存在，</returns>
        public static bool Has<T>(this System.Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断枚举值是否是指定的枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Is<T>(this System.Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 向枚举值中添加枚举值，
        ///     如   Type.Delete.Add(Type.Add)
        ///         return Type.Delete | Type.Add
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Add<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format("不能添加类型 '{0}'", typeof(T).Name), ex);
            }
        }

        /// <summary>
        /// 从枚举中移除制定的枚举
        ///     如 (Type.Delete | Type.Add).Remove(Type.Add)
        ///         return Type.Delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Remove<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format("不能移除类型 '{0}'", typeof(T).Name), ex);
            }
        }

        /// <summary>
        /// 获取枚举的汉字说明，需要在枚举添加 [EnumDescriptionAttribute]
        /// </summary>
        /// <param name="enumSoruces"></param>
        /// <returns></returns>
        public static string GetDisplayText(this Enum enumSoruces)
        {            
            return EnumDescriptionAttribute.GetEnumDescription(enumSoruces);
        }

        /// <summary>
        /// 获取枚举类型的注释
        /// </summary>
        /// <param name="enumT">枚举值</param>
        /// <returns></returns>
        public static string GetTypeDescription(this Enum enumT)
        {
            return EnumDescriptionAttribute.GetEnumTyoeDescription(enumT.GetType());
        }
    }
}
