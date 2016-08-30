using System.Collections.Generic;

namespace BLL.Interface
{
    /// <summary>
    /// BLL Layout User class
    /// </summary>
    public class BllUser
    {
        /// <summary>
        /// Create BllUser entity
        /// </summary>
        public BllUser()
        {
            Roles = new List<string>();
        }
        /// <summary>
        /// BLL User identify number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// BLL User login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// BLL USer email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// BLL User hashed password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// BLL User password salt
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// BLL User roles names
        /// </summary>
        public ICollection<string> Roles { get; set; }
    }
}
