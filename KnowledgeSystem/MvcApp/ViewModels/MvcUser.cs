using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp.ViewModels
{
    public class MvcUser
    {
        public MvcUser()
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