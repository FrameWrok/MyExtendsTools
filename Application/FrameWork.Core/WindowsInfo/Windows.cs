/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：
 ◆版本：1.0
**********************************************************/

using System;
using System.Collections;
using System.Security;

namespace System
{
    /// <summary>
    /// windows 系统相关信息
    /// </summary>
    public static class Window
    {
        /// <summary>
        /// Gets or sets获取或设置当前工作目录的完全限定路径。
        /// </summary>
        public static string CurrentDirectory
        {
            get
            {
                return Environment.CurrentDirectory;
            }

            set
            {
                Environment.CurrentDirectory = value;
            }
        }

        /// <summary>
        ///  Gets or sets 获取或设置进程的退出代码。
        ///  包含退出代码的 32 位有符号整数。默认值为零。
        /// </summary>
        public static int ExitCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets 获取一个值，该值指示公共语言运行时 (CLR) 是否正在关闭。
        /// 如果公共语言运行时正在关闭，则为 true；否则为 false。
        /// </summary>
        public static bool HasShutdownStarted
        {
            get
            {
                return Environment.HasShutdownStarted;
            }
        }

        /// <summary>
        /// Gets 获取此本地计算机的 NetBIOS 名称。
        /// 包含此计算机的名称的字符串。
        /// </summary>
        public static string MachineName
        {
            get
            {
                return Environment.MachineName;
            }
        }

        /// <summary>
        /// Gets 获取当前系统是否是64位系统
        /// </summary>        
        public static bool Is64BitOperatingSystem
        {
            get
            {
                ////if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").IndexOf("64") != -1)
                ////{
                ////    return true;
                ////}
                ////return false;

                return Environment.Is64BitOperatingSystem;
            }
        }

        /// <summary>
        /// Gets 获取当前系统是否是32位系统
        /// </summary>        
        public static bool Is32BitOperatingSystem
        {
            get
            {
                ////if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").IndexOf("86") != -1)
                ////{
                ////    return true;
                ////}

                ////return false;

                return !Environment.Is64BitOperatingSystem;
            }
        }

        /// <summary>
        /// Gets 返回是否是64位处理器
        /// </summary>        
        public static bool Is64BitProcess
        {
            get
            {
                return Environment.Is64BitProcess;
            }
        }

        /// <summary>
        /// Gets 返回是否是32位处理器
        /// </summary>        
        public static bool Is32BitProcess
        {
            get
            {
                return !Environment.Is64BitProcess;
            }
        }

        /// <summary>
        /// Gets 获取该进程的命令行,包含命令行参数的字符串。。
        /// </summary>
        public static string CommandLine
        {
            get
            {
                return Environment.CommandLine;
            }
        }

        /// <summary>
        /// Gets 获取为此环境定义的换行字符串。
        /// 对于非 Unix 平台为包含“\r\n”的字符串，对于 Unix 平台则为包含“\n”的字符串。
        /// </summary>
        public static string NewLine
        {
            get
            {
                return Environment.NewLine;
            }
        }

        /// <summary>
        /// Gets 获取包含当前平台标识符和版本号的 System.OperatingSystem 对象。
        /// 返回结果:
        ///     一个包含平台标识符和版本号的对象。
        ///     异常:
        ///   System.InvalidOperationException:
        ///     该属性无法获得系统版本。- 或 -获得的平台标识符不是 System.PlatformID. 的成员。
        /// </summary>
        public static OperatingSystem OSVersion
        {
            get
            {
                return Environment.OSVersion;
            }
        }

        /// <summary>
        /// Gets 获取当前计算机上的处理器数。
        /// 返回结果:
        ///     指定当前计算机上处理器个数的 32 位有符号整数。没有默认值。
        /// </summary>
        public static int ProcessorCount
        {
            get
            {
                return Environment.ProcessorCount;
            }
        }

        /// <summary>
        /// Gets 获取当前的堆栈跟踪信息。
        /// 返回结果:
        ///         包含堆栈跟踪信息的字符串。此值可为 System.String.Empty.。
        /// 异常:
        ///   System.ArgumentOutOfRangeException:
        ///     请求的堆栈跟踪信息超出范围。
        /// </summary>
        public static string StackTrace
        {
            get
            {
                return Environment.StackTrace;
            }
        }

