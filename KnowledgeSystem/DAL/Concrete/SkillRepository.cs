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
        private readonly DbSet<Category> categories;
        private readonly DbSet<Skill> skills;
        private readonly DbSet<UserSkill> userSkills;
        private readonly DbSet<User> users;
        /// <summary>
        /// Create SkillRepository instance
        /// </summary>
        /// <param name="knowledgeContext"></param>
        public SkillRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
            skills = context.Set<Skill>();
            users = context.Set<User>();
            categories = context.Set<Category>();
            userSkills = context.Set<UserSkill>();
        }

        /// <summary>
        /// The method for creating new skill entity in collection
        /// </summary>
        /// <param name="dalSkill"></param>
        public void Create(DalSkill dalSkill)
        {
            var skill = SkillMapper.Map(dalSkill);
            skills.Add(skill);

            categories.FirstOrDefault(c => c.Name == dalSkill.CategoryName).Skills.Add(skill);
        }

        /// <summary>
        /// The method for updating exsisting skill in collection
        /// </summary>
        /// <param name="dalSkill"></param>
        public void Update(DalSkill dalSkill)
        {
            var skill = skills.FirstOrDefault(s => s.Id == dalSkill.Id);
            if (skill != null)
            {
                skill.Name = dalSkill.Name;
                var category = categories.FirstOrDefault(c => c.Name == dalSkill.CategoryName);
                skill.Category = category;

                context.Entry(skill).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// The method for deleting skill entity from collection
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            skills.Remove(skills.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// The method for getting skill entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DalSkill Get(int id)
        {
            return SkillMapper.Map(skills.Select(s => s).Include(s => s.Category).FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// The method for getting all skills
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DalSkill> GetAll()
        {
            return SkillMapper.Map(skills.Include(s => s.Category).Select(s => s));
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

            return SkillMapper.Map(skills.FirstOrDefault(func));
        }

        /// <summary>
        /// The method for getting all users with that skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns> DalUser collection </returns>
        public IEnumerable<DalUser> GetUsersWithThatSkill(DalSkill skill)
        {
            return UserMapper.Map(userSkills.Where(us => us.Skill.Id == skill.Id).OrderBy(us => us.Level).Select(us => us.User));
        }

        /// <summary>
        /// The method for getting all user skills by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Dictionary DalSkill-level</returns>
        public List<DalCategory> GetUserSkills(int userId)
        {
            var skillsInCategories = CategoryMapper.Map( categories.Select(c => c).Include(c => c.Skills));
            foreach (var item in skillsInCategories)
            {
                foreach (var skill in item.Skills)
                {
                    var userSkill = userSkills.FirstOrDefault(us => us.User.Id == userId && us.Skill.Id == skill.Id);
                    skill.Level = ReferenceEquals(userSkill, null) ? 0 : userSkill.Level;
                }
            }

            return skillsInCategories.ToList();
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
            var userSkill = userSkills.FirstOrDefault(us => us.User.Id == userId && us.Skill.Id == skillId);
            if (!ReferenceEquals(userSkill, null))
            {
                userSkill.Level = level;
                context.Entry(userSkill).State = EntityState.Modified;
            }
            else
            {
                userSkill = new UserSkill()
                {
                    User = users.FirstOrDefault(u => u.Id == userId),
                    Skill = skills.FirstOrDefault(s => s.Id == skillId),
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
            return userSkills.FirstOrDefault(us => us.Skill.Id == skillId && us.User.Id == userId).Level;
        }
    }
}

