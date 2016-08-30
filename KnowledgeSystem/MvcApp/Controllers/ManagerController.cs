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
            GenericPaginationModel<SkillsModel> viewModel=null;
            try
            {
                var userList = SkillMapper.Map(skillService.RateUsers(selector)).ToList();
                viewModel = new GenericPaginationModel<SkillsModel>(page, 2, userList);

                ViewBag.Skills = (userList.First().Skills.Select(s => s.Name)).ToArray();
                ViewBag.AllSkills = (skillService.GetAll().Select(s => s.Name)).ToArray();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            
            return View(viewModel);
        }
    }
}