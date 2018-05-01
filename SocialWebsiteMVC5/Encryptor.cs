using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SocialWebsiteMVC5
{
    public class Encryptor
    {
        public static string MD5Hash(string Value)
        {
            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(Value);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            return encoded;
        }
    }
}