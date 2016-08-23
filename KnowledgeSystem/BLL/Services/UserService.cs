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
        private readonly IUserRepository userRepository;
        private readonly ISkillRepository skillRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository, ISkillRepository skillRepository)
        {
            this.uow = uow;
            this.userRepository = repository;
            this.skillRepository = skillRepository;
        }

        public void Create(BllUser user)
        {
            DalUser dalUser = UserMapper.Map(user);
            //if (IsEnabled(dalUser)) throw new Exception("User with such email is already exists");

            string passwordSalt = PasswordService.GenerateSalt();
            string encoded = PasswordService.GetHash(user.Password, passwordSalt);
            dalUser.Password = encoded;
            dalUser.PasswordSalt = passwordSalt;

            userRepository.Create(dalUser);
        }

        public void Update(BllUser user)
        {
            userRepository.Update(UserMapper.Map(user));
        }

        public void Delete(int id)
        {
            userRepository.Delete(id);
            uow.Commit();
        }

        public BllUser Get(int id)
        {
            return UserMapper.Map(userRepository.Get(id));
        }

        public IEnumerable<BllUser> GetAll()
        {
            return UserMapper.Map(userRepository.GetAll());
        }

        public Dictionary<BllSkill, int> GetUserSkills(int userId)
        {
            var result = new Dictionary<BllSkill, int>();
            foreach (var item in skillRepository.GetUserSkillLevels(userId))
            {
                result.Add(SkillMapper.Map(item.Key), item.Value);
            }

            return result;
        }

        //public BllUser Login(string emailOrLogin, string password)
        //{
        //    DalUser user;
        //    if (IsEnabled(emailOrLogin, out user))
        //        if (PasswordService.VerifyPassword(password, user.PasswordSalt, user.Password)) return UserMapper.Map(user);

        //    throw new Exception("check your data");
        //}

        public BllUser Login(string emailOrLogin, string password)
        {
            var user = UserMapper.Map(userRepository.LoginUser(emailOrLogin));
            if (PasswordService.VerifyPassword(password, user.PasswordSalt, user.Password)) return user;
            else throw new Exception("check your data");

        }

        public void UpdateUserSkills(int userId, IDictionary<int, int> skillLevel)
        {
            skillRepository.UpdateUserSkillLevels(userId, skillLevel);
        }

        private bool IsEnabled(DalUser user)
        {
            Expression<Func<DalUser, bool>> expLogin = u => u.Login == user.Login;
            Expression<Func<DalUser, bool>> expEmail = u => u.Email == user.Email;

            return ReferenceEquals(userRepository.GetByPredicate(expLogin), null) && (ReferenceEquals(userRepository.GetByPredicate(expEmail), null));
        }

        private bool IsEnabled(string userData, out DalUser dalUser)
        {
            Expression<Func<DalUser, bool>> expLogin = u => u.Login == userData;
            Expression<Func<DalUser, bool>> expEmail = u => u.Email == userData;

            dalUser = (userRepository.GetByPredicate(expLogin));
            if (ReferenceEquals(dalUser, null)) dalUser = userRepository.GetByPredicate(expEmail);

            return ReferenceEquals(dalUser, null);
        }
    }
}
