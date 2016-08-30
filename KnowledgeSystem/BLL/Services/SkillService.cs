using System.Collections.Generic;
using System.Linq;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;

namespace BLL
{
    public class SkillService : ISkillService
    {
        private readonly IUnitOfWork uow;

        public SkillService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Create(BllSkill skill)
        {
            uow.Skills.Create(SkillMapper.Map(skill));
            uow.Commit();
        }

        public void Update(BllSkill skill)
        {
            uow.Skills.Update(SkillMapper.Map(skill));
            uow.Commit();
        }

        public void Delete(int id)
        {
            uow.Skills.Delete(id);
            uow.Commit();
        }

        public BllSkill Get(int id)
        {
            return SkillMapper.Map(uow.Skills.Get(id));
        }

        public IEnumerable<BllSkill> GetAll()
        {
            return uow.Skills.GetAll().Select(u => SkillMapper.Map(u));
        }        

        public IEnumerable<BllUserSkills> RateUsers(IEnumerable<string> sortings)
        {
            var result = new List<BllUserSkills>();
            var skills = GetSkills(sortings);

            var users = uow.Users.GetAll();
            foreach (var item in skills)
            {
                users = users.Intersect(uow.Skills.GetUsersWithThatSkill(item), new UserComparer());
            }
            
            foreach (var user in users)
            {
                var userSkills = new BllUserSkills();
                userSkills.userId = user.Id;
                userSkills.UserLogin = user.Login;

                foreach (var skill in skills)
                {
                    int level = uow.Skills.GetLevelOfSkill(user.Id, skill.Id);
                    userSkills.SkillLevelPair.Add(SkillMapper.Map(skill), level);
                }
                result.Add(userSkills);
            }

            return SortBySkill(result);
        }

        private class UserComparer : IEqualityComparer<DalUser>
        {
            public bool Equals(DalUser x, DalUser y)
            {
                return (x.Id == y.Id);
            }

            public int GetHashCode(DalUser obj)
            {
                return (obj.Id * 500) ^ (obj.Email.Length * 500);
            }
        }

        internal List<BllUserSkills> SortBySkill(List<BllUserSkills> users)
        {
            int count = users.First().SkillLevelPair.Count;
            if(count ==1)return users.OrderByDescending(u => u.SkillLevelPair.First().Value).ToList();

            if (count == 2) return users.OrderByDescending(u => u.SkillLevelPair.First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(1).First().Value).ToList();

            if (count == 3) return users.OrderByDescending(u => u.SkillLevelPair.First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(1).First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(2).First().Value).ToList();

            if (count == 4) return users.OrderByDescending(u => u.SkillLevelPair.First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(1).First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(2).First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(3).First().Value).ToList();

            if (count == 5) return users.OrderByDescending(u => u.SkillLevelPair.First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(1).First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(2).First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(3).First().Value).
                       ThenByDescending(u => u.SkillLevelPair.Skip(4).First().Value).ToList();

            return users;
        }

        internal IEnumerable<DalSkill> GetSkills(IEnumerable<string> names)
        {
            if (ReferenceEquals(names, null)) return new List<DalSkill>(){ uow.Skills.GetAll().First()};

            var result = new List<DalSkill>();
            foreach (var item in names)
            {
                result.Add(uow.Skills.GetAll().FirstOrDefault(s => s.Name == item));
            }
            result.RemoveAll(item => item == null);

            return result;
        }

    }
}
