using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public void Create(DalSkill skill)
        {
            var ormSkill = SkillMapper.Map(skill);
            context.Set<Skill>().Add(ormSkill);

            context.Set<Category>().FirstOrDefault(c => c.Name == skill.CategoryName).Skills.Add(ormSkill);
        }

        public void Update(DalSkill skill)
        {
            var ormSkill = context.Set<Skill>().FirstOrDefault(s => s.Id == skill.Id);
            if (ormSkill != null)
            {
                ormSkill.Name = skill.Name;
                var category = context.Set<Category>().FirstOrDefault(c => c.Name == skill.CategoryName);
                ormSkill.Category = category;

                context.Entry(ormSkill).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var skill = context.Set<Skill>().FirstOrDefault(s => s.Id == id);
            context.Set<Skill>().Remove(skill);
        }

        public DalSkill Get(int id)
        {
            return SkillMapper.Map(context.Set<Skill>().Select(s => s).Include(s => s.Category).FirstOrDefault(u => u.Id == id));
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

        public DalSkill GetByPredicate(Expression<Func<DalSkill, bool>> f)
        {
            var expr = ExpressionTransformer<DalSkill, Skill>.Tranform(f);
            var func = expr.Compile();

            return SkillMapper.Map(context.Set<Skill>().FirstOrDefault(func));
        }

        public IEnumerable<DalUser> GetUsersWithThatSkill(DalSkill skill)
        {
            return UserMapper.Map(context.Set<UserSkill>().Where(us => us.Skill.Id == skill.Id).OrderBy(us => us.Level).Select(us => us.User));
        }

        public Dictionary<DalSkill, int> GetUserSkills(int userId)
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

        public void UpdateUserSkills(int userId, IDictionary<int, int> skillLevel)
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
        }

        public int GetLevelOfSkill(int userId, int skillId)
        {
            return context.Set<UserSkill>().FirstOrDefault(us => us.Skill.Id == skillId && us.User.Id == userId).Level;
        }
    }
}

