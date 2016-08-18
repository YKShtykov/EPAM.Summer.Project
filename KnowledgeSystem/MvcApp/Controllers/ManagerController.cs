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
    public class ManagerController : Controller
    {
        private readonly IUserService users;
        private readonly ISkillService skillService;
        private readonly ICategoryService categories;


        public ManagerController(IUserService userService,
                                       ISkillService skillService,
                                       ICategoryService categoryService)
        {
            users = userService;
            this.skillService = skillService;
            categories = categoryService;
        }

        // GET: Manager
        public ActionResult Index()
        {
            var userSkillsList = new List<SkillsModel>();

            foreach (var item in users.GetAllBllUsers())
            {
                var userSkills = new SkillsModel();
                userSkills.UserId = item.Id;
                userSkills.UserLogin = item.Login;
                var skills =new  List<MvcSkill>();

                foreach (var skillLevelPair in users.GetAllSkillLevels(item.Id))
                {
                    MvcSkill skill = SkillMapper.Map(skillLevelPair.Key);
                    skill.Level = skillLevelPair.Value;
                    skills.Add(skill);
                }

                userSkills.Skills = skills;
                userSkillsList.Add(userSkills);
            };

            var skillNames = new List<string>();
            foreach (var item in userSkillsList.First().Skills)
            {
                skillNames.Add(item.Name);
            }
            ViewBag.Skills = skillNames.ToArray();
            ViewBag.AllSkills = (skillService.GetAll().Select(s => s.Name)).ToArray();
            return View(userSkillsList);
        }


        [HttpPost]
        public ActionResult Index(IList<string> selector)
        {
            var usersRating = new List<SkillsModel>();

            foreach (var item in skillService.RateUsers(selector))
            {
                usersRating.Add(SkillMapper.Map(item));
            }
            ViewBag.Skills = (usersRating.First().Skills.Select(s => s.Name)).ToArray();
            ViewBag.AllSkills = (skillService.GetAll().Select(s=>s.Name)).ToArray();
            
            return View(usersRating);
        }
    }
}