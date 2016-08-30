using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;

namespace BLL.Mappers
{
    /// <summary>
    /// Service class for mapping BllSkill and DalSkill entities, BllUserSkills and DalUserSkills
    /// </summary>
    public static class SkillMapper
    {
        /// <summary>
        /// Map Skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>new BllSkill same as skill</returns>
        public static BllSkill Map(DalSkill skill)
        {
            return new BllSkill()
            {
                Id = skill.Id,
                Name = skill.Name,
                CategoryName = skill.CategoryName
            };
        }

        /// <summary>
        /// Map skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>new DalSkill same as skill</returns>
        public static DalSkill Map(BllSkill skill)
        {
            return new DalSkill()
            {
                Id = skill.Id,
                Name = skill.Name,
                CategoryName = skill.CategoryName
            };
        }

        /// <summary>
        /// Map Skills
        /// </summary>
        /// <param name="userSkills"></param>
        /// <returns>new BllUserSkills same as userSkills</returns>
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