        /// <summary>
        /// Gets 获取系统目录的完全限定路径。
        /// 返回结果:
        ///     包含目录路径的字符串。
        /// </summary>
        public static string SystemDirectory
        {
            get
            {
                return Environment.SystemDirectory;
            }
        }

        /// <summary>
        /// Gets 获取操作系统的页面文件的内存量。
        /// 返回结果:
        ///     系统页面文件中的字节数。
        /// </summary>
        public static int SystemPageSize
        {
            get
            {
                return Environment.SystemPageSize;
            }
        }

        /// <summary>
        /// Gets 获取系统启动后经过的毫秒数。
        /// 返回结果:
        ///     一个 32 位带符号整数，它包含自上次启动计算机以来所经过的时间（以毫秒为单位）。
        /// </summary>
        public static int TickCount
        {
            get
            {
                return Environment.TickCount;
            }
        }

        /// <summary>
        /// Gets  获取与当前用户关联的网络域名。
        /// 返回结果:
        ///     与当前用户关联的网络域名。
        ///      异常:
        ///   System.PlatformNotSupportedException:
        ///     该操作系统不支持检索网络域名。        
        ///   System.InvalidOperationException:
        ///     无法检索此网络域名。
        /// </summary>
        public static string UserDomainName
        {
            get
            {
                return Environment.UserDomainName;
            }
        }

        /// <summary>
        /// Gets 获取一个值，用以指示当前进程是否在用户交互模式中运行。
        /// 返回结果:
        ///     如果当前进程在用户交互模式中运行，则为 true；否则为 false。
        /// </summary>
        public static bool UserInteractive
        {
            get
            {
                return Environment.UserInteractive;
            }
        }

        /// <summary>
        /// Gets 获取当前已登录到 Windows 操作系统的人员的用户名。
        /// 返回结果:
        ///     已登录到 Windows 的人员的用户名。
        /// </summary>
        public static string UserName
        {
            get
            {
                return Environment.UserName;
            }
        }

        /// <summary>
        /// Gets 获取一个 System.Version 对象，该对象描述公共语言运行时的主版本、次版本、内部版本和修订号。
        /// 返回结果:
        ///     用于显示公共语言运行时版本的对象。
        /// </summary>
        public static Version Version
        {
            get
            {
                return Environment.Version;
            }
        }

        /// <summary>
        /// Gets 获取映射到进程上下文的物理内存量。
        /// 返回结果:
        ///     一个 64 位有符号整数，包含映射到进程上下文的物理内存字节的数目。
        /// </summary>
        public static long WorkingSet
        {
            get
            {
                return Environment.WorkingSet;
            }
        }

        /// <summary>
        /// 终止此进程并为基础操作系统提供指定的退出代码。
        /// 异常:
        ///   System.Security.SecurityException:
        ///     调用方没有足够的安全权限来执行此函数。
        /// </summary>
        /// <param name="exitCode">提供给操作系统的退出代码。</param>
        [SecuritySafeCritical]
        public static void Exit(int exitCode)
        {
            Environment.Exit(exitCode);
        }

        /// <summary>
        /// 将嵌入到指定字符串中的每个环境变量的名称替换为该变量的值的等效字符串，然后返回结果字符串。
        /// 参数:
        ///   name:
        ///     包含零个或多个环境变量名的字符串。每个环境变量都用百分号 (%) 引起来。
        /// 返回结果:
        ///     一个字符串，其中的每个环境变量均被替换为该变量的值。        
        /// 异常:
        ///   System.ArgumentNullException:
        ///     name 为 null。
        /// </summary>
        /// <param name="name">包含零个或多个环境变量名的字符串。每个环境变量都用百分号 (%) 引起来。</param>
        /// <returns>一个字符串，其中的每个环境变量均被替换为该变量的值。</returns>
        [SecuritySafeCritical]
        public static string ExpandEnvironmentVariables(string name)
        {
            return Environment.ExpandEnvironmentVariables(name);
        }

