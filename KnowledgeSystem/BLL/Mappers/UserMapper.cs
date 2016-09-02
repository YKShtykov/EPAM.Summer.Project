using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;

namespace BLL.Mappers
{
    /// <summary>
    /// Service class for mapping DalUser and BllUser entities
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        /// Map User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>new BllUser same as user</returns>
        public static BllUser Map(DalUser user)
        {
            if (ReferenceEquals(user, null)) return null;
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
        /// Map User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>new DalUser same as user</returns>
        public static DalUser Map(BllUser user)
        {
            if (ReferenceEquals(user, null)) return null;
            DalUser result = new DalUser
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
        /// Map Users
        /// </summary>
        /// <param name="users"></param>
        /// <returns>new BllUsers collection same as users</returns>
        public static IEnumerable<BllUser> Map(IEnumerable<DalUser> users)
        {
            var bllUsers = new List<BllUser>();
            foreach (var item in users)
            {
                bllUsers.Add(Map(item));
            }
            return bllUsers;
        }
    }
}
