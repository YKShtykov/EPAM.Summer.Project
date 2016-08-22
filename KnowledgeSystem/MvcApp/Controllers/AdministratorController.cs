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
    public class AdministratorController : Controller
    {
        private readonly IUserService users;
        private readonly ISkillService skills;
        private readonly ICategoryService categories;
        private readonly IProfileService profiles;


        public AdministratorController(IUserService userService,
                                       ISkillService skillService,
                                       ICategoryService categoryService,
                                       IProfileService profileService)
        {
            users = userService;
            skills = skillService;
            categories = categoryService;
            profiles = profileService;
        }

        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Categories()
        {
            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<MvcCategory> mvcCategories = allCategories.Select(c => CategoryMapper.Map(c)).ToList();
            return View(mvcCategories);
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
                return Redirect("~/Administrator/Categories");
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
                return Redirect("~/Administrator/Categories");
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
            return Redirect("~/Administrator/Categories");
        }

        public ActionResult Skills()
        {
            IEnumerable<BllSkill> allSkills = skills.GetAll();
            List<MvcSkill> mvcskills = allSkills.Select(s => SkillMapper.Map(s)).ToList();
            return View(mvcskills);
        }

        [HttpGet]
        public ActionResult CreateSkill()
        {
            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<string> mvcCategories = allCategories.Select(c => c.Name).ToList();
            ViewBag.Categories = mvcCategories;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSkill(MvcSkill skill)
        {
            if (ModelState.IsValid)
            {
                skills.Create(SkillMapper.Map(skill));
                return Redirect("~/Administrator/Skills");
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
                return Redirect("~/Administrator/Skills");
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
            return Redirect("~/Administrator/Skills");
        }


        public ActionResult Users(int page=1)
        {
            var MvcUserList = new List<MvcUser>();

            foreach (var item in users.GetAllBllUsers())
            {
                MvcUserList.Add(UserMapper.MapUser(item));
            }

            int pageSize = 1;
            List<MvcUser> usersPerPages = MvcUserList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pagination pageInfo = new Pagination { PageNumber = page, PageSize = pageSize, TotalItems = MvcUserList.Count };
            var viewModel = new MvcUsersPaging() { Pagination = pageInfo, Users = usersPerPages };

            ViewBag.Roles = new string[]{ "Administrator", "Manager", "User"};
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UpdateUsers(MvcUsersPaging userCollection)
        {
            foreach (var item in userCollection.Users)
            {
                users.UpdateUser(UserMapper.MapUser(item));
            }

            return Redirect("~/Administrator/Users");
        }

        public ActionResult RemoveUser(int Id)
        {
            users.DeleteUser(Id);
            return Redirect("~/Administrator/Users");
        }

        public ActionResult UserDetails(int id)
        {
            return Json(profiles.GetProfile(id), JsonRequestBehavior.AllowGet);
        }
    }
}
