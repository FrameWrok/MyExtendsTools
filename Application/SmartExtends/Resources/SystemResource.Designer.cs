﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartExtends.Resources {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SystemResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SystemResource() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SmartExtends.Resources.SystemResource", typeof(SystemResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 50577AE0-AB76-4316-8C8E-A3980AFDF41F 的本地化字符串。
        /// </summary>
        internal static string AuthorityAttributeGuid {
            get {
                return ResourceManager.GetString("AuthorityAttributeGuid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 using System;
        ///using System.Attributes;
        ///using System.Collections.Generic;
        ///using System.Linq;
        ///using System.Text;
        ///using System.Reflection;
        ///using System.Runtime;
        ///using System.Threading;
        ///using System.Diagnostics;
        ///using SmartExtends.System.Attributes;
        ///
        ///namespace SmartExtends.System
        ///{
        ///
        ///    internal class Authority
        ///    {
        ///        static Authority()
        ///        {
        ///            StackTrace ss = new StackTrace(true);           
        ///
        ///            StackFrame[] sfs = ss.GetFrames();
        ///            Queue&lt;Assembly&gt; as [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string AuthorizeAssembly {
            get {
                return ResourceManager.GetString("AuthorizeAssembly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 北大纵横 的本地化字符串。
        /// </summary>
        internal static string AuthorizedCompany {
            get {
                return ResourceManager.GetString("AuthorizedCompany", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 2015-09-01 的本地化字符串。
        /// </summary>
        internal static string LastEnableDate {
            get {
                return ResourceManager.GetString("LastEnableDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 TestSmartFrameWork.exe 的本地化字符串。
        /// </summary>
        internal static string ManifestModule {
            get {
                return ResourceManager.GetString("ManifestModule", resourceCulture);
            }
        }
    }
}