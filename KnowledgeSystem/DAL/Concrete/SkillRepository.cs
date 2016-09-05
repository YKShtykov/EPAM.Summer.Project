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
    /// <summary>
    /// Skill repository class implements Repository pattern for skill collection
    /// </summary>
    public class SkillRepository : ISkillRepository
    {
        private readonly KnowledgeSystemContext context;

        /// <summary>
        /// Create SkillRepository instance
        /// </summary>
        /// <param name="knowledgeContext"></param>
        public SkillRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

        /// <summary>
        /// The method for creating new skill entity in collection
        /// </summary>
        /// <param name="skill"></param>
        public void Create(DalSkill skill)
        {
            var ormSkill = SkillMapper.Map(skill);
            context.Set<Skill>().Add(ormSkill);

            context.Set<Category>().FirstOrDefault(c => c.Name == skill.CategoryName).Skills.Add(ormSkill);
        }

        /// <summary>
        /// The method for updating exsisting skill in collection
        /// </summary>
        /// <param name="skill"></param>
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

        /// <summary>
        /// The method for deleting skill entity from collection
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var skill = context.Set<Skill>().FirstOrDefault(s => s.Id == id);
            context.Set<Skill>().Remove(skill);
        }

        /// <summary>
        /// The method for getting skill entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DalSkill Get(int id)
        {
            return SkillMapper.Map(context.Set<Skill>().Select(s => s).Include(s => s.Category).FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// The method for getting all skills
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DalSkill> GetAll()
        {
            List<DalSkill> result = new List<DalSkill>();
            foreach (Skill item in context.Set<Skill>().Include(s => s.Category).Select(s => s))
            {
                result.Add(SkillMapper.Map(item));
            }

            return result;
        }

        /// <summary>
        /// The method for getting skill entity by predicate
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public DalSkill GetByPredicate(Expression<Func<DalSkill, bool>> f)
        {
            var expr = ExpressionTransformer<DalSkill, Skill>.Tranform(f);
            var func = expr.Compile();

            return SkillMapper.Map(context.Set<Skill>().FirstOrDefault(func));
        }

        /// <summary>
        /// The method for getting all users with that skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns> DalUser collection </returns>
        public IEnumerable<DalUser> GetUsersWithThatSkill(DalSkill skill)
        {
            return UserMapper.Map(context.Set<UserSkill>().Where(us => us.Skill.Id == skill.Id).OrderBy(us => us.Level).Select(us => us.User));
        }

        /// <summary>
        /// The method for getting all user skills by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Dictionary DalSkill-level</returns>
        public List<DalSkill> GetUserSkills(int userId)
        {
            var skills = new List<DalSkill>();
            var categories = context.Set<Category>().Select(c => c).Include(c => c.Skills);
            foreach (var item in categories)
            {
                foreach (var skill in item.Skills)
                {
                    var userSkill = SkillMapper.Map(skill);
                    var entity = context.Set<UserSkill>().FirstOrDefault(us => us.User.Id == userId && us.Skill.Id == skill.Id);
                    userSkill.Level = (!ReferenceEquals(entity, null)) ? entity.Level : 0;
                    skills.Add(userSkill);
                }
            }

            return skills;
        }
        public List<DalCategory> GetSortedUserSkills(int userId)
        {
            var categories = CategoryMapper.Map(context.Set<Category>().Select(c => c).Include(c => c.Skills).ToList());

            foreach (var item in categories)
            {
                foreach (var skill in item.Skills)
                {
                    var entity = context.Set<UserSkill>().FirstOrDefault(us => us.User.Id == userId && us.Skill.Id == skill.Id);
                    skill.Level = (!ReferenceEquals(entity, null)) ? entity.Level : 0;
                }
            }

            return categories.ToList();
        }
        /// <summary>
        /// The method for updating all user skills
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillLevel"></param>
        public void UpdateUserSkills(int userId, IDictionary<int, int> skillLevel)
        {
            foreach (var item in skillLevel)
            {
                UpdateSkillLevel(userId, item.Key, item.Value);
            };
        }

        /// <summary>
        /// The method for updating level of skill
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillId"></param>
        /// <param name="level"></param>
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

        /// <summary>
        /// The method for getting user level in that skill by user Id and skill Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillId"></param>
        /// <returns>Level of skill</returns>
        public int GetLevelOfSkill(int userId, int skillId)
        {
            return context.Set<UserSkill>().FirstOrDefault(us => us.Skill.Id == skillId && us.User.Id == userId).Level;
        }
    }
}

