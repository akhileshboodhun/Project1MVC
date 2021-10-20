using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Project1MVC.Services
{
    public class CryptographyProcessor
    {
        public CryptographyProcessor()
        {
            this.size = 64;
        }
        public CryptographyProcessor(int size)
        {
            this.size = size;
        }

        public int size { get; set; }

        public string CreateSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateSaltedHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return salt + Convert.ToBase64String(hash);
        }

        public bool AreEqual(string plainTextInput, string saltedHashInput)
        {
            string salt = saltedHashInput.Split('=')[0] + "==";
            string newHashedPin = GenerateSaltedHash(plainTextInput, salt);
            return newHashedPin.Equals(saltedHashInput);
        }
    }
}