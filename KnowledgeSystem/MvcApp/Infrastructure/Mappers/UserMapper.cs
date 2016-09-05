using System.Collections.Generic;
using MvcApp.ViewModels;
using BLL.Interface;


namespace MvcApp.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static BllUser Map(MvcUser user)
        {
            BllUser result = new BllUser
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                PasswordSalt = user.PasswordSalt,
                Roles = user.Roles
            };

            return result;
        }

        public static MvcUser Map(BllUser user)
        {
            MvcUser result = new MvcUser
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                PasswordSalt = user.PasswordSalt,
                Roles = user.Roles
            };

            return result;
        }

        public static BllUser Map(RegisterModel user)
        {
            

            BllUser result = new BllUser
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                PasswordSalt = "",
                Roles = new List<string>() { "User" }
            };

            return result;
        }

        public static IEnumerable<MvcUser> Map(IEnumerable<BllUser> users)
        {
            var MvcUserList = new List<MvcUser>();

            foreach (var item in users)
            {
                MvcUserList.Add(Map(item));
            }

            return MvcUserList;
        }
    }
}