using System;
using System.Security.Cryptography;
using System.Text;

namespace Standard.Helper
{
    public class Encryption
    {
        public string PrivateKey { get; private set; }

        public Encryption(string privateKey = null)
        {
            if (string.IsNullOrWhiteSpace(privateKey))
            {
                CreatePrivateKey();
                return;
            }

            PrivateKey = privateKey;
        }

        public string Encrypt(string input)
        {
            //Input has to match key encoding
            byte[] inputArray = Encoding.UTF8.GetBytes(input);
            //Init crypto
            var tripleDes = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(PrivateKey),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tripleDes.CreateEncryptor();
            //Encrypt byte array
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDes.Clear();
            //Convert and return
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string input)
        {
            //Input has to match key encoding
            byte[] inputArray = Convert.FromBase64String(input);
            //Init crypto for cypher
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(PrivateKey),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            //Cypher byte array
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            //Convert and return
            return Encoding.UTF8.GetString(resultArray);
        }



        public void CreatePrivateKey(int length = 24)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#";
            StringBuilder key = new StringBuilder();
            Random rand = new Random();
            while (0 < length--)
            {
                key.Append(validChars[rand.Next(validChars.Length)]);
            }
            PrivateKey = key.ToString();
        }
    }
}
