/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

namespace System.Reflection
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 方法 基础反射类
    /// </summary>
    public static class FrameMethodInfoExtends
    {
        /// <summary>
        /// 根据实体及已知的MethodInfo，传入实体及方法参数，执行对应的的方法事件
        /// </summary>
        /// <param name="methodInfo">MethodInfo</param>
        /// <param name="t">实体</param>
        /// <param name="args">方法所需实参，不可超出方法形参个数</param>
        /// <returns>返回执行结果</returns>
        public static object Invokes(this MethodInfo methodInfo, object t, params object[] args)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            List<object> methodParameters = new List<object>();
            List<object> methodParameters1 = new List<object>();
            if (parameterInfos.Length > 0)
            {
                if (parameterInfos.Length < args.Length && args != null)
                {
                    throw new Exception(SmartExtends.Resources.Exceptions.FrameCHExceptionResource.__FrameInvokeInputParametersLengthPassFormParametersLength.Formats(methodInfo.Name));
                }

                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    if (args != null && args.Length > i)
                        methodParameters.Add(Convert.ChangeType(args[i], parameterInfos[i].ParameterType));
                    else
                        methodParameters.Add(null);
                }
            }

            return methodInfo.Invoke(t, methodParameters.ToArray());
        }
    }
}
