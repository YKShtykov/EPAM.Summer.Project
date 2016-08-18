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
            foreach (Skill item in context.Set<Skill>().Select(s => s).Include(s => s.Category))
            {
                result.Add(SkillMapper.Map(item));
            }

            return result;
        }

        public DalSkill GetById(int key)
        {
            return SkillMapper.Map(context.Set<Skill>().Select(s => s).Include(s => s.Category).FirstOrDefault(u => u.Id == key));
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

        public IEnumerable<DalUserSkills> RateUsers(IEnumerable<string> sortings)
        {
            var skills = new List<Skill>();
            var usersRating = new List<DalUserSkills>();
            var ormSkills = context.Set<Skill>().Select(s => s).Include(s => s.Category);
            foreach (var item in sortings)
            {
                skills.Add(ormSkills.FirstOrDefault(s => s.Name == item));
            }

            skills.RemoveAll(item => item == null);
            var users = GetUsersWithThatSkill(skills.First());
            foreach (var item in skills)
            {
                users = users.Intersect(GetUsersWithThatSkill(item));
            }
            foreach (var user in users)
            {
                var userSkills = new DalUserSkills();
                userSkills.userId = user.Id;
                userSkills.UserLogin = user.Login;

                foreach (var skill in skills)
                {
                    int level = context.Set<UserSkill>().FirstOrDefault(us => us.Skill.Id == skill.Id && us.User.Id == user.Id).Level;
                    userSkills.SkillLevelPair.Add(SkillMapper.Map(skill), level);
                }
                usersRating.Add(userSkills);
            }
            var skillsArray = skills.ToArray();
            
            return Sort(usersRating);
        }

        private IEnumerable<User> GetUsersWithThatSkill(Skill skill)
        {
            return context.Set<UserSkill>().Where(us => us.Skill.Id == skill.Id).OrderBy(us => us.Level).Select(us => us.User);
        }

        private IEnumerable<DalUserSkills> Sort(IEnumerable<DalUserSkills> usersRating)
        {
            var skills = new List<DalSkill>();
            foreach (var item in usersRating.First().SkillLevelPair)
            {
                skills.Add(item.Key);
            }
            skills.ToArray();
            var sortingRating = usersRating.ToArray();
            for(int i = 0; i < sortingRating.Length; i++)
            {
                for (int j = 1; j < sortingRating.Length; j++)
                {
                    int lhs = sortingRating[j-1].SkillLevelPair.FirstOrDefault(kvp => kvp.Key == skills[0]).Value;
                    int rhs = sortingRating[j].SkillLevelPair.FirstOrDefault(kvp => kvp.Key == skills[0]).Value;
                    if (lhs>rhs)
                    {
                        var temp = sortingRating[i];
                        sortingRating[i] = sortingRating[j];
                        sortingRating[j] = temp;
                    }
                }
            }
            return sortingRating.ToList();
        }
    }
}
