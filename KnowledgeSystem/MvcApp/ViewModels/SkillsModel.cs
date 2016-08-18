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
        public SkillsModel()
        {
            Skills = new List<MvcSkill>();      
        }
        public int UserId { get; set; }
        public string UserLogin { get; set; }

        public List<MvcSkill> Skills { get; set; }
    }
}