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
        public static User Map(DalUser user)
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

        public static DalUser Map(User user)
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

        public static IEnumerable<DalUser> Map(IQueryable<User> users)
        {
            var dalUsers = new List<DalUser>();
            foreach (var item in users)
            {
                dalUsers.Add(Map(item));
            }
            return dalUsers;
        }
    }
}
