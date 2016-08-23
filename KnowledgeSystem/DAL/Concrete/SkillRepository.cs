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
    public class SkillRepository : ISkillRepository
    {
        private readonly KnowledgeSystemContext context;

        public SkillRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

        public void Create(DalSkill e)
        {
            var skill = SkillMapper.Map(e);
            context.Set<Skill>().Add(skill);

            context.Set<Category>().FirstOrDefault(c => c.Name == e.CategoryName).Skills.Add(skill);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var skill = context.Set<Skill>().FirstOrDefault(s => s.Id == id);
            context.Set<Skill>().Remove(skill);
            context.SaveChanges();
        }

        public IEnumerable<DalSkill> GetAll()
        {
            List<DalSkill> result = new List<DalSkill>();
            foreach (Skill item in context.Set<Skill>().Include(s => s.Category).Select(s => s))
            {
                result.Add(SkillMapper.Map(item));
            }

            return result;
        }

        public DalSkill Get(int id)
        {
            return SkillMapper.Map(context.Set<Skill>().Select(s => s).Include(s => s.Category).FirstOrDefault(u => u.Id == id));
        }

        public void Update(DalSkill entity)
        {
            var ormSkill = context.Set<Skill>().FirstOrDefault(s => s.Id == entity.Id);
            if (ormSkill != null)
            {
                ormSkill.Name = entity.Name;
                var category = context.Set<Category>().FirstOrDefault(c => c.Name == entity.CategoryName);
                ormSkill.Category = category;

                context.Entry(ormSkill).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public DalSkill GetByPredicate(Expression<Func<DalSkill, bool>> f)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalUser> GetUsersWithThatSkill(DalSkill skill)
        {
            return UserMapper.Map(context.Set<UserSkill>().Where(us => us.Skill.Id == skill.Id).OrderBy(us => us.Level).Select(us => us.User));
        }

        public Dictionary<DalSkill, int> GetUserSkillLevels(int userId)
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

        public void UpdateUserSkillLevels(int userId, IDictionary<int, int> skillLevel)
        {
            foreach (var item in skillLevel)
            {
                UpdateSkillLevel(userId, item.Key, item.Value);
            };
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

        public int GetLevelOfSkill(int userId, int skillId)
        {
            return context.Set<UserSkill>().FirstOrDefault(us => us.Skill.Id == skillId && us.User.Id == userId).Level;
        }
    }
}

