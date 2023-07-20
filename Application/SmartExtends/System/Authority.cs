using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using SmartExtends.System.Attributes;
using SmartExtends.System.Interfaces;

namespace SmartExtends.System
{

    internal class Authority: IAuthority
    {
        static Authority()
        {

            


            ////const int level = 1000;
            ////MethodBase mb = MethodBase.GetCurrentMethod();
            ////string systemModule = Environment.NewLine;
            ////systemModule += "模块名:" + mb.Module.ToString() + Environment.NewLine;
            ////systemModule += "命名空间名:" + mb.ReflectedType.Namespace + Environment.NewLine;
            //////完全限定名，包括命名空间
            ////systemModule += "类名:" + mb.ReflectedType.FullName + Environment.NewLine;
            ////systemModule += "方法名:" + mb.Name;

            ////Console.WriteLine("LogDate: {0}{1}Level: {2}{1}systemModule: {3}{1}", DateTime.Now, Environment.NewLine, level, systemModule);
            ////Console.WriteLine();


            StackTrace ss = new StackTrace(true);
            //////index:0为本身的方法；1为调用方法；2为其上上层，依次类推
            ////mb = ss.GetFrame(1).GetMethod();

            return;
            StackFrame[] sfs = ss.GetFrames();
            Queue<Assembly> assemblyQueue = new Queue<Assembly>();
            foreach (var item in sfs)
            {
                Assembly assembly = item.GetMethod().Module.Assembly;
                if (!assemblyQueue.Contains(assembly))
                    assemblyQueue.Enqueue(assembly);
            }

            assemblyQueue.Dequeue();
            Assembly currentAssembly = assemblyQueue.Dequeue();
            if (currentAssembly == null)
                return;
           
            ////if (currentAssembly.ManifestModule.Name.ToLower() != Resources.SystemResource.ManifestModule.ToLower())
            ////    throw new Exception("Your project are not authorized!");

            if (!currentAssembly.IsDefined(typeof(AuthorityAttribute), true))
                throw new Exception("You are not authorized!");

            AuthorityAttribute aa = Attribute.GetCustomAttribute(currentAssembly, typeof(AuthorityAttribute)) as AuthorityAttribute;
            if (aa.AuthorityGuid != Resources.SystemResource.AuthorityAttributeGuid)
                throw new Exception("You are not authorized!");

            DateTime lastEnableData = DateTime.Parse(Resources.SystemResource.LastEnableDate);
            if (lastEnableData < DateTime.Now)
                throw new Exception("This resource has expired!");

            ////systemModule = Environment.NewLine;
            ////systemModule += "模块名:" + mb.Module.ToString() + Environment.NewLine;
            ////systemModule += "命名空间名:" + mb.DeclaringType.Namespace + Environment.NewLine;
            //////仅有类名
            ////systemModule += "类名:" + mb.DeclaringType.Name + Environment.NewLine;
            ////systemModule += "方法名:" + mb.Name;

            ////Console.WriteLine("LogDate: {0}{1}Level: {2}{1}systemModule: {3}{1}", DateTime.Now, Environment.NewLine, level, systemModule);
            ////Console.WriteLine();
        }

        public void InvokeVoid(Object o)
        {
            throw new NotImplementedException();
        }
    }
}
