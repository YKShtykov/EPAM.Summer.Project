using DAL.Interface;
using ORM;
using System.Collections.Generic;

namespace DAL.Mappers
{
    /// <summary>
    /// Service class for mapping DalSkill and ORM Skill entities
    /// </summary>
    public static class SkillMapper
    {
        /// <summary>
        /// Map Skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>new ORM Skill same as skill</returns>
        public static Skill Map(DalSkill skill)
        {
            return new Skill()
            {
                Id = skill.Id,
                Name = skill.Name,
            };
        }

        /// <summary>
        /// Map Skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>new DalSkill same as skill</returns>
        public static DalSkill Map(Skill skill)
        {
            return new DalSkill()
            {
                Id = skill.Id,
                Name = skill.Name,
                CategoryName=skill.Category.Name
            };
        }

        public static IEnumerable<DalSkill> Map(IEnumerable<Skill> skills)
        {
            var skillList = new List<DalSkill>();
            foreach (var item in skills)
            {
                skillList.Add(Map(item));
            }

            return skillList;
        }
    }
}
