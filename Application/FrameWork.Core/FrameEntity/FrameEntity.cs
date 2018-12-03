using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartExtends.Frame;

namespace FrameWork.Core.FrameEntity
{
    public class FrameEntity
    {
        #region 添加

        /// <summary>
        /// 提供将提供的实体添加到数据库的方法
        /// 默认使用框架的数据库连接
        /// 默认使用按字段映射
        /// </summary>            
        /// <returns>返回影响的行数</returns>
        public int Add()
        {
            return FrameEntityBLL.Add(this, FrameMappingType.Field);
        }

        /// <summary>
        /// 提供将提供的实体添加到数据库的方法
        /// 默认使用框架的数据库连接
        /// </summary>           
        /// <param name="frameMappingType">所应用的映射类型</param>
        /// <returns>返回影响的行数</returns>
        public int Add(FrameMappingType frameMappingType)
        {
            return FrameEntityBLL.Add(this, FrameWork.Core.DataCommon.DataBaseConnectionString, FrameWork.Core.DataCommon.FrameDataBaseType, frameMappingType);
        }

        /// <summary>
        /// 提供将提供的实体添加到数据库的方法，
        /// 默认使用 映射字段的方式 生成sql语句
        /// </summary>        
        /// <param name="connectionString">自定应的连接字符串</param>
        /// <param name="dataBaseType">自定应数据库类型</param>        
        /// <returns>返回影响的行数</returns>
        public int Add(string connectionString, FrameData.DataBaseType dataBaseType)
        {
            return FrameEntityBLL.Add(this, connectionString, dataBaseType, FrameMappingType.Field);
        }

        /// <summary>
        /// 提供将提供的实体添加到数据库的方法
        /// </summary>        
        /// <param name="connectionString">自定应的连接字符串</param>
        /// <param name="dataBaseType">自定应数据库类型</param>
        /// <param name="frameMappingType">所应用的映射类型</param>
        /// <returns>返回影响的行数</returns>
        public int Add(string connectionString, FrameData.DataBaseType dataBaseType, FrameMappingType frameMappingType)
        {
            return FrameEntityBLL.Add(this, connectionString, dataBaseType, frameMappingType);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 提供修改该实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// 默认使用框架的数据库连接
        /// 默认使用按字段映射
        /// </summary>              
        /// <returns>返回影响的行数</returns>
        public int Update()
        {
            return FrameEntityBLL.Update(this, FrameMappingType.Field);
        }

        /// <summary>
        /// 提供修改传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// 默认使用框架的数据库连接
        /// </summary>           
        /// <param name="frameMappingType">自定义的 映射方式</param>
        /// <returns>返回影响的 行数</returns>
        public int Update(FrameMappingType frameMappingType)
        {
            return FrameEntityBLL.Update(this, FrameWork.Core.DataCommon.DataBaseConnectionString, FrameWork.Core.DataCommon.FrameDataBaseType, frameMappingType);
        }

        /// <summary>
        /// 提供修改传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// 默认按字段映射
        /// </summary>       
        /// <param name="connectionString">自定应的连接字符串</param>
        /// <param name="dataBaseType">自定应数据库类型</param>        
        /// <returns>返回影响的行数</returns>
        public int Update(string connectionString, FrameData.DataBaseType dataBaseType)
        {
            return FrameEntityBLL.Update(this, connectionString, dataBaseType, FrameMappingType.Field);
        }

        /// <summary>
        /// 提供修改传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// </summary>        
        /// <param name="connectionString">自定应的 连接字符串</param>
        /// <param name="dataBaseType">自定应 数据库类型</param>
        /// <param name="frameMappingType">所应用的 映射类型</param>
        /// <returns>返回影响的行数</returns>
        public int Update(string connectionString, FrameData.DataBaseType dataBaseType, FrameMappingType frameMappingType)
        {
            return FrameEntityBLL.Update(this, connectionString, dataBaseType, frameMappingType);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 提供删除传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// 默认使用框架的数据库连接
        /// </summary>                     
        /// <returns>返回影响的 行数</returns>
        public int Delete()
        {
            return FrameEntityBLL.Delete(this, FrameWork.Core.DataCommon.DataBaseConnectionString, FrameWork.Core.DataCommon.FrameDataBaseType);
        }

        /// <summary>
        /// 提供删除传入实体的方法
        /// 条件为主键相同，该实体必须有且仅有一个字段被特性 [PrimaryKeyAttribute] 标识为主键
        /// </summary>        
        /// <param name="connectionString">自定义的 连接字符串</param>
        /// <param name="dataBaseType">自定义 数据库类型</param>        
        /// <returns>返回影响的 行数</returns>
        public int Delete(string connectionString, FrameData.DataBaseType dataBaseType)
        {
            return FrameEntityBLL.Delete(this, connectionString, dataBaseType);
        }

        #endregion
    }
}