using System.Collections.Generic;

namespace ORM
{
    /// <summary>
    /// ORM Layout Role class
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Create ORM Role entity
        /// </summary>
        public Role()
        {
            Users = new List<User>();
        }

        /// <summary>
        /// Role identify number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// All ORM users, whose have the role. It need for DataBase creating
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
