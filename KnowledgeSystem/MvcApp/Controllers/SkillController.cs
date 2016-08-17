using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using MvcApp.Infrastructure;
using Newtonsoft.Json;
using System.Web.Security;
using System.Security.Principal;

namespace MvcApp.Controllers
{
    public class SkillController : Controller
    {
        private readonly IUserService service;
        private readonly ISkillService skillService;

        public SkillController(IUserService service, ISkillService skillService)
        {
            this.service = service;
            this.skillService = skillService;
        }

        // GET: Skill
        public ActionResult Index()
        {
            var userSkills = new List<MvcSkill>();
            var identity = (CustomIdentity)User.Identity;
            foreach (var item in service.GetAllSkillLevels(identity.Id))
            {
                var skill = SkillMapper.Map(item.Key);
                skill.Level = item.Value;
                userSkills.Add(skill);
            } 

            return View(userSkills);
        }

        [HttpPost]
        public ActionResult Index(List<MvcSkill> skillModel)
        {
            var skillLevel = new Dictionary<int, int>();
            foreach (var item in skillModel)
            {
                skillLevel.Add(item.Id, item.Level);
            }
            var identity = (CustomIdentity)User.Identity;
            service.UpdateAllSkillLevels(identity.Id, skillLevel);

            return Redirect("~/Skill/index");
        }
    }
}