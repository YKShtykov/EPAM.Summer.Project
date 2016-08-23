using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;


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
    }
}