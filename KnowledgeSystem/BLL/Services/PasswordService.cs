using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BLL
{
    public static class PasswordService
    {
        private const int SaltLength = 128;

        public static string GenerateSalt()
        {
            var cryptoService = new RNGCryptoServiceProvider();
            var saltBytes = new byte[SaltLength];
            cryptoService.GetNonZeroBytes(saltBytes);
            return Encoding.Unicode.GetString(saltBytes);
        }

        public static bool VerifyPassword(string password, string passwordSalt, string encoded)
        {
            return encoded == GetHash(password, passwordSalt);
        }

        public static string GetHash(string password, string salt)
        {
            var bytes = Encoding.Unicode.GetBytes(password + salt);
            var hashed = MD5.Create().ComputeHash(bytes);
            return Encoding.Unicode.GetString(hashed);
        }
    }
}
