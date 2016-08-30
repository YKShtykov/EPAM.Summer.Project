using System.Collections.Generic;

namespace ORM
{
    /// <summary>
    /// ORM Layout User class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Create ORM User ENtity
        /// </summary>
        public User()
        {
            Roles = new List<Role>();
            UsersSkills = new List<UserSkill>();
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
        /// User email
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
        /// User Profile. It need for DataBase creating
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// User Roles. It need for DataBase creating
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// User Skills and their levels. It need for DataBase creating
        /// </summary>
        public virtual ICollection<UserSkill> UsersSkills { get; set; }
    }
}
