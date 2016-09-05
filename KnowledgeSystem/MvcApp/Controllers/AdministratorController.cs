using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using MvcApp.Infrastructure;

namespace MvcApp.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IUserService users;

        public AdministratorController(IUserService userService)
        {
            users = userService;
        }

        public ActionResult Users(int page = 1)
        {
            var mvcUsers = UserMapper.Map(users.GetAll());

            var viewModel = new GenericPaginationModel<MvcUser>(page, 2, mvcUsers);

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
    }
}
