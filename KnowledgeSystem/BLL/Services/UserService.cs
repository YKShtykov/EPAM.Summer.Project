using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;
using System.Linq.Expressions;

namespace BLL
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;

        public UserService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Create(BllUser user)
        {
            DalUser dalUser = UserMapper.Map(user);
            if (IsEnabled(dalUser)) throw new Exception("User with such email is already exists");

            string passwordSalt = PasswordService.GenerateSalt();
            string encoded = PasswordService.GetHash(user.Password, passwordSalt);
            dalUser.Password = encoded;
            dalUser.PasswordSalt = passwordSalt;

            uow.Users.Create(dalUser);
            uow.Profiles.Create(dalUser.Id);
            uow.Commit();
        }

        public void Update(BllUser user)
        {
            uow.Users.Update(UserMapper.Map(user));
            uow.Commit();
        }

        public void Delete(int id)
        {
            uow.Profiles.Delete(id);
            uow.Users.Delete(id);
            uow.Commit();
        }

        public BllUser Get(int id)
        {
            return UserMapper.Map(uow.Users.Get(id));
        }

        public IEnumerable<BllUser> GetAll()
        {
            return UserMapper.Map(uow.Users.GetAll());
        }

        public Dictionary<BllSkill, int> GetUserSkills(int userId)
        {
            var result = new Dictionary<BllSkill, int>();
            foreach (var item in uow.Skills.GetUserSkills(userId))
            {
                result.Add(SkillMapper.Map(item.Key), item.Value);
            }

            return result;
        }

        public BllUser Login(string emailOrLogin, string password)
        {
            DalUser user;
            if (IsEnabled(emailOrLogin, out user))
                if (PasswordService.VerifyPassword(password, user.PasswordSalt, user.Password)) return UserMapper.Map(user);

            throw new Exception("check your data");
        }

        public void UpdateUserSkills(int userId, IDictionary<int, int> skillLevel)
        {
            uow.Skills.UpdateUserSkills(userId, skillLevel);
            uow.Commit();
        }

        private bool IsEnabled(DalUser user)
        {
            Expression<Func<DalUser, bool>> expLogin = u => u.Login == user.Login;
            Expression<Func<DalUser, bool>> expEmail = u => u.Email == user.Email;

            return ReferenceEquals(uow.Users.GetByPredicate(expLogin), null) && (ReferenceEquals(uow.Users.GetByPredicate(expEmail), null));
        }

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
