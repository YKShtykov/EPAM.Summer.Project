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
    [Authorize(Roles="Administrator")]
    public class SkillController : Controller
    {
        private readonly IUserService service;
        private readonly ISkillService skills;
        private readonly ICategoryService categories;

        public SkillController(IUserService service, ISkillService skillService, ICategoryService categories)
        {
            this.service = service;
            this.skills = skillService;
            this.categories = categories;
        }

        [Authorize(Roles ="User")]
        public ActionResult Index(int page =1)
        {
            var userSkills = new List<MvcSkill>();
            var id = ((CustomIdentity)User.Identity).Id;
            foreach (var item in service.GetUserSkills(id))
            {
                var skill = SkillMapper.Map(item.Key);
                skill.Level = item.Value;
                userSkills.Add(skill);
            }

            var viewModel = new GenericPaginationModel<MvcSkill>(page, 1, userSkills);            

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Index(List<MvcSkill> Entities, int page=1)
        {
            var skillLevel = new Dictionary<int, int>();
            foreach (var item in Entities)
            {
                skillLevel.Add(item.Id, item.Level);
            }
            var id = ((CustomIdentity)User.Identity).Id;
            service.UpdateUserSkills(id, skillLevel);

            return Redirect("~/Skill/index/?page="+page);
        }

        public ActionResult Skills(int page = 1)
        {
            IEnumerable<BllSkill> allSkills = skills.GetAll();
            List<MvcSkill> mvcSkills = allSkills.Select(s => SkillMapper.Map(s)).ToList();

            var viewModel = new GenericPaginationModel<MvcSkill>(page, 2, mvcSkills);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateSkill(string category = "")
        {
            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<string> mvcCategories = allCategories.Select(c => c.Name).ToList();
            ViewBag.Categories = mvcCategories;
            ViewBag.Category = category;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSkill(MvcSkill skill)
        {
            if (ModelState.IsValid)
            {
                skills.Create(SkillMapper.Map(skill));
                return Redirect("~/Skill/Skills");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditSkill(int id)
        {
            MvcSkill skill = SkillMapper.Map(skills.GetById(id));

            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<string> mvcCategories = allCategories.Select(c => c.Name).ToList();
            ViewBag.Categories = mvcCategories;

            return View(skill);
        }

        [HttpPost]
        public ActionResult EditSkill(MvcSkill skill)
        {
            if (ModelState.IsValid)
            {
                skills.Update(SkillMapper.Map(skill));
                return Redirect("~/Skill/Skills");
            }
            return View();
        }

        [HttpGet]
        public ActionResult RemoveSkill(int id)
        {
            if (ModelState.IsValid)
            {
                skills.Delete(id);
            }
            return Redirect("~/Skill/Skills");
        }

        public ActionResult Categories(int page = 1)
        {
            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<MvcCategory> mvcCategories = allCategories.Select(c => CategoryMapper.Map(c)).ToList();

            var viewModel = new GenericPaginationModel<MvcCategory>(page, 2, mvcCategories);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(MvcCategory category)
        {
            if (ModelState.IsValid)
            {
                categories.Create(CategoryMapper.Map(category));
                return Redirect("~/Skill/Categories");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            MvcCategory category = CategoryMapper.Map(categories.GetById(id));
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(MvcCategory category)
        {
            if (ModelState.IsValid)
            {
                categories.Update(CategoryMapper.Map(category));
                return Redirect("~/Skill/Categories");
            }
            return View();
        }

        [HttpGet]
        public ActionResult RemoveCategory(int id)
        {
            if (ModelState.IsValid)
            {
                categories.Delete(id);
            }
            return Redirect("~/Skill/Categories");
        }
    }
}