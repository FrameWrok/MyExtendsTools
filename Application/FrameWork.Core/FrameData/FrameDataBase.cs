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
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Reflection;
using FrameWork.Core.CoreAttribute;
using FrameWork.Core.HelperClass;
using SmartExtends.Frame;

namespace FrameWork.Core.FrameData
{
    /// <summary>
    /// 操作数据库类
    /// </summary>
    internal static partial class FrameDataBase
    {
        #region OR映射    由实体映射到数据库语句中的变量

        #region 系统统一部分

        /// <summary>
        /// 根据不同的映射类型及数据库类型返回实体对象的Parameter参数列表
        /// </summary>
        /// <param name="item">实体或匿名实体</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="smartMappingType">映射方式</param>
        /// <returns>Parameter参数列表</returns>
        internal static List<DbParameter> GetParameters(object item, DataBaseType smartDataBaseType, FrameMappingType smartMappingType)
        {
            return GetParameters(GetColumnDic(item, smartMappingType), smartDataBaseType);
        }

        /// <summary>
        /// 根据映射方式获取实体或匿名对象的字段以及对应值的 Dictionary 列表
        /// </summary>
        /// <param name="item">实体或匿名实体</param>
        /// <param name="smartMappingType">映射方式</param>
        /// <returns>字段或属性名称，值列表</returns>
        internal static Dictionary<string, object> GetColumnDic(object item, FrameMappingType smartMappingType)
        {
            Type itemType = item.GetType();
            Dictionary<string, object> list = new Dictionary<string, object>();
            switch (smartMappingType)
            {
                case FrameMappingType.FrameAttributePublic:     ////公有特性
                case FrameMappingType.FrameAttributePrivate:    ////私有特性
                case FrameMappingType.FrameAttribute:           ////根据特性映射，不区分公有私有
                    {
                        ////根据特性标识设置属性
                        foreach (PropertyInfo propertyInfo in FrameReflection.GetPropertyInfoByAttribute<FrameColumnMappingAttributes>(itemType, smartMappingType))
                        {
                            FrameColumnMappingAttributes attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(FrameColumnMappingAttributes), false) as FrameColumnMappingAttributes;
                            object value = propertyInfo.GetValue(item, null);
                            list.Add(attribute.ColumnName, value);
                        }
                        ////根据特性标识设置字段
                        foreach (FieldInfo fieldInfo in FrameReflection.GetFieldInfoByAttribute<FrameColumnMappingAttributes>(itemType, smartMappingType))
                        {
                            FrameColumnMappingAttributes attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(FrameColumnMappingAttributes), false) as FrameColumnMappingAttributes;
                            object value = fieldInfo.GetValue(item);
                            list.Add(attribute.ColumnName, value);
                        }
                    }

