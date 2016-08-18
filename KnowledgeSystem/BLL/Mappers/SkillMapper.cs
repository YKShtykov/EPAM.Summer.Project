using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL.Interface;

namespace BLL.Mappers
{
    public static class SkillMapper
    {
        public static BllSkill Map(DalSkill skill)
        {
            return new BllSkill()
            {
                Id = skill.Id,
                Name = skill.Name,
                CategoryName = skill.CategoryName
            };
        }

        public static DalSkill Map(BllSkill skill)
        {
            return new DalSkill()
            {
                Id = skill.Id,
                Name = skill.Name,
                CategoryName = skill.CategoryName
            };
        }

        public static BllUserSkills Map(DalUserSkills userSkills)
        {
            var skills = new Dictionary<BllSkill, int>();
            var result= new BllUserSkills()
            {
                userId = userSkills.userId,
                UserLogin = userSkills.UserLogin,
            };
            foreach (var item in userSkills.SkillLevelPair)
            {
                skills.Add(Map(item.Key), item.Value);
            }
            result.SkillLevelPair = skills;

            return result;
        }
    }
}
