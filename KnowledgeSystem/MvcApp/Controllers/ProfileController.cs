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
    //[Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService service;

        public ProfileController(IProfileService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult UserProfile(int? id)
        {
            var identity = (CustomIdentity)User.Identity;
            MvcProfile profile = ProfileMapper.MapProfile(service.GetProfile(id ?? identity.Id));
            return View(profile);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var identity = (CustomIdentity)User.Identity;
            MvcProfile profile = ProfileMapper.MapProfile(service.GetProfile(identity.Id));
            return View(profile);
        }

        [HttpPost]
        public ActionResult Edit(MvcProfile model)
        {
            if (ModelState.IsValid)
            {
                service.EditProfile(ProfileMapper.MapProfile(model));
            }
            return Redirect("~/Profile/UserProfile");
        }
    }
}