        /// <summary>
        /// 向 Windows 的应用程序事件日志写入消息后立即终止进程，然后在发往 Microsoft 的错误报告中加入该消息。
        /// 参数:
        ///   message:
        ///     解释进程终止原因的消息，或者如果未提供解释则返回 null。
        /// </summary>
        /// <param name="message">解释进程终止原因的消息，或者如果未提供解释则返回 null。</param>
        [SecurityCritical]
        public static void FailFast(string message)
        {
            Environment.FailFast(message);
        }

        /// <summary>
        /// 向 Windows 的应用程序事件日志写入消息后立即终止进程，然后在发往 Microsoft 的错误报告中加入该消息和异常信息。
        /// 参数:
        ///   message:
        ///     解释进程终止原因的消息，或者如果未提供解释则返回 null。        
        ///   exception:
        ///     一个异常，表示导致终止的错误。通常这是 catch 块中的异常。
        /// </summary>
        /// <param name="message">解释进程终止原因的消息，或者如果未提供解释则返回 null。</param>
        /// <param name="exception">一个异常，表示导致终止的错误。通常这是 catch 块中的异常。</param>
        [SecurityCritical]
        public static void FailFast(string message, Exception exception)
        {
            Environment.FailFast(message, exception);
        }

        /// <summary>
        /// 返回包含当前进程的命令行参数的字符串数组。
        /// 返回结果:
        ///     字符串数组，其中的每个元素都包含一个命令行参数。第一个元素是可执行文件名，后面的零个或多个元素包含其余的命令行参数。
        /// 异常:
        ///   System.NotSupportedException:
        ///     系统不支持命令行参数。
        /// </summary>
        /// <returns>字符串数组，其中的每个元素都包含一个命令行参数。第一个元素是可执行文件名，后面的零个或多个元素包含其余的命令行参数。</returns>
        [SecuritySafeCritical]
        public static string[] GetCommandLineArgs()
        {
            return Environment.GetCommandLineArgs();
        }

        /// <summary>
        /// 从当前进程检索环境变量的值。
        /// 参数:
        ///   variable:
        ///     环境变量名。
        /// 返回结果:
        ///     variable 指定的环境变量的值；或者如果找不到环境变量，则返回 null。
        /// 异常:
        ///   System.ArgumentNullException:
        ///     variable 为 null。
        ///   System.Security.SecurityException:
        ///     调用方不具有执行此操作所需的权限。
        /// </summary>
        /// <param name="variable">环境变量名。</param>
        /// <returns>variable 指定的环境变量的值；或者如果找不到环境变量，则返回 null。</returns>
        [SecuritySafeCritical]
        public static string GetEnvironmentVariable(string variable)
        {
            return Environment.GetEnvironmentVariable(variable);
        }

        /// <summary>
        /// 从当前进程或者从当前用户或本地计算机的 Windows 操作系统注册表项检索环境变量的值。
        /// 参数:
        ///   variable:
        ///     环境变量名。
        ///   target:
        ///     System.EnvironmentVariableTarget 值之一。
        /// 返回结果:
        ///     variable 和 target 参数指定的环境变量的值；或者如果找不到环境变量，则返回 null。
        /// 异常:
        ///   System.ArgumentNullException:
        ///     variable 为 null。
        ///   System.NotSupportedException:
        ///     target 是 System.EnvironmentVariableTarget.User 或 System.EnvironmentVariableTarget.Machine，当前操作系统为
        ///     Windows 95、Windows 98 或 Windows Me。
        ///   System.ArgumentException:
        ///     target 不是有效的 System.EnvironmentVariableTarget 值。
        ///   System.Security.SecurityException:
        ///     调用方不具有执行此操作所需的权限。
        /// </summary>
        /// <param name="variable">环境变量名。</param>
        /// <param name="target">System.EnvironmentVariableTarget 值之一。</param>
        /// <returns>variable 和 target 参数指定的环境变量的值；或者如果找不到环境变量，则返回 null。</returns>
        [SecuritySafeCritical]
        public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
        {
            return Environment.GetEnvironmentVariable(variable, target);
        }

