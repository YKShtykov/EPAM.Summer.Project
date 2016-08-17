using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class User
    {
        public User()
        {
            Roles = new List<Role>();
            UsersSkills = new List<UserSkill>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        
        public Profile Profile { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserSkill> UsersSkills { get; set; }
    }
}
