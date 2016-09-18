using System.Collections.Generic;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;

namespace MvcApp.Controllers
{
    /// <summary>
    /// Class for administrator logic in app. It consists users redactor methods
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private readonly IUserService users;

        /// <summary>
        /// Create administrator controller
        /// </summary>
        /// <param name="userService"></param>
        public AdministratorController(IUserService userService)
        {
            users = userService;
        }

        /// <summary>
        /// Returns user redactor page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Users")]
        public ActionResult Users(int page = 1)
        {
            var mvcUsers = UserMapper.Map(users.GetAll());
            var viewModel = new GenericPaginationModel<MvcUser>(page, 2, mvcUsers);

            ViewBag.Roles = new string[] { "Administrator", "Manager", "User" };
            return View(viewModel);
        }

        /// <summary>
        /// Logic for updating users
        /// </summary>
        /// <param name="Entities"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Users", Name = "Users")]
        public ActionResult Users(List<MvcUser> Entities, int page = 1)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in Entities)
                {
                    users.Update(UserMapper.Map(item));
                }
            }

            return Redirect("~/Users/?page=" + page);

        }

        /// <summary>
        /// Logic for removing users
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("RemoveUser", Name = "RemoveUser")]
        public ActionResult RemoveUser(int Id)
        {
            users.Delete(Id);

            return Redirect("~/Users");
        }
    }
}
