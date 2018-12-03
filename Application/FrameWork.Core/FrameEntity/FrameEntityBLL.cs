using System;
using System.Collections.Generic;
using System.Linq;
using FrameWork.Core.FrameData;
using SmartExtends.Frame;

namespace FrameWork.Core.FrameEntity
{
    internal static class FrameEntityBLL
    {
        #region 添加

        /// <summary>
        /// 提供将提供的实体添加到数据库的方法
        /// 默认使用框架的数据库连接
        /// 默认使用按字段映射
        /// </summary>
        /// <param name="item">增加的实体</param>       
        /// <returns>返回影响的行数</returns>
        internal static int Add(object item)
        {
            return Add(item, FrameMappingType.Field);
        }

        /// <summary>
        /// 提供将提供的实体添加到数据库的方法
        /// 默认使用框架的数据库连接
        /// </summary>
        /// <param name="item">增加的实体</param>        
        /// <param name="frameMappingType">所应用的映射类型</param>
        /// <returns>返回影响的行数</returns>
        internal static int Add(object item, FrameMappingType frameMappingType)
        {
            return Add(item, FrameWork.Core.DataCommon.DataBaseConnectionString, FrameWork.Core.DataCommon.FrameDataBaseType, frameMappingType);
        }

        /// <summary>
        /// 提供将提供的实体添加到数据库的方法，
        /// 默认使用 映射字段的方式 生成sql语句
        /// </summary>
        /// <param name="item">增加的实体</param>
        /// <param name="connectionString">自定应的连接字符串</param>
        /// <param name="dataBaseType">自定应数据库类型</param>        
        /// <returns>返回影响的行数</returns>
        internal static int Add(object item, string connectionString, FrameData.DataBaseType dataBaseType)
        {
            return Add(item, connectionString, dataBaseType, FrameMappingType.Field);
        }

        /// <summary>
        /// 提供将提供的实体添加到数据库的方法
        /// </summary>
        /// <param name="item">增加的实体</param>
        /// <param name="connectionString">自定应的连接字符串</param>
        /// <param name="dataBaseType">自定应数据库类型</param>
        /// <param name="frameMappingType">所应用的映射类型</param>
        /// <returns>返回影响的行数</returns>
        internal static int Add(object item, string connectionString, FrameData.DataBaseType dataBaseType, FrameMappingType frameMappingType)
        {
            Dictionary<string, object> columnDic = FrameData.FrameDataBase.GetColumnDic(item, frameMappingType);
            string insertSql = FrameGenerateSql.GenerateInsertSql(item.GetType().ToString(), columnDic, dataBaseType);
            return FrameData.FrameDataBase.ExecuteNonQuery(connectionString, insertSql, System.Data.CommandType.Text, FrameDataBase.GetParameters(columnDic, dataBaseType), dataBaseType);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 提供修改该实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// 默认使用框架的数据库连接
        /// 默认使用按字段映射
        /// </summary>
        /// <param name="item">增加的实体</param>       
        /// <returns>返回影响的行数</returns>
        internal static int Update(object item)
        {
            return Update(item, FrameMappingType.Field);
        }

        /// <summary>
        /// 提供修改传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// 默认使用框架的数据库连接
        /// </summary>
        /// <param name="item">要修改的 实体</param>      
        /// <param name="frameMappingType">自定义的 映射方式</param>
        /// <returns>返回影响的 行数</returns>
        internal static int Update(object item, FrameMappingType frameMappingType)
        {
            return Update(item, FrameWork.Core.DataCommon.DataBaseConnectionString, FrameWork.Core.DataCommon.FrameDataBaseType, frameMappingType);
        }

        /// <summary>
        /// 提供修改传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// 默认按字段映射
        /// </summary>
        /// <param name="item">要修改的实体</param>
        /// <param name="connectionString">自定应的连接字符串</param>
        /// <param name="dataBaseType">自定应数据库类型</param>        
        /// <returns>返回影响的行数</returns>
        internal static int Update(object item, string connectionString, FrameData.DataBaseType dataBaseType)
        {
            return Update(item, connectionString, dataBaseType, FrameMappingType.Field);
        }

        /// <summary>
        /// 提供修改传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// </summary>
        /// <param name="item">要修改的 实体</param>
        /// <param name="connectionString">自定应的 连接字符串</param>
        /// <param name="dataBaseType">自定应 数据库类型</param>
        /// <param name="frameMappingType">所应用的 映射类型</param>
        /// <returns>返回影响的行数</returns>
        internal static int Update(object item, string connectionString, FrameData.DataBaseType dataBaseType, FrameMappingType frameMappingType)
        {
            Dictionary<string, object> columnDic = FrameData.FrameDataBase.GetColumnDic(item, frameMappingType);
            Dictionary<string, object> primaryKeyDic = FrameData.FrameDataBase.GetColumnDicByUserAttribute<CoreAttribute.PrimaryKeyAttribute>(item, "PrimaryKeyName");
            if (primaryKeyDic.Count > 1)
                throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameEntityPrimaryKeyToMary);
            if (primaryKeyDic.Count < 1)
                throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameEntityPrimaryKeyNull);
            string updateSql = FrameGenerateSql.GenerateUpdateSql(item.GetType().ToString(), primaryKeyDic.First().Key, columnDic, dataBaseType);
            return FrameData.FrameDataBase.ExecuteNonQuery(connectionString, updateSql, System.Data.CommandType.Text, FrameDataBase.GetParameters(columnDic, dataBaseType), dataBaseType);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 提供删除传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// 默认使用框架的数据库连接
        /// </summary>
        /// <param name="item">要删除的 实体</param>                
        /// <returns>返回影响的 行数</returns>
        internal static int Delete(object item)
        {
            return Delete(item, FrameWork.Core.DataCommon.DataBaseConnectionString, FrameWork.Core.DataCommon.FrameDataBaseType);
        }

        /// <summary>
        /// 提供删除传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// </summary>
        /// <param name="item">要删除的 实体</param>
        /// <param name="connectionString">自定义的 连接字符串</param>
        /// <param name="dataBaseType">自定义 数据库类型</param>        
        /// <returns>返回影响的 行数</returns>
        internal static int Delete(object item, string connectionString, FrameData.DataBaseType dataBaseType)
        {
            Dictionary<string, object> primaryKeyDic = FrameData.FrameDataBase.GetColumnDicByUserAttribute<CoreAttribute.PrimaryKeyAttribute>(item, null);
            if (primaryKeyDic.Count > 1)
                throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameEntityPrimaryKeyToMary);
            if (primaryKeyDic.Count < 1)
                throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameEntityPrimaryKeyNull);
            string deleteSql = FrameGenerateSql.GenerateDeleteSql(item.GetType().ToString(), primaryKeyDic.First().Key, dataBaseType);
            return FrameData.FrameDataBase.ExecuteNonQuery(connectionString, deleteSql, System.Data.CommandType.Text, FrameDataBase.GetParameters(primaryKeyDic, dataBaseType), dataBaseType);
        }

        #endregion
    }
}