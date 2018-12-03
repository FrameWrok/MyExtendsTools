/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Reflection;

namespace FrameWork.Core.HelperClass
{
    /// <summary>
    /// 基础 反射类
    /// </summary>
    public static class FrameActivator
    {
        /// <summary>
        /// 根据类型完全限定名利用反射创建实体
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="classFullName">类型完全限定名</param>
        /// <param name="args">构造函数的参数</param>
        /// <returns>实例化的实体</returns>
        public static object CreateEntityByClassFullNameAndParameters(string assemblyName, string classFullName, params object[] args)
        {
            object t = null;
            Assembly tarAssem = Assembly.Load(assemblyName);
            if (tarAssem != null)
            {
                Type type = tarAssem.GetType(classFullName);
                if (type == null)
                    throw new Exception(string.Format(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameReflectionAssembltyIsNotFindClass, assemblyName, classFullName));
                if (type.IsSealed)
                    throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameActivatorCreateInstanceSealedError + classFullName);
                if (type.IsEnum)
                    throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameActivatorCreateInstanceIsEnumError + classFullName);
                if (type.IsAbstract)
                    throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameActivatorCreateInstanceIsAbstractError + classFullName);
                if (type.IsInterface)
                    throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameActivatorCreateInstanceIsInterfaceError + classFullName);
                if (type.IsClass)
                    t = Activator.CreateInstance(type, args);
            }
            else
                throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameActivatorAssembltyIsNotFind + assemblyName);
            return t;
        }
    }
}
