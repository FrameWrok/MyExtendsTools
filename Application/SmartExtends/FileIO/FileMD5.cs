using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace System.IO
{
    /// <summary>
    /// 计算文件的 MD5 值的类
    /// </summary>
    public static partial class FileMD5
    {
        /// <summary>
        /// 获取文件的 MD5 值
        /// </summary>
        /// <param name="file">要获取 MD5 值的文件</param>
        /// <param name="algEnum">算法</param>
        /// <returns>文件的 MD5 值</returns>
        public static string GetFileMD5(this FileInfo file, AlgEnum algEnum)
        {
            if (!file.Exists)
                return null;
            return HashData(file.Open(FileMode.Open), algEnum).ToHexString();
        }

        private static byte[] HashData(Stream stream, AlgEnum algEnum)
        {
            HashAlgorithm algorithm;

            switch (algEnum)
            {
                case AlgEnum.MD5:
                    {
                        algorithm = MD5.Create();
                    }
                    break;
                case AlgEnum.SHA1:
                    {
                        algorithm = SHA1.Create();
                    }
                    break;
                default:
                    algorithm = MD5.Create();
                    break;
            }
            return algorithm.ComputeHash(stream);
        }
    }

    /// <summary>
    /// 算法
    /// </summary>
    public enum AlgEnum
    {
        /// <summary>
        /// MD5 算法
        /// </summary>
        MD5,
        /// <summary>
        /// SHA1算法
        /// </summary>
        SHA1
    }
}
