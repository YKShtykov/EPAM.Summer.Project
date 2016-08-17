using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;

namespace BLL
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }

        public void CreateUser(BllUser user)
        {

            if (userRepository.ConsistEmail(user.Email)) throw new Exception("User with such email is already exists");
            if (userRepository.ConsistLogin(user.Login)) throw new Exception("User with such login is already exists");

            string passwordSalt = PasswordService.GenerateSalt();
            string encoded = PasswordService.GetHash(user.Password, passwordSalt);
            user.Password = encoded;
            user.PasswordSalt = passwordSalt;

            userRepository.Create(UserMapper.MapUser(user));
        }

        public void DeleteUser(int id)
        {
            try
            {
                userRepository.Delete(id);
                uow.Commit();
            }
            catch
            {
                throw;
            }
        }        

        public IEnumerable<BllUser> GetAllBllUsers()
        {
            try
            {
                return userRepository.GetAll().Select(u => UserMapper.MapUser(u));
            }
            catch
            {
                throw;
            }
        }

        public Dictionary<BllSkill, int> GetAllSkillLevels(int userId)
        {
            var result = new Dictionary<BllSkill, int>();
            foreach (var item in userRepository.GetAllSkillLevels(userId))
            {
                result.Add(SkillMapper.Map(item.Key), item.Value);
            }

            return result;
        }

        public BllUser GetBllUser(int id)
        {
            return UserMapper.MapUser(userRepository.GetById(id));
        }

        public int GetSkillLevel(int userId, int skillId)
        {
            return userRepository.GetSkillLevel(userId, skillId);
        }

        public BllUser LoginUser(string emailOrLogin, string password)
        {
            var user = UserMapper.MapUser(userRepository.LoginUser(emailOrLogin));
            if (PasswordService.VerifyPassword(password, user.PasswordSalt, user.Password)) return user;
            else throw new Exception("check your data");

            return null;
        }

        public void UpdateAllSkillLevels(int userId, IDictionary<int, int> skillLevel)
        {
            userRepository.UpdateAllSkillLevels(userId, skillLevel);
        }

        public void UpdateSkillLevel(int userId, int skillId, int level)
        {
            userRepository.UpdateSkillLevel(userId, skillId, level);
        }
    }
}
