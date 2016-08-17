using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public class BllUser
    {
        public BllUser()
        {
            Roles = new List<string>();
        }
        public int Id { get; set; }

        public string Login { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
