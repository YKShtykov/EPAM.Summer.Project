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
    /// <summary>
    /// Controller for users managment
    /// </summary>
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IUserService users;
        private readonly ISkillService skillService;


        /// <summary>
        /// Create manager controller
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="skillService"></param>
        public ManagerController(IUserService userService, ISkillService skillService)
        {
            users = userService;
            this.skillService = skillService;
        }

        /// <summary>
        /// Consists logic for users selection
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("Manage", Name = "Manage")]
        public ActionResult Index(IList<string> selector = null, int page = 1)
        {
            var userList = SkillMapper.Map(skillService.RateUsers(selector)).ToList();
            var viewModel = new GenericPaginationModel<SkillsModel>(page, 1, userList);

            ViewBag.Skills = (userList.First().Skills.Select(s => s.Name)).ToArray();
            ViewBag.AllSkills = (skillService.GetAll().Select(s => s.Name)).ToArray();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Users",viewModel);
            }
            else
            {
                return View(viewModel);
            }
        }

        /// <summary>
        /// Creates pdf with users list and their skills information
        /// </summary>
        /// <param name="Skills"></param>
        /// <returns></returns>
        public ActionResult UserListPdf(IList<string> Skills)
        {
            var userList = SkillMapper.Map(skillService.RateUsers(Skills)).Take(20).ToList();
            var memoryStream = PdfManager.CreateUserListPdf(userList);

            return new FileStreamResult(memoryStream, "application/pdf");
        }
    }
}