using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FrameWork.Core.FrameData;
using SmartExtends.Frame;

namespace FrameWork.Core
{
    /// <summary>
    /// 操作数据库类
    /// </summary>
    public static partial class DataCommon
    {
        /// <summary>
        /// 框架默认数据库类型
        /// </summary>
        public static FrameData.DataBaseType FrameDataBaseType;

        /// <summary>
        /// 框架默认数据库连接字符串
        /// </summary>
        public static string DataBaseConnectionString;

        /// <summary>
        /// 程序默认的映射类型
        /// </summary>
        public static FrameMappingType MappingType;

        /// <summary>
        /// 根据不同的映射类型及数据库类型返回实体对象的Parameter参数列表
        /// </summary>
        /// <param name="item">实体或匿名实体</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="smartMappingType">映射方式</param>
        /// <returns>Parameter参数列表</returns>
        public static List<DbParameter> GetParameters(object item, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, FrameMappingType smartMappingType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.GetParameters(item, smartDataBaseType, smartMappingType);
        }

        /// <summary>
        /// 根据不同的数据库类型返回实体对象的Parameter参数列表
        /// </summary>
        /// <param name="list">键值对</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>Parameter参数列表</returns>
        public static List<DbParameter> GetParameters(Dictionary<string, object> list, FrameWork.Core.FrameData.DataBaseType smartDataBaseType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.GetParameters(list, smartDataBaseType);
        }

        /// <summary>
        /// 根据不同的映射类型及数据库类型返回实体对象的Parameter参数列表
        /// </summary>
        /// <typeparam name="TAttribute">用户自定义特性</typeparam>
        /// <param name="item">实体或匿名实体</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="mapingPropertyName">自定义特性映射属性名称</param>
        /// <returns>Parameter参数列表</returns>
        public static List<DbParameter> GetParametersByUserAttribute<TAttribute>(object item, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, string mapingPropertyName) where TAttribute : Attribute
        {
            return FrameWork.Core.FrameData.FrameDataBase.GetParametersByUserAttribute<TAttribute>(item, smartDataBaseType, mapingPropertyName);
        }

        /// <summary>
        /// 由一行数据生成一个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dataRow">数据行</param>
        /// <param name="smartMappingType">反射方式</param>
        /// <returns>反射后的实体</returns>
        public static T GetEntity<T>(DataRow dataRow, FrameMappingType smartMappingType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.GetEntity<T>(dataRow, smartMappingType);
        }

        /// <summary>
        /// 由一行数据生成一个实体,获取第一行数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dataRowCollection">数据行的集合</param>
        /// <param name="smartMappingType">反射方式</param>
        /// <returns>反射后的实体</returns>
        public static T GetEntity<T>(DataRowCollection dataRowCollection, FrameMappingType smartMappingType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.GetEntity<T>(dataRowCollection[0], smartMappingType);
        }

        /// <summary>
        /// 由一行数据生成一个实体,获取第一行数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <param name="smartMappingType">反射方式</param>
        /// <returns>反射后的实体</returns>
        public static T GetEntity<T>(DataTable dataTable, FrameMappingType smartMappingType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.GetEntity<T>(dataTable.Rows[0], smartMappingType);
        }

        /// <summary>
        /// 由一个DataRow集合返回一个实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dataRowCollection">数据行的集合</param>
        /// <param name="smartMappingType">反射的方式</param>
        /// <returns>反射后的实体列表</returns>
        public static List<T> GetEntityList<T>(DataRowCollection dataRowCollection, FrameMappingType smartMappingType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.GetEntityList<T>(dataRowCollection, smartMappingType);
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
            return FrameWork.Core.FrameData.FrameDataBase.GetEntityList<T>(dataTable, smartMappingType);
        }
    }

