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
        //public SkillsModel()
        //{
        //}

        public SkillsModel(Dictionary<BllSkill, int> skills)
        {
            Skills = new Dictionary<MvcSkill, int>();
            foreach (var item in skills)
            {
                Skills.Add(SkillMapper.Map(item.Key), item.Value);
            }       
        }

        public Dictionary<MvcSkill, int> Skills { get; set; }
    }
}