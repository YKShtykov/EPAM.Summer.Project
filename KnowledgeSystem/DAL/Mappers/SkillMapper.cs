using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using ORM;

namespace DAL.Mappers
{
    public static class SkillMapper
    {
        public static Skill Map(DalSkill skill)
        {
            return new Skill()
            {
                Id = skill.Id,
                Name = skill.Name,
            };
        }

        public static DalSkill Map(Skill skill)
        {
            return new DalSkill()
            {
                Id = skill.Id,
                Name = skill.Name,
                CategoryName=skill.Category.Name
            };
        }
    }
}
