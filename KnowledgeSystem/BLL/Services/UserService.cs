using System;
using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;
using System.Linq.Expressions;
using CryptoService.Interface;

namespace BLL
{
    /// <summary>
    /// Service for fork with Users
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IPasswordService passwordService;


        /// <summary>
        /// Create UserService instance
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="passwordService"></param>
        public UserService(IUnitOfWork uow, IPasswordService passwordService)
        {
            this.uow = uow;
            this.passwordService = passwordService;
        }

        /// <summary>
        /// The method for creating new user
        /// </summary>
        /// <param name="user"></param>
        public void Create(BllUser user)
        {
            DalUser dalUser = UserMapper.Map(user);
            if (!IsEmailEnabled(dalUser.Email)) throw new AccountException("User with such email is already exists");
            if (!IsLoginEnabled(dalUser.Login)) throw new AccountException("User with such login is already exists");

            dalUser.PasswordSalt = passwordService.Key;
            dalUser.Password = passwordService.GetHash(user.Password, dalUser.PasswordSalt);

            uow.Users.Create(dalUser);
            uow.Profiles.Create(new DalProfile() { Id = user.Id });
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
        public IEnumerable<BllCategory> GetUserSkills(int userId)
        {
            return CategoryMapper.Map(uow.Skills.GetUserSkills(userId));
        }

        
        /// <summary>
        /// The method returns categories collection, each category consists user skill and its level
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillsWithNullLevel">Incude in collection user skills with null level</param>
        /// <returns></returns>
        public IEnumerable<BllCategory> GetSortedUserSkills(int userId, bool skillsWithNullLevel)
        {
            var result = uow.Skills.GetUserSkills(userId);
            result.RemoveAll(c => c.Skills.Count == 0);
            if (!skillsWithNullLevel)
            {
                foreach (var item in result)
                {
                    item.Skills.RemoveAll(s => s.Level == 0);
                }
                result.RemoveAll(c => c.Skills.Count==0);
            }

            return CategoryMapper.Map(result);
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
                if (passwordService.VerifyPassword(password, user.PasswordSalt, user.Password)) return UserMapper.Map(user);

            throw new AccountException("check your data");
        }

        /// <summary>
        /// The method for updating users skills
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="categories"></param>
        public void UpdateUserSkills(int userId, IEnumerable<BllCategory> categories)
        {
            foreach (var category in categories)
            {
                foreach (var skill in category.Skills)
                {
                    uow.Skills.UpdateSkillLevel(userId, skill.Id, skill.Level);
                }
            }            
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
