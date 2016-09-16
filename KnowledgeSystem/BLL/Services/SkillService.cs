using System.Collections.Generic;
using System.Linq;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;

namespace BLL
{
    /// <summary>
    /// Service for fork with Skills
    /// </summary>
    public class SkillService : ISkillService
    {
        private readonly IUnitOfWork uow;

        /// <summary>
        /// Create SkillService instance
        /// </summary>
        /// <param name="uow"></param>
        public SkillService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// The method for creating new skill
        /// </summary>
        /// <param name="skill"></param>
        public void Create(BllSkill skill)
        {
            uow.Skills.Create(SkillMapper.Map(skill));
            uow.Commit();
        }

        /// <summary>
        /// The method for updating existing skill
        /// </summary>
        /// <param name="skill"></param>
        public void Update(BllSkill skill)
        {
            uow.Skills.Update(SkillMapper.Map(skill));
            uow.Commit();
        }

        /// <summary>
        /// The method for deleting skill
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            uow.Skills.Delete(id);
            uow.Commit();
        }

        /// <summary>
        /// The method for getting skill by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BllSkill Get(int id)
        {
            return SkillMapper.Map(uow.Skills.Get(id));
        }

        /// <summary>
        /// The method for getting all skills
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BllSkill> GetAll()
        {
            return uow.Skills.GetAll().Select(u => SkillMapper.Map(u));
        }

        /// <summary>
        /// The method for construct rating of users
        /// </summary>
        /// <param name="sortings"></param>
        /// <returns></returns>
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
                 var userProfile = uow.Profiles.Get(user.Id);
                userSkills.userId = user.Id;
                userSkills.FirstName = userProfile.FirstName;
                userSkills.LastName = userProfile.LastName;
                userSkills.Photo = userProfile.Image;

                foreach (var skill in skills)
                {
                    var bllSkill = SkillMapper.Map(skill);
                    bllSkill.Level = uow.Skills.GetLevelOfSkill(user.Id, skill.Id);
                    userSkills.Skills.Add(bllSkill);
                }
                result.Add(userSkills);
            }

            return SortBySkill(result);
        }

        /// <summary>
        /// User EqualityComparer
        /// </summary>
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

        /// <summary>
        /// The method for sorting users by skills
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        internal List<BllUserSkills> SortBySkill(List<BllUserSkills> users)
        {
            if (ReferenceEquals(users, null))
                return null;
            int count = users.First().Skills.Count;
            if(count ==1)return users.OrderByDescending(u => u.Skills.First().Level).ToList();

            if (count == 2) return users.OrderByDescending(u => u.Skills.First().Level).
                       ThenByDescending(u => u.Skills.Skip(1).First().Level).ToList();

            if (count == 3) return users.OrderByDescending(u => u.Skills.First().Level).
                       ThenByDescending(u => u.Skills.Skip(1).First().Level).
                       ThenByDescending(u => u.Skills.Skip(2).First().Level).ToList();

            if (count == 4) return users.OrderByDescending(u => u.Skills.First().Level).
                       ThenByDescending(u => u.Skills.Skip(1).First().Level).
                       ThenByDescending(u => u.Skills.Skip(2).First().Level).
                       ThenByDescending(u => u.Skills.Skip(3).First().Level).ToList();

            if (count == 5) return users.OrderByDescending(u => u.Skills.First().Level).
                       ThenByDescending(u => u.Skills.Skip(1).First().Level).
                       ThenByDescending(u => u.Skills.Skip(2).First().Level).
                       ThenByDescending(u => u.Skills.Skip(3).First().Level).
                       ThenByDescending(u => u.Skills.Skip(4).First().Level).ToList();

            return users;
        }

        /// <summary>
        /// the method for getting skills by names
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
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
