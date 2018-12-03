/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Data;
using System.IO;
using System.Xml;
using SmartExtends.Resources.Exceptions;

namespace System.Data
{
    /// <summary>
    /// DataSet的方法扩展类
    /// </summary>
    public static partial class FrameDataSetExtends
    {
        /// <summary>
        /// 获取存取在DataSet的数据的xml表现形式
        /// </summary>
        /// <param name="dataSet">数据源DataSet</param>
        /// <param name="mappingType">列的映射方式</param>
        /// <returns>DataSet中的数据的xml表现形式</returns>
        public static string GetXml(this DataSet dataSet, MappingType mappingType)
        {
            FrameDataSetExtends.SetDataSetColumnElementToAttribute(dataSet, mappingType);
            return dataSet.GetXml();
        }

        /// <summary>
        /// 获取存取在DataSet的数据的xml表现形式，并将列映射为xml中的属性
        /// </summary>
        /// <param name="dataSet">>数据源DataSet</param>
        /// <param name="xmlWriteMode">指定如何从DataSet写入XML数据和关系架构</param>
        /// <returns>DataSet中的数据的xml表现形式</returns>
        public static string GetXmlColumnToAtribute(this DataSet dataSet, XmlWriteMode xmlWriteMode)
        {
            return GetXml(dataSet, MappingType.Attribute, xmlWriteMode);
        }

        /// <summary>
        /// 获取存取在DataSet的数据的xml表现形式，并将列映射为xml中的属性
        /// </summary>
        /// <param name="dataSet">>数据源DataSet</param>        
        /// <returns>DataSet中的数据的xml表现形式</returns>
        public static string GetXmlColumnToAtribute(this DataSet dataSet)
        {
            return GetXml(dataSet, MappingType.Attribute);
        }

        /// <summary>
        /// 获取存取在DataSet的数据的xml表现形式
        /// </summary>
        /// <param name="dataSet">数据源DataSet</param>
        /// <param name="mappingType">映射DataColumn的方式</param>
        /// <param name="xmlWriteMode">指定如何从DataSet写入xml数据和关系架构</param>
        /// <returns>DataSet中的数据的xml表现形式</returns>
        public static string GetXml(this DataSet dataSet, MappingType mappingType, XmlWriteMode xmlWriteMode)
        {
            FrameDataSetExtends.SetDataSetColumnElementToAttribute(dataSet, mappingType);
            MemoryStream ms = new MemoryStream();
            XmlDocument dom = new XmlDocument();
            try
            {
                dataSet.WriteXml(ms, xmlWriteMode);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                dom.Load(ms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }

            return dom.InnerXml;
        }

        /// <summary>
        /// 设置DataSet中的Column映射到xml中的方式
        /// </summary>
        /// <param name="dataSet">数据源</param>
        /// <param name="mappingType">映射DataColumn的方式</param>
        private static void SetDataSetColumnElementToAttribute(DataSet dataSet, MappingType mappingType)
        {
            if (dataSet == null)
                throw new Exception(FrameCHExceptionResource.__FrameDataSetGetXmlDataSourceDontNull);
            foreach (DataTable db in dataSet.Tables)
            {
                foreach (DataColumn c in db.Columns)
                {
                    c.ColumnMapping = mappingType;
                }
            }
        }
    }
}
