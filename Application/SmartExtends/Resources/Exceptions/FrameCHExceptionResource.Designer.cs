﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartExtends.Resources.Exceptions {
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
    internal class FrameCHExceptionResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FrameCHExceptionResource() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SmartExtends.Resources.Exceptions.FrameCHExceptionResource", typeof(FrameCHExceptionResource).Assembly);
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
        ///   查找类似 未能找到该类的程序集 的本地化字符串。
        /// </summary>
        internal static string @__FrameActivatorAssembltyIsNotFind {
            get {
                return ResourceManager.GetString("__FrameActivatorAssembltyIsNotFind", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该类型为抽象类型，不可实例化 的本地化字符串。
        /// </summary>
        internal static string @__FrameActivatorCreateInstanceIsAbstractError {
            get {
                return ResourceManager.GetString("__FrameActivatorCreateInstanceIsAbstractError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该类型为枚举类型，不可实例化 的本地化字符串。
        /// </summary>
        internal static string @__FrameActivatorCreateInstanceIsEnumError {
            get {
                return ResourceManager.GetString("__FrameActivatorCreateInstanceIsEnumError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该类型为接口类型，不可实例化 的本地化字符串。
        /// </summary>
        internal static string @__FrameActivatorCreateInstanceIsInterfaceError {
            get {
                return ResourceManager.GetString("__FrameActivatorCreateInstanceIsInterfaceError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该类型为密封类型，不可实例化 的本地化字符串。
        /// </summary>
        internal static string @__FrameActivatorCreateInstanceSealedError {
            get {
                return ResourceManager.GetString("__FrameActivatorCreateInstanceSealedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 映射的数据库列名不能为空、Null或空格 的本地化字符串。
        /// </summary>
        internal static string @__FrameColumnMappingAttributeColumnName_IsNullOrEmptyOrBlank {
            get {
                return ResourceManager.GetString("__FrameColumnMappingAttributeColumnName_IsNullOrEmptyOrBlank", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 要获取xml的DataSet不允许为空 的本地化字符串。
        /// </summary>
        internal static string @__FrameDataSetGetXmlDataSourceDontNull {
            get {
                return ResourceManager.GetString("__FrameDataSetGetXmlDataSourceDontNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该实体中没有字段或属性被标识主键特性，无法利用框架特有方法进行修改和删除操作 的本地化字符串。
        /// </summary>
        internal static string @__FrameEntityPrimaryKeyNull {
            get {
                return ResourceManager.GetString("__FrameEntityPrimaryKeyNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该实体中被标为主键的字段或属性多余一个,一个实体中被标为主键特性的字段或属性只可唯一 的本地化字符串。
        /// </summary>
        internal static string @__FrameEntityPrimaryKeyToMary {
            get {
                return ResourceManager.GetString("__FrameEntityPrimaryKeyToMary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型中的方法{0}的输入参数个数不能超出方法形参个数 的本地化字符串。
        /// </summary>
        internal static string @__FrameInvokeInputParametersLengthPassFormParametersLength {
            get {
                return ResourceManager.GetString("__FrameInvokeInputParametersLengthPassFormParametersLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 程序集{0}中未找到类型{1} 的本地化字符串。
        /// </summary>
        internal static string @__FrameReflectionAssembltyIsNotFindClass {
            get {
                return ResourceManager.GetString("__FrameReflectionAssembltyIsNotFindClass", resourceCulture);
            }
        }
    }
}