    /// <summary>
    /// 操作数据库类
    /// </summary>
    public static partial class DataCommon
    {
        #region Fill方法

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
        public static void Fill(string connectionString, string sqlSentence, CommandType commandType, DataSet dataSet, List<DbParameter> listParameter, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, string tableName)
        {
            FrameWork.Core.FrameData.FrameDataBase.Fill(connectionString, sqlSentence, commandType, dataSet, listParameter, smartDataBaseType, tableName);
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
        public static void Fill(string connectionString, string sqlSentence, CommandType commandType, DataSet dataSet, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, string tableName)
        {
            FrameWork.Core.FrameData.FrameDataBase.Fill(connectionString, sqlSentence, commandType, dataSet, smartDataBaseType, tableName);
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
        public static void Fill(string connectionString, string sqlSentence, CommandType commandType, DataSet dataSet, object item, FrameMappingType smartMappingType, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, string tableName)
        {
            FrameWork.Core.FrameData.FrameDataBase.Fill(connectionString, sqlSentence, commandType, dataSet, item, smartMappingType, smartDataBaseType, tableName);
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
        public static void Fill(string connectionString, string sqlSentence, CommandType commandType, ref DataTable dataTable, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, List<DbParameter> listParameter, string tableName)
        {
            FrameWork.Core.FrameData.FrameDataBase.Fill(connectionString, sqlSentence, commandType, ref dataTable, smartDataBaseType, listParameter, tableName);
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
        public static void Fill(string connectionString, string sqlSentence, CommandType commandType, ref DataTable dataTable, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, string tableName)
        {
            FrameWork.Core.FrameData.FrameDataBase.Fill(connectionString, sqlSentence, commandType, ref dataTable, smartDataBaseType, tableName);
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
        public static void Fill(string connectionString, string sqlSentence, CommandType commandType, ref DataTable dataTable, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, object item, FrameMappingType smartMappingType, string tableName)
        {
            FrameWork.Core.FrameData.FrameDataBase.Fill(connectionString, sqlSentence, commandType, ref dataTable, smartDataBaseType, item, smartMappingType, tableName);
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
        public static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter, FrameWork.Core.FrameData.DataBaseType smartDataBaseType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteNonQuery(connectionString, sqlSentence, commandType, listParameter, smartDataBaseType);
        }

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程或 SQL语句
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType, FrameWork.Core.FrameData.DataBaseType smartDataBaseType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteNonQuery(connectionString, sqlSentence, commandType, null, smartDataBaseType);
        }

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程或 SQL语句，默认是 SqlServer数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter)
        {
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteNonQuery(connectionString, sqlSentence, commandType, listParameter, FrameWork.Core.FrameData.DataBaseType.SqlServer);
        }

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程或 SQL语句，默认是 SqlServer数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteNonQuery(connectionString, sqlSentence, commandType, null, FrameWork.Core.FrameData.DataBaseType.SqlServer);
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
        public static int ExecuteNonQuery(string connectionString, string sqlSentence, CommandType commandType, object item, FrameMappingType smartMappingType, FrameWork.Core.FrameData.DataBaseType smartDataBaseType)
        {
            List<DbParameter> listDbParameter = FrameWork.Core.FrameData.FrameDataBase.GetParameters(item, smartDataBaseType, smartMappingType);
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteNonQuery(connectionString, sqlSentence, commandType, listDbParameter, smartDataBaseType);
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
        public static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter, FrameWork.Core.FrameData.DataBaseType smartDataBaseType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteReader(connectionString, sqlSentence, commandType, listParameter, smartDataBaseType);
        }

        /// <summary>
        /// 执行存储过程或 SQL语句返回从数据源读取行的只进流（DbDataReader）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <returns>从数据源读取行的只进流</returns>
        public static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType, FrameWork.Core.FrameData.DataBaseType smartDataBaseType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteReader(connectionString, sqlSentence, commandType, null, smartDataBaseType);
        }

        /// <summary>
        /// 执行存储过程或 SQL语句返回从数据源读取行的只进流（DbDataReader），默认是 SqlServer数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns>从数据源读取行的只进流</returns>
        public static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter)
        {
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteReader(connectionString, sqlSentence, commandType, listParameter, FrameWork.Core.FrameData.DataBaseType.SqlServer);
        }

        /// <summary>
        /// 执行存储过程或 SQL语句返回从数据源读取行的只进流（DbDataReader），默认是 SqlServer数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlSentence">数据库执行语句</param>
        /// <param name="commandType">sql语句类型，是存储过程还是sql语句</param>
        /// <returns>从数据源读取行的只进流</returns>
        public static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteReader(connectionString, sqlSentence, commandType, null, FrameWork.Core.FrameData.DataBaseType.SqlServer);
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
        public static DbDataReader ExecuteReader(string connectionString, string sqlSentence, CommandType commandType, object item, FrameMappingType smartMappingType, FrameWork.Core.FrameData.DataBaseType smartDataBaseType)
        {
            List<DbParameter> listDbParameter = FrameWork.Core.FrameData.FrameDataBase.GetParameters(item, smartDataBaseType, smartMappingType);
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteReader(connectionString, sqlSentence, commandType, listDbParameter, smartDataBaseType);
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
        public static object ExecuteScalar(string connectionString, string sqlSentence, CommandType commandType, List<DbParameter> listParameter, FrameWork.Core.FrameData.DataBaseType smartDataBaseType)
        {
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteScalar(connectionString, sqlSentence, commandType, listParameter, smartDataBaseType);
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
        public static object ExecuteScalar(string connectionString, string sqlSentence, CommandType commandType, object item, FrameMappingType smartMappingType, FrameWork.Core.FrameData.DataBaseType smartDataBaseType)
        {
            List<DbParameter> listDbParameter = FrameWork.Core.FrameData.FrameDataBase.GetParameters(item, smartDataBaseType, smartMappingType);
            return FrameWork.Core.FrameData.FrameDataBase.ExecuteScalar(connectionString, sqlSentence, commandType, listDbParameter, smartDataBaseType);
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
        public static void ExecuteNonQueryUseTransaction(string connectionString, Dictionary<string, List<DbParameter>> sqlDic, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, bool useTransaction)
        {
            FrameWork.Core.FrameData.FrameDataBase.ExecuteNonQueryUseTransaction(connectionString, sqlDic, smartDataBaseType, useTransaction);
        }

        /// <summary>
        /// 基于事务的 ExcuteNonQuery 
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlDic">执行的sql语句字典值 key为sql语句，value为参数的列表</param>
        /// <param name="smartDataBaseType">数据库类型</param>
        /// <param name="useTransaction">是否开启事务</param>
        public static void ExecuteNonQueryUseTransaction(string connectionString, Dictionary<string, Dictionary<object, FrameMappingType>> sqlDic, FrameWork.Core.FrameData.DataBaseType smartDataBaseType, bool useTransaction)
        {
            FrameWork.Core.FrameData.FrameDataBase.ExecuteNonQueryUseTransaction(connectionString, sqlDic, smartDataBaseType, useTransaction);
        }

        #endregion
    }
}
