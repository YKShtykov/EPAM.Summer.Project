using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Mappers;
using ORM;
using System.Data.Entity;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly KnowledgeSystemContext context;

        public UserRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

        public void Create(DalUser e)
        {
            var user = UserMapper.MapUser(e);

            foreach (var role in e.Roles)
            {
                var userRole = context.Set<Role>().FirstOrDefault(r => r.Name == role);
                user.Roles.Add(userRole);
            }

            context.Set<User>().Add(user);
            context.Set<Profile>().Add(new Profile() { Id = user.Id, BirthDate = default(DateTime).ToString() });
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.Id == id);
            context.Set<User>().Remove(user);
            context.SaveChanges();
        }

        public IEnumerable<DalUser> GetAll()
        {
            var users = context.Users.Select(u => u);
            var dalUsers = new List<DalUser>();
            foreach (var item in users)
            {
                dalUsers.Add(UserMapper.MapUser(item));
            }
            return dalUsers;
        }

        public DalUser GetById(int key)
        {
            return UserMapper.MapUser(context.Users.FirstOrDefault(u => u.Id == key));
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }

        public bool ConsistEmail(string key)
        {
            return !ReferenceEquals(context.Set<User>().FirstOrDefault(user => user.Email == key), null);
        }

        public bool ConsistLogin(string key)
        {
            return !ReferenceEquals(context.Set<User>().FirstOrDefault(user => user.Login == key), null);
        }

        public DalUser LoginUser(string emailOrLogin)
        {
            User user = context.Set<User>().FirstOrDefault(u => u.Email == emailOrLogin);

            if (user == null) user = context.Set<User>().FirstOrDefault(u => u.Login == emailOrLogin);
            if (user == null) throw new Exception("check your data");

            return UserMapper.MapUser(user);
        }

        public int GetSkillLevel(int userId, int skillId)
        {
            return context.Set<UserSkill>().First(us => us.UserId == userId && us.SkillId == skillId).Level;
        }

        public Dictionary<DalSkill, int> GetAllSkillLevels(int userId)
        {
            var skills = new Dictionary<DalSkill, int>();
            var categories = context.Set<Category>().Select(c => c).Include(c => c.Skills);
            foreach (var item in categories)
            {
                foreach (var skill in item.Skills)
                {
                    var entity = context.Set<UserSkill>().FirstOrDefault(us => us.User.Id == userId && us.Skill.Id == skill.Id);
                    int level = (!ReferenceEquals(entity, null)) ? entity.Level : 0;
                    skills.Add(SkillMapper.Map(skill), level);
                }
            }

            return skills;
        }

        public void UpdateSkillLevel(int userId, int skillId, int level)
        {
            var userSkill = context.Set<UserSkill>().FirstOrDefault(us => us.User.Id == userId && us.Skill.Id == skillId);
            if (!ReferenceEquals(userSkill, null))
            {
                userSkill.Level = level;
                context.Entry(userSkill).State = EntityState.Modified;
            }
            else
            {
                userSkill = new UserSkill()
                {
                    User = context.Set<User>().FirstOrDefault(u => u.Id == userId),
                    Skill = context.Set<Skill>().FirstOrDefault(s => s.Id == skillId),
                    Level = level
                };
                context.Set<UserSkill>().Add(userSkill);
            }            
            context.SaveChanges();
        }

        public void UpdateAllSkillLevels(int userId, IDictionary<int, int> skillLevel)
        {
            foreach (var item in skillLevel)
            {
                UpdateSkillLevel(userId, item.Key, item.Value);
            };
        }
    }
}
