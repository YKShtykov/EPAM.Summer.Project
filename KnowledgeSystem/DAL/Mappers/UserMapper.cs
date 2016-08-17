using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using ORM;

namespace DAL.Mappers
{
    public static class UserMapper
    {
        public static User MapUser(DalUser user)
        {
            User result = new User
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                PasswordSalt = user.PasswordSalt
            };

            return result;
        }

        public static DalUser MapUser(User user)
        {
            DalUser result = new DalUser
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                PasswordSalt = user.PasswordSalt
            };

            foreach (var role in user.Roles)
            {
                result.Roles.Add(role.Name);
            }

            return result;
        }
    }
}
