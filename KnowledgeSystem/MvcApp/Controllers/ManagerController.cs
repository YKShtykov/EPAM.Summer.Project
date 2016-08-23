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
    [Authorize(Roles="Manager")]
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

        public ActionResult Index(IList<string> selector=null, int page =1)
        {
            var userList = SkillMapper.Map(skillService.RateUsers(selector)).ToList();

            var viewModel = new GenericPaginationModel<SkillsModel>(page, 2, userList);

            ViewBag.Skills = (userList.First().Skills.Select(s => s.Name)).ToArray();
            ViewBag.AllSkills = (skillService.GetAll().Select(s=>s.Name)).ToArray();
            
            return View(viewModel);
        }
    }
}