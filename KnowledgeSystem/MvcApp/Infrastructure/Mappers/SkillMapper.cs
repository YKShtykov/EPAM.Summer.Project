using System.Collections.Generic;
using MvcApp.ViewModels;
using BLL.Interface;

namespace MvcApp.Infrastructure.Mappers
{
    /// <summary>
    /// Class-mapper for skills
    /// </summary>
    public static  class SkillMapper
    {
        /// <summary>
        /// Map skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>Bll skill such as skill</returns>
        public static BllSkill Map(MvcSkill skill)
        {
            return new BllSkill()
            {
                Id = skill.Id,
                Name = skill.Name,
                Level = skill.Level,
                CategoryName = skill.CategoryName
            };
        }

        /// <summary>
        /// Map skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>Mvc skill such as skill</returns>
        public static MvcSkill Map(BllSkill skill)
        {
            return new MvcSkill()
            {
                Id = skill.Id,
                Name = skill.Name,
                Level = skill.Level,
                CategoryName = skill.CategoryName
            };
        }

        /// <summary>
        /// Map Skill model
        /// </summary>
        /// <param name="userSkills"></param>
        /// <returns>bll skill model such as model</returns>
        public static SkillsModel Map(BllUserSkills userSkills)
        {
            var mvcSkills = new List<MvcSkill>();
            var result = new SkillsModel()
            {
                UserId = userSkills.userId,
                FirstName = userSkills.FirstName,
                LastName = userSkills.LastName,
                Photo = userSkills.Photo
            };

            foreach (var item in userSkills.Skills)
            {
                result.Skills.Add(Map(item));
            }

            return result;
        }

        /// <summary>
        /// Map skill model list
        /// </summary>
        /// <param name="skills"></param>
        /// <returns></returns>
        public static IEnumerable<SkillsModel> Map(IEnumerable<BllUserSkills> skills)
        {
            var result = new List<SkillsModel>();
            foreach (var item in skills)
            {
                result.Add(Map(item));
            }

            return result;
        }
    }
}