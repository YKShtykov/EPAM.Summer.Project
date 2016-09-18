using System.Collections.Generic;
using MvcApp.ViewModels;
using BLL.Interface;


namespace MvcApp.Infrastructure.Mappers
{
    /// <summary>
    /// Class-mapper for users
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        /// Map user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Bll user such us user</returns>
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

        /// <summary>
        /// Map user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Mvc user such us user</returns>
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

        /// <summary>
        /// Map user from register model
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Map users list
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
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