using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Security
{
    /// <summary>
    /// 散列加密类型枚举
    /// </summary>
    public enum EncryptHashType
    {
        // <summary>
        /// 比SHA1算法快，使用128的散列长度，在抵御暴力攻击方面不如 SHA1 强大
        /// .NET Framework
        /// 受以下版本支持：3.5、3.0 SP1、3.0、2.0 SP1、2.0、1.1、1.0
        /// .NET Compact Framework
        /// 受以下版本支持：3.5、2.0
        /// </summary>
        MD5,

        /// <summary>
        /// 在抵御暴力攻击方面比MD5强大
        /// 使用160位的散列长度
        /// </summary>
        SHA1,

        /// <summary>
        /// 比SHA1的散列长度更长，同时也更慢
        /// 使用256位的散列长度
        /// </summary>
        SHA256,

        /// <summary>
        /// 比 SHA256 的散列长度更长，同时也更慢
        /// 使用384位的散列长度
        /// </summary>
        SHA384,

        /// <summary>
        /// 比SHA384的散列长度更长，同时也更慢
        /// 使用512位的散列长度
        /// </summary>
        SHA512
    }

    /// <summary>
    /// 对称 加密类型枚举
    /// </summary>
    public enum EncryptSymmetryType
    {
        /// <summary>    
        /// DES  现在认为改加密算法是不安全的，因为它只使用56位的密钥长度        
        /// 可以在不超过 24 小时的时间内破解
        /// 受以下版本支持： .NET Framework 3.5、3.0 SP1、3.0、2.0 SP1、2.0、1.1、1.0
        /// </summary>
        DES,

        /// <summary>        
        /// TripleDES 是DES的继承者，其密钥长度为 168 位，但它提供的有效安全性只有 112 位
        /// 受以下版本支持：.NET Framework 3.5、3.0 SP1、3.0、2.0 SP1、2.0、1.1、1.0
        /// </summary>
        TripleDES,

        /// <summary> 
        /// 使用高级加密标准 (AES) 算法的加密应用程序编程接口 (CAPI) 实现来执行对称加密和解密。
        /// 受以下版本支持：.NET Framework 3.5
        /// </summary>
        Aes,

        /// <summary>
        /// 提供高级加密标准 (AES) 对称算法的托管实现。 
        /// </summary>
        AesManaged,

        /// <summary>
        /// 受以下版本支持：.NET Framework 3.5、3.0 SP1、3.0、2.0 SP1、2.0、1.1、1.0
        /// .NET Compact Framework 受以下版本支持：3.5、2.0
        /// </summary>
        RC2,

        /// <summary>
        /// 非常类似于 AES 它只是在密钥长度方面的选项比较多
        /// AES是美国政府采用的加密标准
        /// </summary>
        Rijandel
    }

    /// <summary>
    /// 非对称加密类型枚举
    /// </summary>
    public enum EncryptAsymmetricType
    {
        /// <summary>
        /// .NET Framework
        /// 受以下版本支持：4、3.5、3.0、2.0、1.1、1.0
        /// .NET Framework Client Profile
        /// 受以下版本支持：4、3.5 SP1
        /// </summary>
        DSA, 

        ////ECDsaCng,

        ////ECDiffieHellman,

        ////ECDiffieHellmanCng,

        /// <summary>        
        /// .NET Framework
        /// 受以下版本支持：4、3.5、3.0、2.0、1.1、1.0
        /// .NET Framework Client Profile
        /// 受以下版本支持：4、3.5 SP1
        /// </summary>
        RSA,
    }
}
