using System;
using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;
using System.Linq.Expressions;

namespace BLL
{
    /// <summary>
    /// Service for fork with Users
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;

        /// <summary>
        /// Create UserService instance
        /// </summary>
        /// <param name="uow"></param>
        public UserService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// The method for creating new user
        /// </summary>
        /// <param name="user"></param>
        public void Create(BllUser user)
        {
            DalUser dalUser = UserMapper.Map(user);
            if (!IsEmailEnabled(dalUser.Email)) throw new Exception("User with such email is already exists");
            if (!IsLoginEnabled(dalUser.Login)) throw new Exception("User with such login is already exists");

            string passwordSalt = PasswordService.GenerateSalt();
            string encoded = PasswordService.GetHash(user.Password, passwordSalt);
            dalUser.Password = encoded;
            dalUser.PasswordSalt = passwordSalt;

            uow.Users.Create(dalUser);
            uow.Profiles.Create(dalUser.Id);
            uow.Commit();
        }

        /// <summary>
        /// The method for updating existing user
        /// </summary>
        /// <param name="user"></param>
        public void Update(BllUser user)
        {
            uow.Users.Update(UserMapper.Map(user));
            uow.Commit();
        }

        /// <summary>
        /// The method for deleting user
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            uow.Profiles.Delete(id);
            uow.Users.Delete(id);
            uow.Commit();
        }

        /// <summary>
        /// The method for getting user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BllUser Get(int id)
        {
            return UserMapper.Map(uow.Users.Get(id));
        }

        /// <summary>
        /// The method for getting all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BllUser> GetAll()
        {
            return UserMapper.Map(uow.Users.GetAll());
        }

        /// <summary>
        /// The method for getting user skills
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Dictionary<BllSkill, int> GetUserSkills(int userId)
        {
            var result = new Dictionary<BllSkill, int>();
            foreach (var item in uow.Skills.GetUserSkills(userId))
            {
                result.Add(SkillMapper.Map(item.Key), item.Value);
            }

            return result;
        }

        /// <summary>
        /// The method for user logining
        /// </summary>
        /// <param name="emailOrLogin"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public BllUser Login(string emailOrLogin, string password)
        {
            DalUser user;
            if (IsEnabled(emailOrLogin, out user))
                if (PasswordService.VerifyPassword(password, user.PasswordSalt, user.Password)) return UserMapper.Map(user);

            throw new Exception("check your data");
        }

        /// <summary>
        /// The method for updating users skills
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillLevel"></param>
        public void UpdateUserSkills(int userId, IDictionary<int, int> skillLevel)
        {
            uow.Skills.UpdateUserSkills(userId, skillLevel);
            uow.Commit();
        }

        /// <summary>
        /// The method for checking email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IsEmailEnabled(string email)
        {
            Expression<Func<DalUser, bool>> expEmail = u => u.Email == email;

            return (ReferenceEquals(uow.Users.GetByPredicate(expEmail), null));
        }

        /// <summary>
        /// The method for checking login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private bool IsLoginEnabled(string login)
        {
            Expression<Func<DalUser, bool>> expEmail = u => u.Login == login;

            return (ReferenceEquals(uow.Users.GetByPredicate(expEmail), null));
        }

        /// <summary>
        /// The method for checking if the user is enabled and out parameter for the User
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="dalUser"></param>
        /// <returns></returns>
        private bool IsEnabled(string userData, out DalUser dalUser)
        {
            Expression<Func<DalUser, bool>> expLogin = u => u.Login == userData;
            Expression<Func<DalUser, bool>> expEmail = u => u.Email == userData;

            dalUser = (uow.Users.GetByPredicate(expLogin));
            if (ReferenceEquals(dalUser, null)) dalUser = uow.Users.GetByPredicate(expEmail);

            return !ReferenceEquals(dalUser, null);
        }
    }
}
