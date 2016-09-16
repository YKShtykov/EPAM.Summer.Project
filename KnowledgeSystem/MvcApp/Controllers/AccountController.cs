using System;
using System.Web;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using MvcApp.Infrastructure;
using Newtonsoft.Json;
using System.Web.Security;

namespace MvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService service;

        public AccountController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("Registration")]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login", Name = "Login")]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserMapper.Map(service.Login(loginModel.EmailOrLogin, loginModel.Password));
                AddCookiesToResponce(user, loginModel.IsRemember);

                return RedirectToRoute("Home");
            }

            return View();
        }

        [HttpPost]
        [Route("Registration", Name = "Registration")]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterModel model)
        {
            var work = Request.IsAjaxRequest();
                
            if (ModelState.IsValid)
            {
                service.Create(UserMapper.Map(model));
                Logger.LogInfo("User (Login=" + model.Login + "was created");

                if (Request.IsAjaxRequest())
                    return Json(new { Result = "You have successfully registered" });

                return RedirectToRoute("Home");
            }
            string errorMessage =string.Empty;
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errorMessage += error.ErrorMessage + "       \n";
                }
            }
            if (Request.IsAjaxRequest())
                return Json(new { Result = errorMessage });
            return View();
        }

        [Route("Logout", Name = "Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToRoute("Home");
        }

        private void AddCookiesToResponce(MvcUser user, bool remember)
        {
            string userData = JsonConvert.SerializeObject(user);
            var ticket = new FormsAuthenticationTicket(1,
                                                       user.Id.ToString(),
                                                       DateTime.Now,
                                                       DateTime.Now.AddDays(1),
                                                       remember,
                                                       userData);
            var encoded = FormsAuthentication.Encrypt(ticket);
            var coockie = new HttpCookie(FormsAuthentication.FormsCookieName, encoded);
            if (ticket.IsPersistent)
                coockie.Expires = ticket.Expiration;
            Response.Cookies.Add(coockie);
        }
    }
}