using System.Text;
using CryptoLogic.Interface;
using System.Security.Cryptography;

namespace CryptoLogic
{
    /// <summary>
    /// Service class for hashing users passwords
    /// </summary>
    public class PasswordService : IPasswordService
    {
        private const int SaltLength = 128;

        /// <summary>
        /// Hashing key
        /// </summary>
        public string Key
        {
            get
            {
                var cryptoService = new RNGCryptoServiceProvider();
                var saltBytes = new byte[SaltLength];
                cryptoService.GetNonZeroBytes(saltBytes);
                return Encoding.Unicode.GetString(saltBytes);
            }
            set
            {
            }
        }

        /// <summary>
        /// Get hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="key"></param>
        /// <returns>hashed password</returns>
        public string GetHash(string password, string key)
        {
            var bytes = Encoding.Unicode.GetBytes(password + key);
            var hashed = MD5.Create().ComputeHash(bytes);
            return Encoding.Unicode.GetString(hashed);
        }

        /// <summary>
        /// Check input password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="key"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string key, string hash)
        {
            return hash == GetHash(password, key);
        }
    }
}
