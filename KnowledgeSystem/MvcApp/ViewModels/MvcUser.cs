using System.Collections.Generic;

namespace MvcApp.ViewModels
{
    /// <summary>
    /// Class-model for mvc layout user
    /// </summary>
    public class MvcUser
    {
        /// <summary>
        /// Create mvc user
        /// </summary>
        public MvcUser()
        {
            Roles = new List<string>();
        }

        /// <summary>
        /// User identify number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// User mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User hashed password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// User password salt
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        public ICollection<string> Roles { get; set; }
    }
}