        /// <summary>
        /// 从当前进程检索所有环境变量名及其值。
        /// 返回结果:
        ///     包含所有环境变量名及其值的字典；如果找不到任何环境变量，则返回空字典。        
        /// 异常:
        ///   System.Security.SecurityException:
        ///     调用方不具有执行此操作所需的权限。
        ///   System.OutOfMemoryException:
        ///     缓冲区内存不足。
        /// </summary>
        /// <returns>包含所有环境变量名及其值的字典；如果找不到任何环境变量，则返回空字典。</returns>
        [SecuritySafeCritical]
        public static IDictionary GetEnvironmentVariables()
        {
            return Environment.GetEnvironmentVariables();
        }

        /// <summary>
        /// 从当前进程或者从当前用户或本地计算机的 Windows 操作系统注册表项检索所有环境变量名及其值。
        /// 参数:
        ///   target:
        ///     System.EnvironmentVariableTarget 值之一。
        /// 返回结果:
        ///     包含 target 参数所指定的源中所有环境变量名及其值的字典；否则，如果找不到任何环境变量，则返回空字典。
        /// 异常:
        ///   System.Security.SecurityException:
        ///     调用方没有为 target 的指定值执行此操作所需具备的权限。
        ///   System.NotSupportedException:
        ///     此方法不能用在 Windows 95 或 Windows 98 平台上。
        ///   System.ArgumentException:
        ///     target 包含非法值。
        /// </summary>
        /// <param name="target">System.EnvironmentVariableTarget 值之一。</param>
        /// <returns>包含 target 参数所指定的源中所有环境变量名及其值的字典；否则，如果找不到任何环境变量，则返回空字典。</returns>
        [SecuritySafeCritical]
        public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
        {
            return Environment.GetEnvironmentVariables(target);
        }

        /// <summary>
        /// 获取由指定枚举标识的系统特殊文件夹的路径。
        /// 参数:
        ///   folder:
        ///     标识系统特殊文件夹的枚举常数。
        /// 返回结果:
        ///     如果指定的系统特殊文件夹实际存在于您的计算机上，则为到该文件夹的路径；否则为空字符串 ("")。如果操作系统未创建文件夹、已删除现有文件夹，或者文件夹是不对应物理路径的虚拟目录（例如“我的电脑”），则该文件夹不会实际存在。
        /// 异常:
        ///   System.ArgumentException:
        ///     folder 不是 System.Environment.SpecialFolder 的成员。
        ///   System.PlatformNotSupportedException:
        ///     当前平台不受支持。
        /// </summary>
        /// <param name="folder">标识系统特殊文件夹的枚举常数。</param>
        /// <returns>如果指定的系统特殊文件夹实际存在于您的计算机上，则为到该文件夹的路径；否则为空字符串 ("")。如果操作系统未创建文件夹、已删除现有文件夹，或者文件夹是不对应物理路径的虚拟目录（例如“我的电脑”），则该文件夹不会实际存在。</returns>
        [SecuritySafeCritical]
        public static string GetFolderPath(Environment.SpecialFolder folder)
        {
            return Environment.GetFolderPath(folder);
        }

        /// <summary>
        /// 获取由指定枚举标识的系统特殊文件夹的路径，并使用用于访问特殊文件夹的指定选项。
        /// 参数:
        ///   folder:
        ///     标识系统特殊文件夹的枚举常数。
        ///   option:
        ///     指定用于访问特殊文件夹的选项。
        /// 返回结果:
        ///     如果指定的系统特殊文件夹实际存在于您的计算机上，则为到该文件夹的路径；否则为空字符串 ("")。如果操作系统未创建文件夹、已删除现有文件夹，或者文件夹是不对应物理路径的虚拟目录（例如“我的电脑”），则该文件夹不会实际存在。
        /// 异常:
        ///   System.ArgumentException:
        ///     folder 不是 System.Environment.SpecialFolder. 的成员。
        ///   System.PlatformNotSupportedException:
        ///     System.PlatformNotSupportedException
        /// </summary>
        /// <param name="folder">标识系统特殊文件夹的枚举常数。</param>
        /// <param name="option">指定用于访问特殊文件夹的选项。</param>
        /// <returns>如果指定的系统特殊文件夹实际存在于您的计算机上，则为到该文件夹的路径；否则为空字符串 ("")。如果操作系统未创建文件夹、已删除现有文件夹，或者文件夹是不对应物理路径的虚拟目录（例如“我的电脑”），则该文件夹不会实际存在。</returns>
        [SecuritySafeCritical]
        public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            return Environment.GetFolderPath(folder, option);
        }

