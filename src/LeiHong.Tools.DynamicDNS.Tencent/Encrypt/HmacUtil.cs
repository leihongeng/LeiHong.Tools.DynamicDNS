using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LeiHong.Tools.DynamicDNS.TencentCloud.Encrypt
{
    public class HmacUtil
    {
        public static string EncryptWithSha256(string data, string secret)
        {
            secret ??= "";
            var encoding = Encoding.UTF8;
            var keyByte = encoding.GetBytes(secret);
            var dataBytes = encoding.GetBytes(data);
            using var hmac256 = new HMACSHA256(keyByte);
            var hashData = hmac256.ComputeHash(dataBytes);
            return Convert.ToBase64String(hashData);
        }

        public static string EncryptWithSha1(string data, string secret)
        {
            secret ??= "";
            var encoding = Encoding.UTF8;
            var keyByte = encoding.GetBytes(secret);
            var dataBytes = encoding.GetBytes(data);
            using var hmac1 = new HMACSHA1(keyByte);
            var hashData = hmac1.ComputeHash(dataBytes);
            return Convert.ToBase64String(hashData);
        }

        public static string EncryptWithSha256Original(string data, string secret)
        {
            secret ??= "";
            var encoding = Encoding.UTF8;
            var keyByte = encoding.GetBytes(secret);
            var dataBytes = encoding.GetBytes(data);
            using var hmac256 = new HMACSHA256(keyByte);
            var hashData = hmac256.ComputeHash(dataBytes);
            return BitConverter.ToString(hashData).Replace("-", "").ToLower();
        }

        public static string EncryptWithSha1Original(string data, string secret)
        {
            secret ??= "";
            var encoding = Encoding.UTF8;
            var keyByte = encoding.GetBytes(secret);
            var dataBytes = encoding.GetBytes(data);
            using var hmac1 = new HMACSHA1(keyByte);
            var hashData = hmac1.ComputeHash(dataBytes);
            return BitConverter.ToString(hashData).Replace("-", "").ToLower();
        }
    }
}
