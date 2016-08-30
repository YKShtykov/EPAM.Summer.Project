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

        public ActionResult Users(int page = 1)
        {
            var MvcUserList = new List<MvcUser>();

            foreach (var item in users.GetAll())
            {
                MvcUserList.Add(UserMapper.Map(item));
            }

            var viewModel = new GenericPaginationModel<MvcUser>(page, 2, MvcUserList);

            ViewBag.Roles = new string[] { "Administrator", "Manager", "User" };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Users(List<MvcUser> Entities, int page)
        {
            foreach (var item in Entities)
            {
                try
                {                    
                    users.Update(UserMapper.Map(item));
                    Logger.LogInfo("User: Id=" + item.Id + "Name=" + item.Login + "was changed");
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }

            return Redirect("~/Administrator/Users/?page=" + page);
        }

        public ActionResult RemoveUser(int Id)
        {
            try
            {
                users.Delete(Id);
                Logger.LogInfo("User: Id=" + Id +  "was removed");
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return Redirect("~/Administrator/Users");
        }

        public ActionResult UserDetails(int id)
        {
            return Json(profiles.Get(id), JsonRequestBehavior.AllowGet);
        }
    }
}
