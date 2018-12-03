/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.Threading;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;

namespace System.IO
{
    /// <summary>
    /// 文件压缩类
    /// </summary>
    public partial class FileCompress
    {
        #region 此部分为实例构造函数以及压缩相关的实例方法

        /// <summary>
        /// Initializes a new instance of the FileCompress class
        /// 压缩文件类的构造函数
        /// </summary>
        /// <param name="sourcePath">来源文件的路径(文件夹或文件)，如是解压缩操作则必须是文件的路径</param>
        /// <param name="outPutPath">压缩或解压缩后文件的输出路径(压缩时可输入文件名，默认为压缩的文件或文件夹的名称)</param>
        /// <param name="passWord">压缩文件的密码(不需要密码可传入NULL或tring.Empty)</param>
        /// <param name="level">压缩等级，解压缩可不传入(等级大小从0-9,0为不压缩，只打包;9为最大限度压缩;默认压缩等级为7)</param>
        /// <param name="bufferSize">压缩时压缩类所用的缓冲区大小</param>
        /// <param name="async">是否执行异步压缩解压缩</param>
        public FileCompress(string sourcePath, string outPutPath, string passWord, int level, int bufferSize, bool async = false)
        {
            this.SourcePath = sourcePath;
            this.OutPutPath = outPutPath;
            this.PassWord = passWord;
            this.Level = level;
            this.BufferSize = bufferSize;
            this.Async = async;
        }