                    break;
                case FrameMappingType.XmlElementPublic:     ////公有XmlElement
                case FrameMappingType.XmlElementPrivate:    ////私有XmlElement
                case FrameMappingType.XmlElement:           ////根据XmlElement映射，不区分公有私有
                    {
                        ////根据特性标识设置属性
                        foreach (PropertyInfo propertyInfo in FrameReflection.GetPropertyInfoByAttribute<System.Xml.Serialization.XmlElementAttribute>(itemType, smartMappingType))
                        {
                            System.Xml.Serialization.XmlElementAttribute attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(System.Xml.Serialization.XmlElementAttribute), false) as System.Xml.Serialization.XmlElementAttribute;
                            object value = propertyInfo.GetValue(item, null);
                            list.Add(attribute.ElementName, value);
                        }
                        ////根据特性标识设置字段
                        foreach (FieldInfo fieldInfo in FrameReflection.GetFieldInfoByAttribute<System.Xml.Serialization.XmlElementAttribute>(itemType, smartMappingType))
                        {
                            System.Xml.Serialization.XmlElementAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(System.Xml.Serialization.XmlElementAttribute), false) as System.Xml.Serialization.XmlElementAttribute;
                            object value = fieldInfo.GetValue(item);
                            list.Add(attribute.ElementName, value);
                        }
                    }

                    break;
                case FrameMappingType.Field:            ////根据字段映射，私有公有均可
                case FrameMappingType.FieldPrivate:     ////根据私有字段映射
                case FrameMappingType.FieldPublic:      ////根据公有字段映射
                    {
                        foreach (FieldInfo fieldInfo in FrameReflection.GetFieldInfos(itemType, smartMappingType))
                        {
                            object value = fieldInfo.GetValue(item);
                            list.Add(fieldInfo.Name, value);
                        }
                    }

                    break;
                case FrameMappingType.Property:             ////根据属性映射   私有公有均可
                case FrameMappingType.PropertyPublic:       ////根据公有属性映射
                case FrameMappingType.ProertyPrivate:       ////根据私有属性映射
                    {
                        foreach (PropertyInfo propertyInfo in FrameReflection.GetPropertyInfos(itemType, smartMappingType))
                        {
                            object value = propertyInfo.GetValue(item, null);
                            list.Add(propertyInfo.Name, value);
                        }
                    }

                    break;
            }

            return list;
        }

        /// <summary>
        /// 根据不同的数据库类型返回实体对象的Parameter参数列表
        /// </summary>
        /// <param name="list">键值对</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>Parameter参数列表</returns>
        internal static List<DbParameter> GetParameters(Dictionary<string, object> list, DataBaseType smartDataBaseType)
        {
            List<DbParameter> listParameter = new List<DbParameter>();
            var items = list.GetEnumerator();
            DbProviderFactory dbproviderFactory = GetDbProviderFactoryByDataBaseType(smartDataBaseType);
            while (items.MoveNext())
            {
                string key = items.Current.Key;
                object value = items.Current.Value;
                ////根据数据库类型的数据库工厂创建Parameter
                DbParameter dbParameter = dbproviderFactory.CreateParameter();
                dbParameter.ParameterName = CreateParameterName(key, smartDataBaseType);

                ////判断参数类型
                if (value != null)
                    if (FrameParameterDirection.Output.ToString().Compare(value.ToString(), true))
                    {
                        dbParameter.Direction = ParameterDirection.Output;
                    }
                    else if (FrameParameterDirection.ReturnValue.ToString().Compare(value.ToString(), true))
                    {
                        dbParameter.Direction = ParameterDirection.ReturnValue;
                    }
                    else
                    {
                        dbParameter.Direction = ParameterDirection.Input;
                    }
                else
                    dbParameter.Direction = ParameterDirection.Input;

                if (value == null)
                    dbParameter.Value = DBNull.Value;
                else
                    dbParameter.Value = value;

                if (dbParameter.Direction == ParameterDirection.Output)
                    dbParameter.Size = int.MaxValue;

                listParameter.Add(dbParameter);
            }

            return listParameter;
        }

        #endregion

        #region 用户自定义部分

        /// <summary>
        /// 根据用户自定义特性返回实体或匿名对象的Parameter参数列表
        /// </summary>
        /// <typeparam name="TAttribute">用户自定义特性</typeparam>
        /// <param name="item">实体或匿名实体</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="mapingPropertyName">自定义特性映射属性名称</param>
        /// <returns>Parameter参数列表</returns>
        internal static List<DbParameter> GetParametersByUserAttribute<TAttribute>(object item, DataBaseType smartDataBaseType, string mapingPropertyName) where TAttribute : Attribute
        {
            return GetParameters(GetColumnDicByUserAttribute<TAttribute>(item, mapingPropertyName), smartDataBaseType);
        }

        /// <summary>
        /// 根据用户自定义特性获取实体或匿名对象的字段以及对应值的 Dictionary 列表
        /// </summary>
        /// <typeparam name="TAttribute">用户自定义特性</typeparam>
        /// <param name="item">实体或匿名实体</param>
        /// <param name="mapingPropertyName">自定应特性映射的字段</param>
        /// <returns>返回实体或匿名对象根据用户自定应特性获得字段以及值的列表</returns>
        internal static Dictionary<string, object> GetColumnDicByUserAttribute<TAttribute>(object item, string mapingPropertyName) where TAttribute : Attribute
        {
            Type attrType = typeof(TAttribute);
            Type itemType = item.GetType();
            Dictionary<string, object> list = new Dictionary<string, object>();
            string paramName = string.Empty;

            ////根据特性标识设置属性
            foreach (PropertyInfo propertyInfo in FrameReflection.GetPropertyInfoByAttribute<TAttribute>(itemType, FrameMappingType.FrameAttribute))
            {
                TAttribute attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(TAttribute), false) as TAttribute;
                object value = propertyInfo.GetValue(item, null);
                if (attribute != null)
                {
                    if (mapingPropertyName.IsNullOrEmptyOrBlank())
                    {
                        paramName = propertyInfo.Name;
                    }
                    else
                    {
                        PropertyInfo attrProperty = attrType.GetProperty(mapingPropertyName);
                        paramName = attrProperty.GetValue(attribute, null).ToString();
                    }

                    if (list.ContainsKey(paramName))
                    {
                        list[paramName] = value;
                    }
                    else
                    {
                        list.Add(paramName, value);
                    }
                }
            }
            ////根据特性标识设置字段
            foreach (FieldInfo fieldInfo in FrameReflection.GetFieldInfoByAttribute<TAttribute>(itemType, FrameMappingType.FrameAttribute))
            {
                TAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(TAttribute), false) as TAttribute;
                object value = fieldInfo.GetValue(item);
                if (attribute != null)
                {
                    if (mapingPropertyName.IsNullOrEmptyOrBlank())
                    {
                        paramName = fieldInfo.Name;
                    }
                    else
                    {
                        PropertyInfo attrProperty = attrType.GetProperty(mapingPropertyName);
                        paramName = attrProperty.GetValue(attribute, null).ToString();
                    }

                    if (list.ContainsKey(paramName))
                    {
                        list[paramName] = value;
                    }
                    else
                    {
                        list.Add(paramName, value);
                    }
                }
            }

            return list;
        }

        #endregion

        #endregion

        #region RO映射    由DataTable中的数据行映射成实体

        /// <summary>
        /// 由一行数据生成一个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dataRow">数据行</param>
        /// <param name="smartMappingType">反射方式</param>
        /// <returns>反射后的实体</returns>
        internal static T GetEntity<T>(DataRow dataRow, FrameMappingType smartMappingType)
        {
            T item = Activator.CreateInstance<T>();
            GetEntity<T>(dataRow, smartMappingType, ref item);
            return item;
        }

        /// <summary>
        /// 由一个DataRow集合返回一个实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dataRowCollection">数据行的集合</param>
        /// <param name="smartMappingType">反射的方式</param>
        /// <returns>反射后的实体列表</returns>
        internal static List<T> GetEntityList<T>(DataRowCollection dataRowCollection, FrameMappingType smartMappingType)
        {
            List<T> list = new List<T>();
            PropertyInfo[] propertyInfos = null;
            FieldInfo[] fieldInfos = null;
            foreach (DataRow row in dataRowCollection)
            {
                T item = Activator.CreateInstance<T>();
                GetEntity<T>(row, smartMappingType, ref item, propertyInfos, fieldInfos);
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 由一个DataTable返回一个实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <param name="smartMappingType">反射的方式</param>
        /// <returns>反射后的实体列表</returns>
        internal static List<T> GetEntityList<T>(DataTable dataTable, FrameMappingType smartMappingType)
        {
            if (dataTable.Rows.Count > 0)
                return GetEntityList<T>(dataTable.Rows, smartMappingType);
            return new List<T>();
        }

        /// <summary>
        /// 由一行数据生成一个实体
        /// </summary>
        /// <typeparam name="T">目标实体类型</typeparam>
        /// <param name="dataRow">数据行</param>
        /// <param name="smartMappingType">反射方式</param>
        /// <param name="item">已实例化的实体</param>
        /// <param name="propertyInfos">属性</param>
        /// <param name="fieldInfos">字段</param>
        private static void GetEntity<T>(DataRow dataRow, FrameMappingType smartMappingType, ref T item, PropertyInfo[] propertyInfos = null, FieldInfo[] fieldInfos = null)
        {
            Type itemType = item.GetType();
            switch (smartMappingType)
            {
                case FrameMappingType.FrameAttributePublic:     ////在公有字段或属性中查找特性
                case FrameMappingType.FrameAttributePrivate:    ////在私有字段或属性中查找特性
                case FrameMappingType.FrameAttribute:           ////在所有字段或属性中查找特性
                    {
                        ////设置属性的值

                        foreach (PropertyInfo propertyInfo in propertyInfos = propertyInfos != null ? propertyInfos : FrameReflection.GetPropertyInfoByAttribute<FrameColumnMappingAttributes>(itemType, smartMappingType))
                        {
                            FrameColumnMappingAttributes attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(FrameColumnMappingAttributes), false) as FrameColumnMappingAttributes;
                            if (dataRow.Table.Columns.Contains(attribute.ColumnName))
                            {
                                object value = dataRow[attribute.ColumnName];
                                if (value == null || value.ToString().Trim() == string.Empty)
                                {
                                    propertyInfo.SetValue(item, attribute.DefaultVale, null);
                                }
                                else
                                {
                                    propertyInfo.SetValue(item, (value.GetType() == propertyInfo.PropertyType ? value : FrameReflection.ConvertTo(value.ToString().Trim(), propertyInfo.PropertyType)), null);
                                }
                            }
                            else
                                propertyInfo.SetValue(item, attribute.DefaultVale, null);
                        }
                        ////设置字段的值
                        foreach (FieldInfo fieldInfo in fieldInfos = fieldInfos != null ? fieldInfos : FrameReflection.GetFieldInfoByAttribute<FrameColumnMappingAttributes>(itemType, smartMappingType))
                        {
                            FrameColumnMappingAttributes attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(FrameColumnMappingAttributes), false) as FrameColumnMappingAttributes;
                            if (dataRow.Table.Columns.Contains(attribute.ColumnName))
                            {
                                object value = dataRow[attribute.ColumnName];
                                if (value == null || value.ToString().Trim() == string.Empty)
                                {
                                    fieldInfo.SetValue(item, attribute.DefaultVale);
                                }
                                else
                                {
                                    fieldInfo.SetValue(item, (value.GetType() == fieldInfo.FieldType ? value : FrameReflection.ConvertTo(value.ToString().Trim(), fieldInfo.FieldType)));
                                }
                            }
                            else
                                fieldInfo.SetValue(item, attribute.DefaultVale);
                        }
                    }

                    break;

                case FrameMappingType.XmlElement:
                case FrameMappingType.XmlElementPrivate:
                case FrameMappingType.XmlElementPublic:
                    {
                        ////设置属性的值
                        foreach (PropertyInfo propertyInfo in propertyInfos = propertyInfos != null ? propertyInfos : FrameReflection.GetPropertyInfoByAttribute<System.Xml.Serialization.XmlElementAttribute>(itemType, smartMappingType))
                        {
                            System.Xml.Serialization.XmlElementAttribute attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(System.Xml.Serialization.XmlElementAttribute), false) as System.Xml.Serialization.XmlElementAttribute;
                            if (dataRow.Table.Columns.Contains(attribute.ElementName))
                            {
                                object value = dataRow[attribute.ElementName];
                                if (value == null || value.ToString().Trim() == string.Empty)
                                {
                                    propertyInfo.SetValue(item, null, null);
                                }
                                else
                                {
                                    propertyInfo.SetValue(item, (value.GetType() == propertyInfo.PropertyType ? value : FrameReflection.ConvertTo(value.ToString().Trim(), propertyInfo.PropertyType)), null);
                                }
                            }
                            else
                                propertyInfo.SetValue(item, null, null);
                        }
                        ////设置字段的值
                        foreach (FieldInfo fieldInfo in fieldInfos = fieldInfos != null ? fieldInfos : FrameReflection.GetFieldInfoByAttribute<System.Xml.Serialization.XmlElementAttribute>(itemType, smartMappingType))
                        {
                            System.Xml.Serialization.XmlElementAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(System.Xml.Serialization.XmlElementAttribute), false) as System.Xml.Serialization.XmlElementAttribute;
                            if (dataRow.Table.Columns.Contains(attribute.ElementName))
                            {
                                object value = dataRow[attribute.ElementName];
                                if (value == null || value.ToString().Trim() == string.Empty)
                                {
                                    fieldInfo.SetValue(item, null);
                                }
                                else
                                {
                                    fieldInfo.SetValue(item, (value.GetType() == fieldInfo.FieldType ? value : FrameReflection.ConvertTo(value.ToString().Trim(), fieldInfo.FieldType)));
                                }
                            }
                            else
                                fieldInfo.SetValue(item, null);
                        }
                    }

                    break;

                case FrameMappingType.Field:    ////按字段映射，不区分公有私有
                case FrameMappingType.FieldPrivate: ////按私有字段映射
                case FrameMappingType.FieldPublic:  ////按公有字段映射
                    {
                        ////设置字段的值
                        foreach (FieldInfo fieldInfo in fieldInfos = fieldInfos != null ? fieldInfos : FrameReflection.GetFieldInfos(itemType, smartMappingType))
                        {
                            if (dataRow.Table.Columns.Contains(fieldInfo.Name))
                            {
                                object value = dataRow[fieldInfo.Name];
                                if (value != null && value.ToString().Trim() != string.Empty)
                                    fieldInfo.SetValue(item, (value.GetType() == fieldInfo.FieldType ? value : FrameReflection.ConvertTo(value.ToString(), fieldInfo.FieldType)));
                            }
                        }
                    }

                    break;

                case FrameMappingType.Property:
                case FrameMappingType.ProertyPrivate:
                case FrameMappingType.PropertyPublic:
                    {
                        ////设置属性的值
                        foreach (PropertyInfo propertyInfo in propertyInfos = propertyInfos != null ? propertyInfos : FrameReflection.GetPropertyInfos(itemType, smartMappingType))
                        {
                            if (dataRow.Table.Columns.Contains(propertyInfo.Name))
                            {
                                object value = dataRow[propertyInfo.Name];
                                if (value != null && value.ToString().Trim() != string.Empty)
                                    propertyInfo.SetValue(item, (value.GetType() == propertyInfo.PropertyType ? value : FrameReflection.ConvertTo(value.ToString(), propertyInfo.PropertyType)), null);
                            }
                        }
                    }

                    break;
            }
        }

        #endregion
    }

    /// <summary>
    /// 操作数据库类
    /// </summary>
    internal static partial class FrameDataBase
    {
        #region     Fill 方法

        /// <summary>
        /// 由连接字符串，sql语句，Parameter列表，数据库类型填充 DataSet
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">sql查询语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="dataSet">要填充的 DataSet</param>        
        /// <param name="listParameter">参数列表</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="tableName">填充的表名称</param>
        internal static void Fill(string connectionString, string sqlSentence, CommandType commandType, DataSet dataSet, List<DbParameter> listParameter, DataBaseType smartDataBaseType, string tableName)
        {
            DbProviderFactory dbFactory = GetDbProviderFactoryByDataBaseType(smartDataBaseType);
            DbConnection dbConnection = dbFactory.CreateConnection();
            dbConnection.ConnectionString = connectionString;
            using (DbCommand dbcommand = dbConnection.CreateCommand())
            {
                dbcommand.CommandType = commandType;
                dbcommand.CommandText = sqlSentence;
                dbcommand.CommandTimeout = 180;
                if (listParameter != null && listParameter.Count > 0)
                {
                    dbcommand.Parameters.AddRange(listParameter.ToArray());
                }

                using (DbDataAdapter dataAdapter = dbFactory.CreateDataAdapter())
                {
                    dataAdapter.SelectCommand = dbcommand;
                    DataTable dt = new DataTable();
                    if (!tableName.IsNullOrEmptyOrBlank() && dt.TableName != tableName)
                        dt.TableName = tableName;
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);
                    if (!tableName.IsNullOrEmptyOrBlank() && !dataSet.Tables.Contains(tableName) && ds.Tables.Count > 0 && !ds.Tables.Contains(tableName))
                        ds.Tables[0].TableName = tableName;
                    if (dataSet == null)
                        dataSet = new DataSet();
                    if (ds.Tables.Count < 1)
                        return;
                    foreach (DataTable db in ds.Tables)
                    {
                        if (dataSet.Tables.Contains(db.TableName))
                            db.TableName = "Table" + (dataSet.Tables.Count + 1);
                        dataSet.Tables.Add(db.Copy());
                    }
                }
            }
        }

        /// <summary>
        /// 由连接字符串，sql语句，数据库类型填充  DataSet ,默认执行sql语句
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">sql查询语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="dataSet">要填充的 DataSet</param>          
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="tableName">填充的表名称</param>
        internal static void Fill(string connectionString, string sqlSentence, CommandType commandType, DataSet dataSet, DataBaseType smartDataBaseType, string tableName)
        {
            Fill(connectionString, sqlSentence, commandType, dataSet, null, smartDataBaseType, tableName);
        }

        /// <summary>
        /// 由数据库连接字符串，sql查询语句，实体或匿名类型，映射方式，数据库类型填充 DataSet
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库查询语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="dataSet">DataSet</param>
        /// <param name="item">实体或匿名类型</param>
        /// <param name="smartMappingType">映射方式</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="tableName">填充的表名称</param>
        internal static void Fill(string connectionString, string sqlSentence, CommandType commandType, DataSet dataSet, object item, FrameMappingType smartMappingType, DataBaseType smartDataBaseType, string tableName)
        {
            List<DbParameter> listParameter = GetParameters(item, smartDataBaseType, smartMappingType);
            Fill(connectionString, sqlSentence, commandType, dataSet, listParameter, smartDataBaseType, tableName);
        }

        /// <summary>
        /// 由连接字符串，sql语句，Parameter列表，数据库类型填充 DataTable
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">sql查询语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="dataTable">要填充的 DataTable</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="listParameter">参数列表</param>
        /// <param name="tableName">填充的表名称</param>
        internal static void Fill(string connectionString, string sqlSentence, CommandType commandType, ref DataTable dataTable, DataBaseType smartDataBaseType, List<DbParameter> listParameter, string tableName)
        {
            DbProviderFactory dbFactory = GetDbProviderFactoryByDataBaseType(smartDataBaseType);
            DbConnection dbConnection = dbFactory.CreateConnection();
            dbConnection.ConnectionString = connectionString;
            using (DbCommand dbcommand = dbConnection.CreateCommand())
            {
                dbcommand.CommandType = commandType;
                dbcommand.CommandText = sqlSentence;
                dbcommand.CommandTimeout = 180;
                if (listParameter != null && listParameter.Count > 0)
                {
                    dbcommand.Parameters.AddRange(listParameter.ToArray());
                }

                using (DbDataAdapter dataAdapter = dbFactory.CreateDataAdapter())
                {
                    dataAdapter.SelectCommand = dbcommand;
                    dataAdapter.Fill(dataTable);
                    if (!tableName.IsNullOrEmptyOrBlank() && dataTable.TableName != tableName)
                        dataTable.TableName = tableName;
                }
            }
        }

        /// <summary>
        /// 由连接字符串，sql语句，Parameter列表，数据库类型填充DataTable
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">sql查询语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="dataTable">要填充的 DataTable</param>        
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="tableName">填充的表名称</param>
        internal static void Fill(string connectionString, string sqlSentence, CommandType commandType, ref DataTable dataTable, DataBaseType smartDataBaseType, string tableName)
        {
            Fill(connectionString, sqlSentence, commandType, ref dataTable, smartDataBaseType, null, tableName);
        }

        /// <summary>
        /// 由数据库连接字符串，sql查询语句，实体或匿名类型，映射方式，数据库类型填充 DataTable
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库查询语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="dataTable">来源数据表</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="item">实体或匿名类型</param>
        /// <param name="smartMappingType">映射方式</param>
        /// <param name="tableName">填充的表名称</param>
        internal static void Fill(string connectionString, string sqlSentence, CommandType commandType, ref DataTable dataTable, DataBaseType smartDataBaseType, object item, FrameMappingType smartMappingType, string tableName)
        {
            List<DbParameter> listParameter = GetParameters(item, smartDataBaseType, smartMappingType);
            Fill(connectionString, sqlSentence, commandType, ref dataTable, smartDataBaseType, listParameter, tableName);
        }

        #endregion

        #region   ExecuteNonQuery方法

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程或 SQL语句
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="listParameter">参数列表</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>返回影响的行数</returns>
        internal static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter, DataBaseType smartDataBaseType)
        {
            DbProviderFactory dbProviderFactory = GetDbProviderFactoryByDataBaseType(smartDataBaseType);
            using (DbConnection dbConnection = dbProviderFactory.CreateConnection())
            {
                try
                {
                    dbConnection.ConnectionString = connectionString;
                    using (DbCommand dbCommand = dbConnection.CreateCommand())
                    {
                        dbCommand.CommandType = commandType;
                        dbCommand.CommandTimeout = 180;
                        dbCommand.CommandText = sqlSentence;
                        if (listParameter != null && listParameter.Count > 0)
                        {
                            dbCommand.Parameters.AddRange(listParameter.ToArray());
                        }

                        dbConnection.Open();
                        return dbCommand.ExecuteNonQuery();
                    }
                }
                catch (DbException dex)
                {
                    throw dex;
                }
                finally
                {
                    if (!ConnectionState.Closed.Equals(dbConnection.State))
                        dbConnection.Close();
                }
            }
        }

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程或 SQL语句
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>返回影响的行数</returns>
        internal static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType, DataBaseType smartDataBaseType)
        {
            return ExecuteNonQuery(connectionString, sqlSentence, commandType, null, smartDataBaseType);
        }

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程或 SQL语句，默认是 SqlServer数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns>返回影响的行数</returns>
        internal static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter)
        {
            return ExecuteNonQuery(connectionString, sqlSentence, commandType, listParameter, DataBaseType.SqlServer);
        }

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程或 SQL语句，默认是 SqlServer数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <returns>返回影响的行数</returns>
        internal static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType)
        {
            return ExecuteNonQuery(connectionString, sqlSentence, commandType, null, DataBaseType.SqlServer);
        }

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程或 SQL语句
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="item">实体或者匿名类型</param>
        /// <param name="smartMappingType">映射方式</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>返回影响的行数</returns>
        internal static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType, object item, FrameMappingType smartMappingType, DataBaseType smartDataBaseType)
        {
            List<DbParameter> listDbParameter = GetParameters(item, smartDataBaseType, smartMappingType);
            return ExecuteNonQuery(connectionString, sqlSentence, commandType, listDbParameter, smartDataBaseType);
        }

        #endregion

        #region   ExecuteReader方法

        /// <summary>
        /// 执行存储过程或 SQL语句返回从数据源读取行的只进流（DbDataReader）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="listParameter">参数列表</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>从数据源读取行的只进流</returns>
        internal static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter, DataBaseType smartDataBaseType)
        {
            DbProviderFactory dbProviderFactory = GetDbProviderFactoryByDataBaseType(smartDataBaseType);

            using (DbConnection dbConnection = dbProviderFactory.CreateConnection())
            {
                try
                {
                    dbConnection.ConnectionString = connectionString;
                    using (DbCommand dbCommand = dbConnection.CreateCommand())
                    {
                        dbCommand.CommandType = commandType;
                        dbCommand.CommandTimeout = 180;
                        dbCommand.CommandText = sqlSentence;
                        if (listParameter != null && listParameter.Count > 0)
                        {
                            dbCommand.Parameters.AddRange(listParameter.ToArray());
                        }

                        dbConnection.Open();
                        return dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                }
                catch (DbException dex)
                {
                    throw dex;
                }
                finally
                {
                    if (!ConnectionState.Closed.Equals(dbConnection.State))
                        dbConnection.Close();
                }
            }
        }

        /// <summary>
        /// 执行存储过程或 SQL语句返回从数据源读取行的只进流（DbDataReader）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>从数据源读取行的只进流</returns>
        internal static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType, DataBaseType smartDataBaseType)
        {
            return ExecuteReader(connectionString, sqlSentence, commandType, null, smartDataBaseType);
        }

        /// <summary>
        /// 执行存储过程或 SQL语句返回从数据源读取行的只进流（DbDataReader），默认是 SqlServer数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns>从数据源读取行的只进流</returns>
        internal static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter)
        {
            return ExecuteReader(connectionString, sqlSentence, commandType, listParameter, DataBaseType.SqlServer);
        }

        /// <summary>
        /// 执行存储过程或 SQL语句返回从数据源读取行的只进流（DbDataReader），默认是 SqlServer数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <returns>从数据源读取行的只进流</returns>
        internal static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType)
        {
            return ExecuteReader(connectionString, sqlSentence, commandType, null, DataBaseType.SqlServer);
        }

        /// <summary>
        /// 执行存储过程或 SQL语句返回从数据源读取行的只进流（DbDataReader）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="item">实体或者匿名类型</param>
        /// <param name="smartMappingType">映射方式</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>从数据源读取行的只进流</returns>
        internal static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType, object item, FrameMappingType smartMappingType, DataBaseType smartDataBaseType)
        {
            List<DbParameter> listDbParameter = GetParameters(item, smartDataBaseType, smartMappingType);
            return ExecuteReader(connectionString, sqlSentence, commandType, listDbParameter, smartDataBaseType);
        }

        #endregion

        #region  ExecuteScalar方法

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。
        /// 结果是DBNull类型或者是空，则返回null
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">执行的Sql语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="listParameter">参数列表</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>返回的结果集中第一行的第一列</returns>
        internal static object ExecuteScalar(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter, DataBaseType smartDataBaseType)
        {
            object result = null;
            DbProviderFactory dbProviderFactory = GetDbProviderFactoryByDataBaseType(smartDataBaseType);
            using (DbConnection dbConnection = dbProviderFactory.CreateConnection())
            {
                dbConnection.ConnectionString = connectionString;
                using (DbCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandType = commandType;
                    dbCommand.CommandText = sqlSentence;
                    if (listParameter != null || listParameter.Count > 0)
                    {
                        dbCommand.Parameters.AddRange(listParameter.ToArray());
                    }

                    dbConnection.Open();
                    result = dbCommand.ExecuteScalar();
                }
            }

            ////如果结果是DBNull类型或者是空，则返回null
            if (result == null || result == DBNull.Value || result.ToString().Trim() == string.Empty)
                result = null;
            return result;
        }

        /// <summary>
        /// 根据实体或匿名类型，映射方式 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。
        /// 结果是DBNull类型或者是空，则返回null
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">执行的Sql语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="item">实体或匿名类型</param>
        /// <param name="smartMappingType">映射方式</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>返回的结果集中第一行的第一列</returns>
        internal static object ExecuteScalar(string connectionString, string sqlSentence, CommandType commandType, object item, FrameMappingType smartMappingType, DataBaseType smartDataBaseType)
        {
            List<DbParameter> listDbParameter = GetParameters(item, smartDataBaseType, smartMappingType);
            return ExecuteScalar(connectionString, sqlSentence, commandType, listDbParameter, smartDataBaseType);
        }

        #endregion

        #region 基于事务

        /// <summary>
        /// 基于事务的 ExcuteNonQuery 
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlDic">执行的sql语句字典值 key为sql语句，value为参数Parameter列表</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="useTransaction">是否开启事务</param>
        internal static void ExecuteNonQueryUseTransaction(string connectionString, Dictionary<string, List<DbParameter>> sqlDic, DataBaseType smartDataBaseType, bool useTransaction)
        {
            DbProviderFactory dbProviderFactory = GetDbProviderFactoryByDataBaseType(smartDataBaseType);
            using (DbConnection dbConnection = dbProviderFactory.CreateConnection())
            {
                dbConnection.ConnectionString = connectionString;
                using (DbCommand dbCommand = dbConnection.CreateCommand())
                {
                    DbTransaction tran = null;
                    try
                    {
                        dbConnection.Open();
                        if (useTransaction)
                            tran = dbConnection.BeginTransaction();
                        var sqlItem = sqlDic.GetEnumerator();
                        while (sqlItem.MoveNext())
                        {
                            dbCommand.CommandType = CommandType.Text;
                            dbCommand.CommandTimeout = 180;
                            dbCommand.CommandText = sqlItem.Current.Key;
                            if (sqlItem.Current.Value != null && sqlItem.Current.Value.Count > 0)
                            {
                                dbCommand.Parameters.AddRange(sqlItem.Current.Value.ToArray());
                            }

                            dbCommand.ExecuteNonQuery();
                        }

                        if (tran != null)
                            tran.Commit();
                    }
                    catch (DbException dbex)
                    {
                        if (tran != null && !ConnectionState.Closed.Equals(dbConnection.State))
                            tran.Rollback();
                        throw dbex;
                    }
                    finally
                    {
                        if (!ConnectionState.Closed.Equals(dbConnection.State))
                            dbConnection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 基于事务的 ExcuteNonQuery 
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlDic">执行的sql语句字典值 key为sql语句，value为参数的列表</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="useTransaction">是否开启事务</param>
        internal static void ExecuteNonQueryUseTransaction(string connectionString, Dictionary<string, Dictionary<object, FrameMappingType>> sqlDic, DataBaseType smartDataBaseType, bool useTransaction)
        {
            Dictionary<string, List<DbParameter>> sqlDicItem = new Dictionary<string, List<DbParameter>>();
            List<DbParameter> paraList = new List<DbParameter>();
            var sqlEnumerator = sqlDic.GetEnumerator();
            while (sqlEnumerator.MoveNext())
            {
                var itemEnumerator = sqlEnumerator.Current.Value.GetEnumerator();
                while (itemEnumerator.MoveNext())
                {
                    paraList = GetParameters(itemEnumerator.Current.Key, smartDataBaseType, itemEnumerator.Current.Value);
                }

                sqlDicItem.Add(sqlEnumerator.Current.Key, paraList);
            }

            ExecuteNonQueryUseTransaction(connectionString, sqlDicItem, smartDataBaseType, useTransaction);
        }

        #endregion
    }

    /// <summary>
    /// 操作数据库类
    /// </summary>
    internal static partial class FrameDataBase
    {
        #region 其他方法

        /// <summary>
        /// 根据数据库的类型返回Parameter的参数名称
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>合法的参数名称</returns>
        internal static string CreateParameterName(string columnName, DataBaseType smartDataBaseType)
        {
            string parameterName = string.Empty;
            switch (smartDataBaseType)
            {
                case DataBaseType.SqlServer:
                    {
                        parameterName = "@" + columnName.Trim().TrimStart(new char[] { '@' });
                    }

                    break;

                case DataBaseType.PostgreSQL:
                case DataBaseType.Oracle:
                    {
                        parameterName = ":" + columnName.Trim().TrimStart(new char[] { ':' });
                    }

                    break;

                case DataBaseType.Oledb:
                    {
                        parameterName = columnName.Trim();
                    }

                    break;
            }

            return parameterName;
        }

        /// <summary>
        /// 根据数据库的类型返回数据库工厂
        /// </summary>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>数据工厂库</returns>
        private static DbProviderFactory GetDbProviderFactoryByDataBaseType(DataBaseType smartDataBaseType)
        {
            DbProviderFactory dbProviderFactory = null;
            switch (smartDataBaseType)
            {
                case DataBaseType.SqlServer:
                    {
                        dbProviderFactory = SqlClientFactory.Instance;
                    }

                    break;

                case DataBaseType.Oracle:
                    {
                        ////dbProviderFactory = Oracle.DataAccess.Client.OracleClientFactory.Instance;
                    }

                    break;

                case DataBaseType.Oledb:
                    {
                        dbProviderFactory = OleDbFactory.Instance;
                    }

                    break;

                case DataBaseType.PostgreSQL:
                    {
                       //// dbProviderFactory = Npgsql.NpgsqlFactory.Instance;
                    }

                    break;
            }

            return dbProviderFactory;
        }

        #endregion
    }
}
