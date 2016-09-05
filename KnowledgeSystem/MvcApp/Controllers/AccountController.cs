﻿using System;
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
        public ActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = UserMapper.Map(service.Login(loginModel.EmailOrLogin, loginModel.Password));
                    AddCookiesToResponce(user, loginModel.IsRemember);

                    return Redirect("~/Home/Index");
                }
                catch (Exception e)
                {
                    Logger.LogInfo("Login error" + e.Message);
                    ModelState.AddModelError("", e.Message);
                }                
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    service.Create(UserMapper.Map(model));
                    Logger.LogInfo("User (Login=" + model.Login + "was created");

                    return Redirect("~/Home/Index");
                }
                catch (Exception e)
                {
                    Logger.LogInfo("Registration error"+e.Message);
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return Redirect("~/Home/Index");
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