        /// <summary>
        /// Initializes a new instance of the FileCompress class
        /// 压缩文件类的构造函数
        /// </summary>
        /// <param name="sourcePath">来源文件的路径(文件夹或文件)，如是解压缩操作则必须是文件的路径</param>
        /// <param name="outPutPath">压缩或解压缩后文件的输出路径(压缩时可输入文件名，默认为压缩的文件或文件夹的名称)</param>
        /// <param name="passWord">压缩文件的密码(不需要密码可传入NULL或tring.Empty)</param>
        /// <param name="level">压缩等级，解压缩可不传入(等级大小从0-9,0为不压缩，只打包;9为最大限度压缩;默认压缩等级为7)</param>        
        /// <param name="async">是否执行异步压缩解压缩</param>
        public FileCompress(string sourcePath, string outPutPath, string passWord, int level, bool async = false)
            : this(sourcePath, outPutPath, passWord, level, DefaultBufferSize, async)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FileCompress class
        /// 压缩文件类的构造函数
        /// </summary>
        /// <param name="sourcePath">来源文件的路径(文件夹或文件)，如是解压缩操作则必须是文件的路径</param>
        /// <param name="outPutPath">压缩或解压缩后文件的输出路径(压缩时可输入文件名，默认为压缩的文件或文件夹的名称)</param>
        /// <param name="passWord">压缩文件的密码(不需要密码可传入NULL或tring.Empty)</param>      
        /// <param name="async">是否执行异步压缩解压缩</param>
        public FileCompress(string sourcePath, string outPutPath, string passWord, bool async)
            : this(sourcePath, outPutPath, passWord, FileCompress.StaticLevel, DefaultBufferSize, async = false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FileCompress class
        /// 压缩文件类的构造函数
        /// </summary>
        /// <param name="sourcePath">来源文件的路径(文件夹或文件)，如是解压缩操作则必须是文件的路径</param>
        /// <param name="outPutPath">压缩或解压缩后文件的输出路径(压缩时可输入文件名，默认为压缩的文件或文件夹的名称)</param>    
        /// <param name="async">是否执行异步压缩解压缩</param>
        public FileCompress(string sourcePath, string outPutPath, bool async)
            : this(sourcePath, outPutPath, string.Empty, FileCompress.StaticLevel, DefaultBufferSize, async = false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FileCompress class
        /// 压缩文件类的构造函数
        /// </summary>
        /// <param name="sourcePath">来源文件的路径(文件夹或文件)，如是解压缩操作则必须是文件的路径</param>
        /// <param name="outPutPath">压缩或解压缩后文件的输出路径(压缩时可输入文件名，默认为压缩的文件或文件夹的名称)</param>        
        /// <param name="level">压缩等级，解压缩可不传入(等级大小从0-9,0为不压缩，只打包;9为最大限度压缩;默认压缩等级为7)</param>        
        /// <param name="async">是否执行异步压缩解压缩</param>
        public FileCompress(string sourcePath, string outPutPath, int level, bool async = false)
            : this(sourcePath, outPutPath, string.Empty, level, DefaultBufferSize, async)
        {
        }

        /// <summary>
        /// 开始执行压缩
        /// </summary>
        public void RunCompress()
        {
            Compress(this.SourcePath, this.OutPutPath, this.PassWord, this.Level, this.bufferSize, this.Async);
        }

        /// <summary>
        /// 开始执行解压缩
        /// </summary>
        public void RunDeCompress()
        {
            DeCompress(this.SourcePath, this.OutPutPath, this.BufferSize);
        }

        #endregion
    }

    /// <summary>
    /// 文件压缩类
    /// </summary>
    public partial class FileCompress
    {
        #region 此部分为实例压缩类对象的属性及字段

        /// <summary>
        /// 设置或获取压缩文件的密码
        /// </summary>
        public string PassWord
        {
            get
            {
                return this.passWord;
            }

            set
            {
                this.passWord = value;
            }
        }

        /// <summary>
        /// 压缩文件或文件夹的源路径
        /// </summary>
        public string SourcePath
        {
            get
            {
                return this.sourcePath;
            }

            set
            {
                if (value.IsNullOrEmptyOrBlank())
                    throw new ArgumentNullException("SourcePath");

                this.sourcePath = value;
            }
        }

        /// <summary>
        /// 获取或设置文件的输出路径
        /// </summary>
        public string OutPutPath
        {
            get
            {
                return this.outputPath;
            }

            set
            {
                if (value.IsNullOrEmptyOrBlank())
                    throw new ArgumentNullException("OutPutPath");

                this.outputPath = value;
            }
        }

        /// <summary>
        /// 获取或设置压缩等级
        /// </summary>
        public int Level
        {
            get
            {
                return this.level;
            }

            set
            {
                if (value < 0 || value > 9)
                    throw new ArgumentException("Level的值应介于 (0-9)");

                this.level = value;
            }
        }

        /// <summary>
        /// 获取或设置压缩类压缩或解压缩时的缓存大小
        /// </summary>
        public int BufferSize
        {
            get
            {
                return this.bufferSize;
            }

            set
            {
                if (value < 1)
                    throw new ArgumentException("BufferSize的值应大于0");

                this.bufferSize = value;
            }
        }

        /// <summary>
        /// 是否执行异步操作
        /// </summary>
        public bool Async
        {
            get
            {
                return this.async;
            }

            set
            {
                this.async = value;
            }
        }

        private int bufferSize;
        private int level;
        private string outputPath;
        private string sourcePath;
        private string passWord;
        private bool async = false;

        #endregion
    }

    /// <summary>
    /// 文件压缩类
    /// </summary>
    public partial class FileCompress
    {
        #region 此部分为负责压缩静态方法，以及压缩时所需要的默认值

        /// <summary>
        /// 压缩文件类的构造函数
        /// </summary>
        /// <param name="sourcePath">要压缩的文件的路径(文件夹或文件)</param>
        /// <param name="outPutPath">压缩后文件的输出路径(可输入文件名，默认为压缩的文件或文件夹的名称)</param>
        public static void Compress(string sourcePath, string outPutPath)
        {
            Compress(sourcePath, outPutPath, string.Empty);
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourcePath">要压缩的文件的路径(文件夹或文件)</param>
        /// <param name="outPutPath">压缩后文件的输出路径(可输入文件名，默认为压缩的文件或文件夹的名称)</param>
        /// <param name="password">压缩文件的密码(不需要密码可传入NULL或tring.Empty)</param>
        public static void Compress(string sourcePath, string outPutPath, string password)
        {
            FileCompress fileCompress = new FileCompress(sourcePath, outPutPath, password, false);
            fileCompress.RunCompress();
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourcePath">要压缩的文件的路径(文件夹或文件)</param>
        /// <param name="outPutPath">压缩后文件的输出路径(可输入文件名，默认为压缩的文件或文件夹的名称)</param>
        /// <param name="password">压缩文件的密码(不需要密码可传入NULL或tring.Empty)</param>
        /// <param name="level">压缩等级(等级大小从0-9,0为不压缩，只打包;9为最大限度压缩;默认压缩等级为7)</param>
        /// <param name="buffersize">压缩时压缩类所用的缓冲区大小</param>
        /// <param name="async">是否执行异步压缩</param>
        public static void Compress(string sourcePath, string outPutPath, string password, int level, int buffersize, bool async = false)
        {
            if (!async)
                using (Stream outputFileStream = File.Create(GetOutputFileName(sourcePath, outPutPath)))
                {
                    ////进入压缩
                    Compress(sourcePath, outputFileStream, password, level, buffersize, async);
                    outputFileStream.Flush();
                    outputFileStream.Close();
                }
            else
            {
                Stream outputFileStream = File.Create(GetOutputFileName(sourcePath, outPutPath));
                ////进入压缩
                Compress(sourcePath, outputFileStream, password, level, buffersize);
            }
        }

        /// <summary>
        /// 将文件压缩后的内容写入到压缩输出流中(不关闭输出流)
        /// </summary>
        /// <param name="sourcePath">要压缩的文件的路径(文件夹或文件)</param>
        /// <param name="outputStream">写入的压缩流</param>
        /// <param name="password">压缩文件的密码(不需要密码可传入NULL或tring.Empty)</param>
        /// <param name="level">压缩等级(等级大小从0-9,0为不压缩，只打包;9为最大限度压缩;默认压缩等级为7)</param>
        /// <param name="buffersize">压缩时压缩类所用的缓冲区大小</param>
        /// <param name="async">是否执行异步压缩</param>
        public static void Compress(string sourcePath, Stream outputStream, string password, int level, int buffersize, bool async = false)
        {
            CompressParams objectParams = new CompressParams
                {
                    SourcesPath = sourcePath,
                    OutputStream = outputStream,
                    Password = password,
                    Level = level,
                    Buffersize = buffersize,
                    Async = async,
                    IsStream = true
                };

            if (async)
            {
                Thread compressThread = new Thread(new ParameterizedThreadStart(Compressd));
                compressThread.Start(objectParams);
            }
            else
            {
                Compressd(objectParams);
            }
        }

        /// <summary>
        /// 将文件压缩后的内容写入到压缩输出流中
        /// </summary>
        /// <param name="sourcePath">要压缩的文件的路径(文件夹或文件)</param>
        /// <param name="outputStream">写入的压缩流</param>
        /// <param name="password">压缩文件的密码(不需要密码可传入NULL或tring.Empty)</param>
        /// <param name="level">压缩等级(等级大小从0-9,0为不压缩，只打包;9为最大限度压缩;默认压缩等级为7)</param>
        /// <param name="buffersize">压缩时压缩类所用的缓冲区大小</param>        
        private static void Compress(string sourcePath, Stream outputStream, string password, int level, int buffersize)
        {
            CompressParams objectParams = new CompressParams
            {
                SourcesPath = sourcePath,
                OutputStream = outputStream,
                Password = password,
                Level = level,
                Buffersize = buffersize,
                Async = true,
            };
            Thread compressThread = new Thread(new ParameterizedThreadStart(Compressd));
            compressThread.Start(objectParams);
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="o">压缩参数</param>
        private static void Compressd(object o)
        {
            CompressParams c = (CompressParams)o;
            byte[] buffer = new byte[c.Buffersize];
            using (ZipOutputStream zipOutputStream = new ZipOutputStream(c.OutputStream))
            {
                ////进入写压缩
                Compress(c.SourcesPath, zipOutputStream, c.Password, c.Level, c.Buffersize);
                if (c.IsStream)
                {
                    zipOutputStream.deflater_.Flush();
                    zipOutputStream.Deflate();
                    zipOutputStream.DeflaterOutputStreamClose();
                }
                else
                {
                    zipOutputStream.Flush();
                    zipOutputStream.Close();
                }
            }
        }

        /// <summary>
        /// 将文件压缩后的内容写入到包装的压缩输出流中
        /// </summary>
        /// <param name="sourcesPath">要压缩的文件的路径(文件夹或文件)</param>
        /// <param name="zipOutputStream">写入的压缩流</param>
        /// <param name="password">压缩文件的密码</param>
        /// <param name="level">压缩等级(等级大小从0-9,0为不压缩，只打包;9为最大限度压缩;默认压缩等级为7)</param>
        /// <param name="buffersize">压缩时压缩类所用的缓冲区大小</param>        
        private static void Compress(string sourcesPath, ZipOutputStream zipOutputStream, string password, int level, int buffersize)
        {
            byte[] buffer = new byte[buffersize];
            zipOutputStream.SetLevel(level);
            ////是否设置文件密码
            if (!password.IsNullOrEmptyOrBlank())
                zipOutputStream.Password = password;

            Compress(sourcesPath.TrimEnd('\\'), sourcesPath, zipOutputStream, buffer);
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="rootPath">压缩文件路径</param>
        /// <param name="path">压缩文件夹内当前要压缩的文件夹路径</param>
        /// <param name="zipOutputStream">要写入的压缩流</param>
        /// <param name="buffer">读取文件的缓冲区大小</param>
        private static void Compress(string rootPath, string path, ZipOutputStream zipOutputStream, byte[] buffer)
        {
            string[] fileNames = rootPath.IndexOf('.') != -1 ? new string[] { Path.GetFullPath(rootPath) } : Directory.GetFiles(path);
            string[] dirNames = rootPath.IndexOf('.') != -1 ? new string[0] : Directory.GetDirectories(path);
            int sourceBytes;
            string relativePath = path.Replace(rootPath, string.Empty);
            if (!relativePath.IsNullOrEmptyOrBlank())
            {
                relativePath = relativePath.Replace("\\", "/") + "/";
            }

            ZipEntry entry;

            foreach (string file in fileNames)
            {
                entry = new ZipEntry(relativePath + Path.GetFileName(file));
                entry.DateTime = DateTime.Now;
                entry.AESKeySize = DefaultAESKeySize;
                zipOutputStream.PutNextEntry(entry);
                using (FileStream fs = File.OpenRead(file))
                {
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        zipOutputStream.Write(buffer, 0, sourceBytes);
                    }
                    while (sourceBytes > 0);
                }
            }

            string relativeDirPath;

            foreach (string dirName in dirNames)
            {
                relativeDirPath = dirName.Replace(rootPath, string.Empty);
                entry = new ZipEntry(relativeDirPath.Replace("\\", "/") + "/");
                entry.AESKeySize = DefaultAESKeySize;
                zipOutputStream.PutNextEntry(entry);
                Compress(rootPath, dirName, zipOutputStream, buffer);
            }
        }

        /// <summary>
        /// 获取要压缩的文件或文件夹压缩后的名称
        /// </summary>
        /// <param name="sourcePath">要压缩的数据源(文件或文件夹)</param>
        /// <param name="outputPath">文件压缩后的输出路径</param>
        /// <returns>完整输出路径</returns>
        private static string GetOutputFileName(string sourcePath, string outputPath)
        {
            if (outputPath.IndexOf('.') == -1 && sourcePath.IndexOf('.') == -1)
                outputPath += sourcePath.Substring(sourcePath.LastIndexOf("\\")) + ".zip";

            if (outputPath.IndexOf('.') == -1 && sourcePath.IndexOf(".") != -1)
            {
                int dotindex = sourcePath.LastIndexOf('.');
                int dirindex = sourcePath.LastIndexOf('\\');
                outputPath += sourcePath.Substring(dirindex + 1, dotindex - dirindex) + ".zip";
            }

            return outputPath;
        }

        /// <summary>
        /// 默认的缓冲区大小
        /// </summary>
        public static int DefaultBufferSize = 1024;

        /// <summary>
        /// 默认的对文件加密时的密钥位数
        /// </summary>
        public static int DefaultAESKeySize = 256;

        /// <summary>
        /// 压缩时默认的压缩等级，介于 （0-9）
        /// </summary>
        public static int StaticLevel = 7;

        /// <summary>
        /// 辅助多线程传递参数类
        /// </summary>
        private class CompressParams
        {
            public string SourcesPath;
            public Stream OutputStream;
            public string Password;
            public int Level;
            public int Buffersize;
            public bool Async;
            public bool IsStream = false;
        }

        #endregion
    }

    /// <summary>
    /// 文件压缩类
    /// </summary>
    public partial class FileCompress
    {
        #region 此部分为负责解压缩静态方法

        /// <summary>
        /// 解压缩zip文件
        /// </summary>
        /// <param name="zipFilePath">解压的zip文件路径</param>
        /// <param name="extractPath">解压到的文件夹路径</param>
        /// <param name="bufferSize">读取文件的缓冲区大小</param>
        public static void DeCompress(string zipFilePath, string extractPath, int bufferSize)
        {
            extractPath = extractPath.TrimEnd('\\') + "\\";
            byte[] data = new byte[bufferSize];
            int size;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry entry;
                while ((entry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name);

                    ////先创建目录
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(extractPath + directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(extractPath + entry.Name.Replace("/", "\\")))
                        {
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 解压缩zip文件
        /// </summary>
        /// <param name="zipFilePath">解压的zip文件路径</param>
        /// <param name="extractPath">解压到的文件夹路径</param>
        /// <param name="bufferSize">读取文件的缓冲区大小</param>
        public static void DeGzCompress(string zipFilePath, string extractPath, int bufferSize)
        {
            extractPath = extractPath.TrimEnd('\\') + "\\";
            byte[] data = new byte[bufferSize];
            int size;
            using (GZipInputStream s = new GZipInputStream(File.OpenRead(zipFilePath)))
            {
                string directoryName = Path.GetDirectoryName(zipFilePath);
                string fileName = Path.GetFileName(zipFilePath);

                using (FileStream streamWriter = File.Create(extractPath + fileName.Replace("/", "\\")))
                {
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        #endregion
    }
}