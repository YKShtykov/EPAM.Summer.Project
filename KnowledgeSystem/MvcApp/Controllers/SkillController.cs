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
            var identity = (CustomIdentity)User.Identity;
            SkillsModel userSkills = new SkillsModel(service.GetAllSkillLevels(identity.Id));

            return View(userSkills);
        }

        [HttpPost]
        public ActionResult Index(SkillsModel model)
        {
            var skillLevel = new Dictionary<int, int>();
            foreach (var item in model.Skills)
            {
                skillLevel.Add(item.Key.Id, item.Value);
            }
            var identity = (CustomIdentity)User.Identity;
            service.UpdateAllSkillLevels(identity.Id, skillLevel);

            return View();
        }
    }
}