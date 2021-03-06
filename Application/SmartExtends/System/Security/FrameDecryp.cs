﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace System.Security
{
    /// <summary>
    /// 超级解密类
    /// </summary>
    public static partial class Decrypt
    {
        /// <summary>
        /// 对称加密输入结果，返回解密后的结果
        /// </summary>
        /// <param name="input">要解密的内容</param>
        /// <param name="key">加密用密钥</param>
        /// <param name="iv">加密用的初始化向量</param>
        /// <param name="encryptSymmetryType">加密算法</param>
        /// <returns>解密后的结果</returns>
        public static string DecryptString(this string input, string key, string iv, EncryptSymmetryType encryptSymmetryType)
        {
            byte[] keY = Convert.FromBase64String(key);
            byte[] iV = Convert.FromBase64String(iv);
            return DecryptString(input, keY, iV, encryptSymmetryType);
        }

        /// <summary>
        /// 对称加密后的字符串，返回解密后的结果
        /// </summary>
        /// <param name="input">要解密的内容</param>
        /// <param name="key">加密用密钥</param>
        /// <param name="iv">加密用的初始化向量</param>
        /// <param name="encryptSymmetryType">加密算法</param>
        /// <returns>解密后的结果</returns>
        public static string DecryptString(this string input, byte[] key, byte[] iv, EncryptSymmetryType encryptSymmetryType)
        {
            if (input == null || input.Length <= 0)
                throw new ArgumentNullException("input");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] cipherBytes = Convert.FromBase64String(input);
            string plaintext = string.Empty;
            SymmetricAlgorithm symmetricAlgorithm = null;
            switch (encryptSymmetryType)
            {
                case EncryptSymmetryType.DES:
                case EncryptSymmetryType.Rijandel:
                case EncryptSymmetryType.TripleDES:
                case EncryptSymmetryType.RC2:
                    {
                        symmetricAlgorithm = Encrypt.GetSymmetricAlgorithmBySmartEncryptSymmetryType(encryptSymmetryType);
                        symmetricAlgorithm.Key = key;
                        symmetricAlgorithm.IV = iv;
                        ICryptoTransform decryptor = symmetricAlgorithm.CreateDecryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV);
                        using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader streamReader = new StreamReader(cryptoStream))
                                    plaintext = streamReader.ReadToEnd();
                            }
                        }

                        if (symmetricAlgorithm != null)
                            symmetricAlgorithm.Clear();
                    }

                    break;
            }

            return plaintext;
        }
    }

    /// <summary>
    /// 超级解密类
    /// </summary>
    public static partial class Decrypt
    {
        /// <summary>
        /// 用公钥验证已用该公钥对应的私钥签名的数据(采用SHA1散列加密的数据)
        /// 采用 DSA 算法
        /// </summary>
        /// <param name="hashValue">原始数据</param>
        /// <param name="signedHashValue">已签名的数据</param>
        /// <param name="publicKeyXmlStirng">公钥</param>        
        /// <returns>验证结果</returns>
        public static bool DSAVerifyHash(byte[] hashValue, byte[] signedHashValue, string publicKeyXmlStirng)
        {
            bool verified = false;
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                dsa.FromXmlString(publicKeyXmlStirng);
                DSASignatureDeformatter dsaDeformatter = new DSASignatureDeformatter(dsa);
                dsaDeformatter.SetHashAlgorithm("SHA1");
                verified = dsaDeformatter.VerifySignature(hashValue, signedHashValue);
            }

            return verified;
        }

        /// <summary>
        /// 用公钥验证已用该公钥对应的私钥签名的数据(采用SHA1散列加密的数据)
        /// 采用 DSA 算法
        /// </summary>
        /// <param name="oldValue">原始数据</param>
        /// <param name="signedStringValue">已签名的数据的base64编码后的字符串</param>
        /// <param name="publicKeyXmlStirng">公钥</param>        
        /// <returns>验证结果</returns>
        public static bool DSAVerifyHash(this string oldValue, string signedStringValue, string publicKeyXmlStirng)
        {
            byte[] hashValue = (new SHA1CryptoServiceProvider()).ComputeHash(Encoding.Default.GetBytes(oldValue));
            ////byte[] signedHashValue = (new SHA1CryptoServiceProvider()).ComputeHash(Encoding.Default.GetBytes(signedStringValue));
            byte[] signedHashValue = Convert.FromBase64String(signedStringValue);
            return DSAVerifyHash(hashValue, signedHashValue, publicKeyXmlStirng);
        }


        /// <summary>
        /// 用公钥验证已用该公钥对应的私钥签名的数据(采用SHA1散列加密的数据)
        /// 采用 RSA 算法
        /// </summary>
        /// <param name="hashValue">原始数据</param>
        /// <param name="signedHashValue">已签名的数据</param>
        /// <param name="publicKeyXmlStirng">公钥</param>        
        /// <returns>验证结果</returns>
        public static bool RSAVerifyHash(byte[] hashValue, byte[] signedHashValue, string publicKeyXmlStirng)
        {
            bool verified = false;
            using (RSACryptoServiceProvider dsa = new RSACryptoServiceProvider())
            {
                dsa.FromXmlString(publicKeyXmlStirng);
                RSAPKCS1SignatureDeformatter dsaDeformatter = new RSAPKCS1SignatureDeformatter(dsa);
                dsaDeformatter.SetHashAlgorithm("SHA1");
                verified = dsaDeformatter.VerifySignature(hashValue, signedHashValue);
            }

            return verified;
        }

        /// <summary>
        /// 用公钥验证已用该公钥对应的私钥签名的数据(采用SHA1散列加密的数据)
        /// 采用 RSA 算法
        /// </summary>
        /// <param name="oldValue">原始数据</param>
        /// <param name="signedStringValue">已签名的数据的base64编码后的字符串</param>
        /// <param name="publicKeyXmlStirng">公钥</param>        
        /// <returns>验证结果</returns>
        public static bool RSAVerifyHash(this string oldValue, string signedStringValue, string publicKeyXmlStirng)
        {
            byte[] hashValue = (new SHA1CryptoServiceProvider()).ComputeHash(Encoding.Default.GetBytes(oldValue));
            byte[] signedHashValue = Convert.FromBase64String(signedStringValue);
            return RSAVerifyHash(hashValue, signedHashValue, publicKeyXmlStirng);
        }

        /// <summary>
        /// 返回用RSA解密的数据
        /// </summary>
        /// <param name="encryptString">加密后的字符串</param>
        /// <param name="rsaKeyXmlStirng">公钥或私钥 xml </param>
        /// <returns></returns>
        public static string RSADecrypString(this string encryptString, string rsaKeyXmlStirng)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider() { PersistKeyInCsp = false };
            rsa.FromXmlString(rsaKeyXmlStirng);
            return Encoding.Default.GetString(rsa.Decrypt(Encoding.Default.GetBytes(encryptString), false));
        }
    }
}
