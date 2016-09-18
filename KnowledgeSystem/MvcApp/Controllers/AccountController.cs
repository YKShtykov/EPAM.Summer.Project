using System;
using System.Web;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using Newtonsoft.Json;
using System.Web.Security;
using System.Linq;
using System.Collections.Generic;

namespace MvcApp.Controllers
{
    /// <summary>
    /// Class for account logic in app. It consists register, login and  logout methods
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUserService service;

        /// <summary>
        /// Create Account controller
        /// </summary>
        /// <param name="service"></param>
        public AccountController(IUserService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Returns registration page 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Registration")]
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Returns login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Consists loging logic
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login", Name = "Login")]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = UserMapper.Map(service.Login(loginModel.EmailOrLogin, loginModel.Password));
                    AddCookiesToResponce(user, loginModel.IsRemember);

                    return RedirectToRoute("User");
                }
                catch (AccountException e)
                {
                    ModelState.AddModelError(e.Message, e.Message);
                }                
            }

            string errorMessage = GetModelStateErrors(ModelState);

            if (Request.IsAjaxRequest())
                return Json(new { Result = errorMessage });

            return View();
        }

        /// <summary>
        /// Consists registration logic
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Registration", Name = "Registration")]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    service.Create(UserMapper.Map(model));

                    if (Request.IsAjaxRequest())
                        return Json(new { Result = "You have successfully registered" });

                    return RedirectToRoute("Home");
                }
                catch (AccountException e)
                {
                    ModelState.AddModelError(e.Message, e.Message);
                }
            }

            string errorMessage = GetModelStateErrors(ModelState);

            if (Request.IsAjaxRequest())
                return Json(new { Result = errorMessage });

            return View();
        }

        /// <summary>
        /// Logout logic
        /// </summary>
        /// <returns></returns>
        [Route("Logout", Name = "Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToRoute("Home");
        }

        /// <summary>
        /// Adding cookies for remmember user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="remember"></param>
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

        private string GetModelStateErrors(ModelStateDictionary dictionary)
        {
            string errorMessage = string.Empty;

            foreach (ModelState modelState in dictionary.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errorMessage += error.ErrorMessage + "\n";
                }                
            }

            return errorMessage;
        }
    }
}