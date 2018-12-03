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
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FrameWork.Core.HelperClass
{
    /// <summary>
    /// 方法的反射类
    /// </summary>
    public static class FrameMethodInvoke
    {
        /// <summary>
        /// 根据Class的完全限定名和方法名获取MethodInfo
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="classFullName">Class的完全限定名</param>
        /// <param name="t">已实例化的实体, 如是静态类，可传入 NULL </param>
        /// <param name="methodName">方法名</param>
        /// <returns>MethodInfo</returns>
        public static MethodInfo GetMethodInfoByClassNameAndMethodName(string assemblyName, string classFullName, object t, string methodName)
        {
            MethodInfo methodInfo = null;
            Assembly tarAssem = Assembly.Load(assemblyName);
            if (tarAssem != null)
            {
                Type type = tarAssem.GetType(classFullName);
                if (type.IsAbstract)
                    methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                else
                {
                    if (t.GetType().IsClass)
                    {
                        if (t != null)
                            methodInfo = t.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                        else
                        {
                            t = FrameActivator.CreateEntityByClassFullNameAndParameters(assemblyName, classFullName);
                            methodInfo = t.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                        }
                    }
                    else
                        throw new ArgumentException("error: object t is not a Class");
                }
            }
            else
                throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameActivatorAssembltyIsNotFind + assemblyName);
            return methodInfo;
        }

        /// <summary>
        /// 根据类型完全限定名及方法名称，方法所需参数，通过反射执行方法
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="classFullName">类型完全限定名</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="t">已实例化的实体, 如是静态类，可传入 NULL </param>
        /// <param name="args">方法所需参数</param>
        /// <returns>方法执行结果</returns>
        public static object InvokeMethod(string assemblyName, string classFullName, string methodName, object t, params object[] args)
        {
            MethodInfo methodInfo = null;
            Assembly tarAssem = Assembly.Load(assemblyName);
            if (tarAssem != null)
            {
                Type type = tarAssem.GetType(classFullName);
                if (type.IsAbstract)
                    methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                else
                {
                    if (t != null && t.GetType().IsClass)
                        methodInfo = t.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                    else
                    {
                        t = FrameActivator.CreateEntityByClassFullNameAndParameters(assemblyName, classFullName);
                        methodInfo = t.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                    }
                }
            }
            else
                throw new Exception(FrameWork.Core.Resources.Exceptions.FrameCHExceptionResource.__FrameActivatorAssembltyIsNotFind + assemblyName);
            return methodInfo.Invokes(t, args);
        }
    }
}
