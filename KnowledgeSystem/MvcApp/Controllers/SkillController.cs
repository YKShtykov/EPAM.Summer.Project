using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using MvcApp.Infrastructure;

namespace MvcApp.Controllers
{
    [Authorize]
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


        public ActionResult Index(int page =1)
        {

            var id = ((CustomIdentity)User.Identity).Id;
            var userSkills = CategoryMapper.Map(service.GetSortedUserSkills(id,true));

            var viewModel = new GenericPaginationModel<MvcCategory>(page, 5, userSkills);            

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(List<MvcCategory> Entities, int page=1)
        {
            try
            {
                int id = ((CustomIdentity)User.Identity).Id;
                service.UpdateUserSkills(id, CategoryMapper.Map(Entities));
                Logger.LogInfo("Skills of user(id=" + id + ") was changed");
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }

            return Redirect("~/Skill/index/?page="+page);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Skills(string FindSkill, string FindCategory, int page = 1)
        {
            var mvcCategories = FindSkills(FindSkill, FindCategory);
            var viewModel = new GenericPaginationModel<MvcCategory>(page, 2, mvcCategories);
            var categoryNames = new List<string>();

            foreach (var item in categories.GetAll())
            {
                categoryNames.Add(item.Name);
            }
            ViewBag.Categories = categoryNames;
            ViewBag.FindSkill = FindSkill;
            ViewBag.FindCategory = FindCategory;

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateSkill(string category = "")
        {
            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<string> mvcCategories = allCategories.Select(c => c.Name).ToList();
            ViewBag.Categories = mvcCategories;
            ViewBag.Category = category;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateSkill(MvcSkill skill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    skills.Create(SkillMapper.Map(skill));
                    return Redirect("~/Skill/Skills");
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditSkill(int id)
        {
            MvcSkill skill = SkillMapper.Map(skills.Get(id));

            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<string> mvcCategories = allCategories.Select(c => c.Name).ToList();
            ViewBag.Categories = mvcCategories;

            return View(skill);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditSkill(MvcSkill skill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    skills.Update(SkillMapper.Map(skill));
                    return Redirect("~/Skill/Skills");
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveSkill(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    skills.Delete(id);
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }
            return Redirect("~/Skill/Skills");
        }        

        private List<MvcCategory> FindSkills(string skill, string category)
        {
            var mvcCategories = CategoryMapper.Map(categories.GetAll());

            if (!ReferenceEquals(category, null) && category != "")
            {
                mvcCategories = mvcCategories.Where(s => s.Name == category).ToList();
            }

            if (!ReferenceEquals(skill, null) && skill != "")
            {
                foreach (var item in mvcCategories)
                {
                    item.Skills = item.Skills.Where(s => s.Name.Contains(skill)).ToList();
                }
            }

            return mvcCategories.Where(c => c.Skills.Count > 0).ToList();
        }
    }
}