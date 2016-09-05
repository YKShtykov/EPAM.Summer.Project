using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;

namespace MvcApp.Infrastructure.Mappers
{
    public static  class SkillMapper
    {
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

        public static IEnumerable<SkillsModel> Map(IEnumerable<BllUserSkills> skills)
        {
            var result = new List<SkillsModel>();
            foreach (var item in skills)
            {
                result.Add(Map(item));
            }

            return result;
        }

        public static IEnumerable<MvcSkill> Map(Dictionary<BllSkill, int> skills)
        {
            var result = new List<MvcSkill>();
            foreach (var item in skills)
            {
                var skill = Map(item.Key);
                skill.Level = item.Value;
                result.Add(skill);
            }

            return result;
        }
    }
}