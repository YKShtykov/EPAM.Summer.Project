using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;

namespace MvcApp.ViewModels
{
    public class SkillsModel
    {
        public SkillsModel(List<MvcSkill> skills)
        {
            Skills = skills;      
        }

        public List<MvcSkill> Skills { get; set; }
    }
}