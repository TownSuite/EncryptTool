using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = args[0];
            bool useHash = true;
            bool.TryParse(args[1], out useHash);
            var key = args[2];

            Console.Write(Encrypt(value, useHash, key));
        }

        private static string Encrypt(string toEncrypt, bool useHashing, string key)
        {
            var toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            byte[] keyArray;

            if (useHashing)
            {
                using (var hashmd5 = new MD5CryptoServiceProvider())
                {
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = keyArray;

                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                var cTransform = tdes.CreateEncryptor();

                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }
    }
}
