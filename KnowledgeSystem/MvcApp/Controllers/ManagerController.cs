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
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IUserService users;
        private readonly ISkillService skillService;


        public ManagerController(IUserService userService, ISkillService skillService)
        {
            users = userService;
            this.skillService = skillService;
        }

        [Route("Manage", Name = "Manage")]
        public ActionResult Index(IList<string> selector = null, int page = 1)
        {
            var userList = SkillMapper.Map(skillService.RateUsers(selector)).ToList();
            var viewModel = new GenericPaginationModel<SkillsModel>(page, 5, userList);

            ViewBag.Skills = (userList.First().Skills.Select(s => s.Name)).ToArray();
            ViewBag.AllSkills = (skillService.GetAll().Select(s => s.Name)).ToArray();

            return View(viewModel);
        }

        public ActionResult UserListPdf(IList<string> Skills)
        {
            var userList = SkillMapper.Map(skillService.RateUsers(Skills)).Take(20).ToList();
            var memoryStream = PdfManager.CreateUserListPdf(userList);

            return new FileStreamResult(memoryStream, "application/pdf");
        }
    }
}