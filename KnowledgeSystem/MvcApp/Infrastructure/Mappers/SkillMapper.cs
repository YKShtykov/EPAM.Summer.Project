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
                CategoryName = skill.CategoryName
            };
        }

        public static MvcSkill Map(BllSkill skill)
        {
            return new MvcSkill()
            {
                Id = skill.Id,
                Name = skill.Name,
                CategoryName = skill.CategoryName
            };
        }
    }
}