        /// <summary>
        /// 返回包含当前计算机中的逻辑驱动器名称的字符串数组。
        /// 返回结果:
        ///     字符串数组，其中的每个元素都包含逻辑驱动器名称。例如，如果计算机的硬盘是第一个逻辑驱动器，则返回的第一个元素是“C:\”。
        /// 异常:
        ///   System.IO.IOException:
        ///     发生 I/O 错误。
        ///   System.Security.SecurityException:
        ///     调用方没有所需的权限。
        /// </summary>
        /// <returns>字符串数组，其中的每个元素都包含逻辑驱动器名称。例如，如果计算机的硬盘是第一个逻辑驱动器，则返回的第一个元素是“C:\”。</returns>
        [SecuritySafeCritical]
        public static string[] GetLogicalDrives()
        {
            return Environment.GetLogicalDrives();
        }

        /// <summary>
        /// 创建、修改或删除当前进程中存储的环境变量。
        /// 参数:
        ///   variable:
        ///     环境变量名。
        ///   value:
        ///     要分配给 variable 的值。
        /// 异常:
        ///   System.ArgumentNullException:
        ///     variable 为 null。
        ///   System.ArgumentException:
        ///     variable 包含零长度字符串、起始十六进制零字符 (0x00) 或等号（“=”）。- 或 -variable 或 value 的长度大于等于
        ///     32,767 个字符。- 或 -在执行此操作的过程中发生错误。
        ///   System.Security.SecurityException:
        ///     调用方不具有执行此操作所需的权限。
        /// </summary>
        /// <param name="variable">环境变量名。</param>
        /// <param name="value">要分配给 variable 的值。</param>
        [SecuritySafeCritical]
        public static void SetEnvironmentVariable(string variable, string value)
        {
            Environment.SetEnvironmentVariable(variable, value);
        }

        /// <summary>
        /// 创建、修改或删除当前进程中或者为当前用户或本地计算机保留的 Windows 操作系统注册表项中存储的环境变量。
        /// 参数:
        ///   variable:
        ///     环境变量名。
        ///   value:
        ///     要分配给 variable 的值。
        ///   target:
        ///     System.EnvironmentVariableTarget 值之一。
        /// 异常:
        ///   System.ArgumentNullException:
        ///     variable 为 null。
        ///   System.ArgumentException:
        ///     variable 包含零长度字符串、起始十六进制零字符 (0x00) 或等号（“=”）。- 或 -variable 的长度大于等于 32,767
        ///     个字符。- 或 -target 不是 System.EnvironmentVariableTarget 枚举的成员。- 或 -target 为 System.EnvironmentVariableTarget.Machine
        ///     或 System.EnvironmentVariableTarget.User，并且 variable 的长度大于等于 255。- 或 -target
        ///     为 System.EnvironmentVariableTarget.Process，并且 value 的长度大于等于 32,767 个字符。-
        ///     或 -在执行此操作的过程中发生错误。
        ///   System.NotSupportedException:
        ///     target 是 System.EnvironmentVariableTarget.User 或 System.EnvironmentVariableTarget.Machine，当前操作系统为
        ///     Windows 95、Windows 98 或 Windows Me。
        ///   System.Security.SecurityException:
        ///     调用方不具有执行此操作所需的权限。
        /// </summary>
        /// <param name="variable">环境变量名。</param>
        /// <param name="value">要分配给 variable 的值。</param>
        /// <param name="target">System.EnvironmentVariableTarget 值之一。</param>
        [SecuritySafeCritical]
        public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
        {
            Environment.SetEnvironmentVariable(variable, value, target);
        }
    }
}
