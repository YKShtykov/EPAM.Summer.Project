using System;

namespace BLL.Interface
{
    /// <summary>
    /// Exception class for registration or login errors such as occupied email
    /// </summary>
    public class AccountException: Exception
    {
        /// <summary>
        /// Create account exception
        /// </summary>
        /// <param name="message"></param>
        public AccountException(string message) : base(message) { }

        /// <summary>
        /// Create account exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AccountException(string message, Exception innerException) : base(message, innerException) { }
    }
}
