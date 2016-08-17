﻿using System;
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
                MvcUser user;
                try
                {
                    user = UserMapper.MapUser(service.LoginUser(loginModel.EmailOrLogin, loginModel.Password));

                }
                catch (Exception)
                {

                    throw;
                }
                string userData = JsonConvert.SerializeObject(user);
                var ticket = new FormsAuthenticationTicket(1,
                                                           user.Id.ToString(),
                                                           DateTime.Now,
                                                           DateTime.Now.AddMinutes(40),
                                                           loginModel.IsRemember,
                                                           userData);
                var encoded = FormsAuthentication.Encrypt(ticket);
                var coockie = new HttpCookie(FormsAuthentication.FormsCookieName, encoded);
                if (ticket.IsPersistent)
                    coockie.Expires = ticket.Expiration;
                Response.Cookies.Add(coockie);
                return Redirect("~/Home/Index");
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
                    service.CreateUser(UserMapper.MapUser(model));
                    return Redirect("~/Home/Index");
                }
                catch (Exception e)
                {
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
    }
}