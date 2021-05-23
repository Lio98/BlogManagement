using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlogManagement.Core
{
    public class Encrypt
    {
        public static string MD5Encrypt(string source, int length = 32)
        {
            HashAlgorithm provider = CryptoConfig.CreateFromName("MD5") as HashAlgorithm;
            if (string.IsNullOrEmpty(source)) return string.Empty;
            byte[] bytes = Encoding.ASCII.GetBytes(source);
            byte[] hashValue = provider.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            switch (length)
            {
                case 16://16位密文是32位密文的9到24位字符
                    for (int i = 4; i < 12; i++)
                        sb.Append(hashValue[i].ToString("x2"));
                    break;
                case 32:
                    for (int i = 0; i < 16; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                default:
                    for (int i = 0; i < hashValue.Length; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
            }
            return sb.ToString();
        }
    }
}
