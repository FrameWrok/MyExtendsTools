/**********************************************************
 ◆项目：
 ◆公司：
 ◆作者：李丙龙
 ◆邮箱：794544095@qq.com
 ◆创建：2012-01-01
 ◆版本：1.0
**********************************************************/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace System.Encrypt
{
    /// <summary>
    /// 超级加密类
    /// </summary>
    public static partial class Encrypt
    {
        /// <summary>
        /// 输入要对称加密的原文，返回加密后的结果，输出密钥和初初始化变量
        /// 密钥字节长度采用默认值
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">加密的密钥</param>
        /// <param name="iv">加密的初始化变量</param>        
        /// <param name="encryptSymmetryType">要采用的对称加密算法</param>        
        /// <returns>加密后的结果</returns>
        public static string EncryptString(string input, out string key, out string iv, FrameEncryptSymmetryType encryptSymmetryType)
        {
            string result;
            string keY;
            string iV;
            result = EncryptString(input, out keY, out iV, int.MinValue, encryptSymmetryType);
            key = keY;
            iv = iV;
            return result;
        }

        /// <summary>
        /// 输入要对称加密的原文，返回加密后的结果，输出密钥和初初始化变量
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">加密的密钥</param>
        /// <param name="iv">加密的初始化变量</param>        
        /// <param name="encryptSymmetryType">要采用的对称加密算法</param>        
        /// <returns>加密后的结果</returns>
        public static byte[] EncryptString(string input, out byte[] key, out byte[] iv, FrameEncryptSymmetryType encryptSymmetryType)
        {
            byte[] resultBuffer;
            byte[] keyBytes;
            byte[] ivandBytes;
            resultBuffer = EncryptString(input, out keyBytes, out ivandBytes, int.MinValue, encryptSymmetryType);
            key = keyBytes;
            iv = ivandBytes;
            return resultBuffer;
        }

        /// <summary>
        /// 输入要对称加密的原文，返回加密后的结果，输出密钥和初初始化变量
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">加密的密钥</param>
        /// <param name="iv">加密的初始化变量</param>
        /// <param name="bitLength">密钥的字节长度</param>
        /// <param name="encryptSymmetryType">要采用的对称加密算法</param>        
        /// <returns>加密后的结果</returns>
        private static string EncryptString(string input, out string key, out string iv, int bitLength, FrameEncryptSymmetryType encryptSymmetryType)
        {
            byte[] resultBuffer;
            byte[] keyBytes;
            byte[] ivandBytes;
            resultBuffer = EncryptString(input, out keyBytes, out ivandBytes, bitLength, encryptSymmetryType);
            key = Convert.ToBase64String(keyBytes);
            iv = Convert.ToBase64String(ivandBytes);
            return Convert.ToBase64String(resultBuffer);
        }

        /// <summary>
        /// 输入要对称加密的原文，返回加密后的结果，输出密钥和初初始化变量
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">加密的密钥</param>
        /// <param name="iv">加密的初始化变量</param>
        /// <param name="bitLength">密钥的字节长度</param>
        /// <param name="encryptSymmetryType">要采用的对称加密算法</param>        
        /// <returns>加密后的结果</returns>
        private static byte[] EncryptString(string input, out byte[] key, out byte[] iv, int bitLength, FrameEncryptSymmetryType encryptSymmetryType)
        {
            if (input == null || input.Length <= 0)
                throw new ArgumentNullException("input");
            SymmetricAlgorithm symmetricAlgorithm = null;
            byte[] plainBytes = null;
            key = null;
            iv = null;
            MemoryStream memoryStream = new MemoryStream();
            switch (encryptSymmetryType)
            {
                case FrameEncryptSymmetryType.DES:
                case FrameEncryptSymmetryType.Rijandel:
                case FrameEncryptSymmetryType.TripleDES:
                case FrameEncryptSymmetryType.RC2:
                    {
                        symmetricAlgorithm = Encrypt.GetSymmetricAlgorithmBySmartEncryptSymmetryType(encryptSymmetryType);
                        if (bitLength != int.MinValue && !symmetricAlgorithm.ValidKeySize(bitLength))
                            throw new Exception("密钥大小对当前算法无效");
                        if (bitLength != int.MinValue && symmetricAlgorithm.ValidKeySize(bitLength))
                            symmetricAlgorithm.KeySize = bitLength;
                        symmetricAlgorithm.GenerateKey();
                        symmetricAlgorithm.GenerateIV();
                        key = symmetricAlgorithm.Key;
                        iv = symmetricAlgorithm.IV;
                        ICryptoTransform decryptor = symmetricAlgorithm.CreateEncryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV);
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(input);
                            }
                        }

                        if (symmetricAlgorithm != null)
                            symmetricAlgorithm.Clear();
                    }

                    break;
            }

            plainBytes = memoryStream.ToArray();
            return plainBytes;
        }

        /// <summary>
        /// 用输入的密钥和初始化向量，对输入的文本进行加密，返回加密后的结果
        /// 密钥与初始话向量不可为汉语
        /// 密钥字节长度采用默认值
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">加密用的密钥</param>
        /// <param name="iv">加密用的初始化变量</param>        
        /// <param name="encryptSymmetryType">要采用的对称加密算法</param>
        /// <returns>加密后的结果</returns>
        private static string EncryptStringByKeyIV(string input, string key, string iv, FrameEncryptSymmetryType encryptSymmetryType)
        {
            return EncryptStringByKeyIV(input, key, iv, int.MinValue, encryptSymmetryType);
        }

        /// <summary>
        /// 用输入的密钥和初始化向量，对输入的文本进行加密，返回加密后的结果
        /// 密钥与初始话向量不可为汉语
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">加密用的密钥</param>
        /// <param name="iv">加密用的初始化变量</param>
        /// <param name="bitLength">密钥的字节长度</param>
        /// <param name="encryptSymmetryType">要采用的对称加密算法</param>
        /// <returns>加密后的结果</returns>
        private static string EncryptStringByKeyIV(string input, string key, string iv, int bitLength, FrameEncryptSymmetryType encryptSymmetryType)
        {
            if (input == null || input.Length <= 0)
                throw new ArgumentNullException("input");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");
            byte[] resultBuffer;
            byte[] keyBytes = Encoding.Default.GetBytes(key);
            byte[] ivandBytes = Encoding.Default.GetBytes(iv);
            resultBuffer = EncryptStringByKeyIV(input, keyBytes, ivandBytes, (bitLength == int.MinValue ? keyBytes.Length : bitLength), encryptSymmetryType);
            return Convert.ToBase64String(resultBuffer);
        }

        /// <summary>
        /// 用输入的密钥和初始化向量，对输入的文本进行加密，返回加密后的结果
        /// 密钥与初始话向量不可为汉语
        /// 密钥字节长度采用默认值
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">加密用的密钥</param>
        /// <param name="iv">加密用的初始化变量</param>        
        /// <param name="encryptSymmetryType">要采用的对称加密算法</param>
        /// <returns>加密后的结果</returns>
        private static byte[] EncryptStringByKeyIV(string input, byte[] key, byte[] iv, FrameEncryptSymmetryType encryptSymmetryType)
        {
            return EncryptStringByKeyIV(input, key, iv, int.MinValue, encryptSymmetryType);
        }

        /// <summary>
        /// 用输入的密钥和初始化向量，对输入的文本进行加密，返回加密后的结果
        /// 密钥与初始话向量不可为汉语
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="key">加密用的密钥</param>
        /// <param name="iv">加密用的初始化变量</param>
        /// <param name="bitLength">密钥的字节长度</param>
        /// <param name="encryptSymmetryType">要采用的对称加密算法</param>
        /// <returns>加密后的结果</returns>
        private static byte[] EncryptStringByKeyIV(string input, byte[] key, byte[] iv, int bitLength, FrameEncryptSymmetryType encryptSymmetryType)
        {
            if (input == null || input.Length <= 0)
                throw new ArgumentNullException("input");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            SymmetricAlgorithm symmetricAlgorithm = null;
            byte[] plainBytes = null;
            MemoryStream memoryStream = new MemoryStream();
            switch (encryptSymmetryType)
            {
                case FrameEncryptSymmetryType.DES:
                case FrameEncryptSymmetryType.Rijandel:
                case FrameEncryptSymmetryType.TripleDES:
                case FrameEncryptSymmetryType.RC2:
                    {
                        symmetricAlgorithm = Encrypt.GetSymmetricAlgorithmBySmartEncryptSymmetryType(encryptSymmetryType);
                        if (bitLength != int.MinValue && !symmetricAlgorithm.ValidKeySize(bitLength))
                            throw new Exception("密钥大小对当前算法无效");
                        if (bitLength != int.MinValue && symmetricAlgorithm.ValidKeySize(bitLength))
                            symmetricAlgorithm.KeySize = bitLength;
                        symmetricAlgorithm.Key = key;
                        symmetricAlgorithm.IV = iv;
                        ICryptoTransform decryptor = symmetricAlgorithm.CreateEncryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV);
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(input);
                            }

                            if (symmetricAlgorithm != null)
                                symmetricAlgorithm.Clear();
                        }
                    }

                    break;
            }

            plainBytes = memoryStream.ToArray();
            return plainBytes;
        }
    }

    /// <summary>
    /// 超级加密类
    /// </summary>
    public static partial class Encrypt
    {
        /// <summary>
        /// 根据散列加密类获得用散列算法加密后的数据
        /// </summary>
        /// <param name="input">要加密的内容</param>
        /// <param name="encryptHashType">要采用的散列加密算法</param>
        /// <returns>加密后的数据</returns>
        public static string EncryptStringHash(string input, FrameEncryptHashType encryptHashType)
        {
            if (input == null || input.Length <= 0)
                throw new ArgumentNullException("input");
            HashAlgorithm hashAlgorithm = GetHashAlgorithmBySmartEncryptHashType(encryptHashType);
            byte[] data = hashAlgorithm.ComputeHash(Encoding.Default.GetBytes(input));
            string result = string.Empty;
            result = Convert.ToBase64String(data);
            return result;
        }
    }

    /// <summary>
    /// 超级加密类
    /// </summary>
    public static partial class Encrypt
    {
        #region  签名相关

        /// <summary>
        /// 根据SHA1散列加密算法，返回用非对称算法（DSA）的私钥对输入数据进行签名的结果，并输出公钥与私钥
        /// </summary>
        /// <param name="input">要加密的数据</param>
        /// <param name="publicKeyString">输出xml格式的公钥</param>
        /// <param name="privateKeyString">输出xml格式的私钥</param>        
        /// <returns>返回用私钥签名后的数据</returns>
        public static string DSASignaturetStringAsymmetric(string input, out string publicKeyString, out string privateKeyString)
        {
            publicKeyString = string.Empty;
            privateKeyString = string.Empty;
            byte[] rgbHash = (new SHA1CryptoServiceProvider()).ComputeHash(Encoding.Default.GetBytes(input));
            byte[] resultBytes;
            using (DSACryptoServiceProvider newDSA = new DSACryptoServiceProvider())
            {
                publicKeyString = newDSA.ToXmlString(false);
                privateKeyString = newDSA.ToXmlString(true);
                DSASignatureFormatter dsaFormatter = new DSASignatureFormatter(newDSA);
                dsaFormatter.SetHashAlgorithm("SHA1");
                resultBytes = dsaFormatter.CreateSignature(rgbHash);
            }

            return Convert.ToBase64String(resultBytes);
        }

        /// <summary>
        /// 根据输入的xml私钥返回用DSA进行签名的数据(采用SHA1散列加密的数据)
        /// </summary>
        /// <param name="input">要加密的数据</param>
        /// <param name="privataKeyXmlString">xml格式的私钥</param>        
        /// <returns>根据输入的私钥采用DSA算法签名后的数据</returns>
        public static string DSASignaturetStringAsymmetricByPrivateKey(string input, string privataKeyXmlString)
        {
            byte[] rgbHash = (new SHA1CryptoServiceProvider()).ComputeHash(Encoding.Default.GetBytes(input));
            byte[] resultBytes;
            using (DSACryptoServiceProvider newDSA = new DSACryptoServiceProvider())
            {
                newDSA.FromXmlString(privataKeyXmlString);
                DSASignatureFormatter dsaFormatter = new DSASignatureFormatter(newDSA);
                dsaFormatter.SetHashAlgorithm("SHA1");
                resultBytes = dsaFormatter.CreateSignature(rgbHash);
            }

            return Convert.ToBase64String(resultBytes);
        }

        #endregion

        /// <summary>
        /// 根据输入的xml公钥返回用DSA进行签名的数据(采用SHA1散列加密的数据)
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <param name="publicKeyString">公钥</param>
        /// <returns>解密后的内容</returns>
        public static string RSASignaturetStringAsymmetric(string input, string publicKeyString)
        {
            byte[] rgbHash = (new SHA1CryptoServiceProvider()).ComputeHash(Encoding.Default.GetBytes(input));
            byte[] resultBytes;
            using (RSACryptoServiceProvider newRSA = new RSACryptoServiceProvider())
            {
                newRSA.FromXmlString(publicKeyString);
                resultBytes = newRSA.EncryptValue(rgbHash);
            }

            return Convert.ToBase64String(resultBytes);
        }
    }

    /// <summary>
    /// 超级加密类
    /// </summary>
    public static partial class Encrypt
    {
        /// <summary>
        /// 根据对称加密算法类型获取对应的对称加密的实例
        /// </summary>
        /// <param name="encryptHashType">对称加密类型枚举</param>
        /// <returns>实例化的实例</returns>
        internal static SymmetricAlgorithm GetSymmetricAlgorithmBySmartEncryptSymmetryType(FrameEncryptSymmetryType encryptHashType)
        {
            SymmetricAlgorithm symmetricAlgorithm = null;
            switch (encryptHashType)
            {
                case FrameEncryptSymmetryType.Aes:
                    {
                        symmetricAlgorithm = AesCryptoServiceProvider.Create();
                    }

                    break;
                case FrameEncryptSymmetryType.AesManaged:
                    {
                        symmetricAlgorithm = new AesManaged();
                    }

                    break;
                case FrameEncryptSymmetryType.DES:
                    {
                        symmetricAlgorithm = new DESCryptoServiceProvider();
                    }

                    break;
                case FrameEncryptSymmetryType.TripleDES:
                    {
                        symmetricAlgorithm = new TripleDESCryptoServiceProvider();
                    }

                    break;
                case FrameEncryptSymmetryType.RC2:
                    {
                        symmetricAlgorithm = new RC2CryptoServiceProvider();
                    }

                    break;
                case FrameEncryptSymmetryType.Rijandel:
                    {
                        symmetricAlgorithm = new RijndaelManaged();
                    }

                    break;
            }

            return symmetricAlgorithm;
        }

        /// <summary>
        /// 根据散列加密类型返回散列加密类型实例
        /// </summary>
        /// <param name="smartEncryHashType">散列加密类型枚举</param>
        /// <returns>加密实例</returns>
        private static HashAlgorithm GetHashAlgorithmBySmartEncryptHashType(FrameEncryptHashType smartEncryHashType)
        {
            HashAlgorithm hashAlgorithm = null;
            switch (smartEncryHashType)
            {
                case FrameEncryptHashType.MD5:
                    {
                        hashAlgorithm = new MD5CryptoServiceProvider();
                    }

                    break;
                case FrameEncryptHashType.SHA1:
                    {
                        hashAlgorithm = new SHA1CryptoServiceProvider();
                    }

                    break;
                case FrameEncryptHashType.SHA256:
                    {
                        hashAlgorithm = new SHA256Managed();
                    }

                    break;
                case FrameEncryptHashType.SHA384:
                    {
                        hashAlgorithm = new SHA384Managed();
                    }

                    break;
                case FrameEncryptHashType.SHA512:
                    {
                        hashAlgorithm = new SHA512Managed();
                    }

                    break;
            }

            return hashAlgorithm;
        }
    }
}
