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

        //public ActionResult Index(int page=1)
        //{
        //    var userList = new List<SkillsModel>();
        //    foreach (var item in skillService.RateUsers(null))
        //    {
        //        userList.Add(SkillMapper.Map(item));
        //    }

        //    int pageSize = 1;
        //    List<SkillsModel> usersPerPages = userList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        //    Pagination pageInfo = new Pagination { PageNumber = page, PageSize = pageSize, TotalItems = userList.Count };
        //    var viewModel = new SkillModelPaging() { Pagination = pageInfo, UsersSkills = usersPerPages };

        //    ViewBag.Skills = new string[] { skillService.GetAll().First().Name };
        //    ViewBag.AllSkills = (skillService.GetAll().Select(s => s.Name)).ToArray();

        //    return View(viewModel);
        //}


        //[HttpPost]
        public ActionResult Index(IList<string> selector=null, int page =1)
        {
            var userList = new List<SkillsModel>();
            foreach (var item in skillService.RateUsers(selector))
            {
                userList.Add(SkillMapper.Map(item));
            }

            int pageSize = 1;
            List<SkillsModel> usersPerPages = userList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pagination pageInfo = new Pagination { PageNumber = page, PageSize = pageSize, TotalItems = userList.Count };
            var viewModel = new SkillModelPaging() { Pagination = pageInfo, UsersSkills = usersPerPages };

            ViewBag.Skills = (userList.First().Skills.Select(s => s.Name)).ToArray();
            ViewBag.AllSkills = (skillService.GetAll().Select(s=>s.Name)).ToArray();
            
            return View(viewModel);
        }
    }
}