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

        //[Authorize(Roles ="User")]
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

            var viewModel = new GenericPaginationModel<MvcSkill>(page, 5, userSkills);            

            return View(viewModel);
        }

        [HttpPost]
        //[Authorize(Roles = "User")]
        public ActionResult Index(List<MvcSkill> Entities, int page=1)
        {
            try
            {
                var skillLevel = new Dictionary<int, int>();
                foreach (var item in Entities)
                {
                    skillLevel.Add(item.Id, item.Level);
                }
                var id = ((CustomIdentity)User.Identity).Id;
                service.UpdateUserSkills(id, skillLevel);
                Logger.LogInfo("Skills of user(id=" + id + ") was changed");
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }

            return Redirect("~/Skill/index/?page="+page);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Skills(string SearchString, string AdditionalSearchString, int page = 1)
        {
            IEnumerable<BllSkill> allSkills = skills.GetAll();
            List<MvcSkill> mvcSkills = allSkills.Select(s => SkillMapper.Map(s)).ToList();
            if (!ReferenceEquals(SearchString,null)&&SearchString!="")
            {
                mvcSkills = mvcSkills.Where(s => s.Name.Contains(SearchString)).ToList();
            }
            if (!ReferenceEquals(AdditionalSearchString, null)&&AdditionalSearchString!="")
            {
                mvcSkills = mvcSkills.Where(s => s.CategoryName == AdditionalSearchString).ToList();
            }

            var viewModel = new GenericPaginationModel<MvcSkill>(page, 2, mvcSkills);
            var categoryNames = new List<string>();
            foreach (var item in categories.GetAll())
            {
                categoryNames.Add(item.Name);
            }
            ViewBag.Categories = categoryNames;
            ViewBag.SearchString = SearchString;
            ViewBag.AdditionalSearchString = AdditionalSearchString;

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

        [Authorize(Roles = "Administrator")]
        public ActionResult Categories(string SearchString, int page = 1)
        {
            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<MvcCategory> mvcCategories = allCategories.Select(c => CategoryMapper.Map(c)).ToList();

            if (!ReferenceEquals(SearchString, null) && SearchString != "")
            {
                mvcCategories = mvcCategories.Where(s => s.Name.Contains(SearchString)).ToList();
            }
            ViewBag.SearchString = SearchString;
            var viewModel = new GenericPaginationModel<MvcCategory>(page, 2, mvcCategories);

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateCategory(MvcCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categories.Create(CategoryMapper.Map(category));
                    return Redirect("~/Skill/Categories");
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
        public ActionResult EditCategory(int id)
        {
            MvcCategory category = CategoryMapper.Map(categories.Get(id));
            return View(category);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditCategory(MvcCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categories.Update(CategoryMapper.Map(category));
                    return Redirect("~/Skill/Categories");
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
        public ActionResult RemoveCategory(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categories.Delete(id);
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }
            return Redirect("~/Skill/Categories");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult UserSkills(int id)
        {
            var userSkills = SkillMapper.Map(service.GetUserSkills(id));

            return View(userSkills);
        }
    }
}