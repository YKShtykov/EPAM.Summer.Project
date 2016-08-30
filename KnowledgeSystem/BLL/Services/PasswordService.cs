using System.Text;
using System.Security.Cryptography;

namespace BLL
{
    /// <summary>
    /// Service class for hashing users passwords
    /// </summary>
    public static class PasswordService
    {
        private const int SaltLength = 128;

        /// <summary>
        /// The method for generation password salt
        /// </summary>
        /// <returns>Password sat</returns>
        public static string GenerateSalt()
        {
            var cryptoService = new RNGCryptoServiceProvider();
            var saltBytes = new byte[SaltLength];
            cryptoService.GetNonZeroBytes(saltBytes);
            return Encoding.Unicode.GetString(saltBytes);
        }

        /// <summary>
        /// The method for verification of password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="encoded"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string passwordSalt, string encoded)
        {
            return encoded == GetHash(password, passwordSalt);
        }

        /// <summary>
        /// The method for getting hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string GetHash(string password, string salt)
        {
            var bytes = Encoding.Unicode.GetBytes(password + salt);
            var hashed = MD5.Create().ComputeHash(bytes);
            return Encoding.Unicode.GetString(hashed);
        }
    }
}
