using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using MvcApp.Infrastructure;

namespace MvcApp.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdministratorController : Controller
    {
        private readonly IUserService users;

        public AdministratorController(IUserService userService)
        {
            users = userService;
        }

        [HttpGet]
        [Route("Users")]
        public ActionResult Users(int page = 1)
        {
            var mvcUsers = UserMapper.Map(users.GetAll());

            var viewModel = new GenericPaginationModel<MvcUser>(page, 2, mvcUsers);

            ViewBag.Roles = new string[] { "Administrator", "Manager", "User" };
            return View(viewModel);
        }

        [HttpPost]
        [Route("Users", Name = "Users")]
        public ActionResult Users(List<MvcUser> Entities, int page=1)
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

            return Redirect("~/Users/?page=" + page);
        }

        [Route("RemoveUser", Name ="RemoveUser")]
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

            return Redirect("~/Users");
        }       
    }
}
