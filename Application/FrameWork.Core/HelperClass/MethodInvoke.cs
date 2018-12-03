/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

namespace FrameWork.Core.HelperClass
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 方法的反射类
    /// </summary>
    public static class MethodInvoke
    {
        /// <summary>
        /// 根据Class的完全限定名和方法名获取MethodInfo
        /// </summary>
        /// <param name="classFullName">Class的完全限定名</param>
        /// <param name="methodName">方法 名</param>
        /// <returns>MethodInfo</returns>
        public static MethodInfo GetMethodInfoByClassNameAndMethodName(string classFullName, string methodName)
        {
            MethodInfo methodInfo = null;
            string assemblyName = classFullName.Substring(0, classFullName.LastIndexOf('.'));
            ////Assembly tarAssem = Assembly.LoadWithPartialName(assemblyName);
            Assembly tarAssem = Assembly.Load(assemblyName);
            if (tarAssem != null)
            {
                Type type = tarAssem.GetType(classFullName);
                methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            }
            else
                throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameActivatorAssembltyIsNotFind + assemblyName);
            return methodInfo;
        }

        /// <summary>
        /// 根据类型完全限定名及方法名称，方法所需参数，通过反射执行方法
        /// </summary>
        /// <param name="classFullName">类型完全限定名</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="args">方法所需参数</param>
        /// <returns>方法执行结果</returns>
        public static object InvokeMethod(string classFullName, string methodName, params object[] args)
        {
            return GetMethodInfoByClassNameAndMethodName(classFullName, methodName).Invokes(null, args);
        }

        /// <summary>
        /// 根据类型完全限定名及方法名称，方法所需参数，通过反射执行方法
        /// </summary>
        /// <param name="classFullName">类型完全限定名</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="t">已实例化的实体</param>
        /// <param name="args">方法所需参数</param>
        /// <returns>方法执行结果</returns>
        public static object InvokeMethod(string classFullName, string methodName, object t, params object[] args)
        {
            return GetMethodInfoByClassNameAndMethodName(classFullName, methodName).Invokes(t, args);
        }
    }
}
