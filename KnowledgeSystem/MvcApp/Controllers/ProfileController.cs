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
            MvcProfile profile = ProfileMapper.Map(service.Get(id ?? identity.Id));
            return View(profile);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var identity = (CustomIdentity)User.Identity;
            MvcProfile profile = ProfileMapper.Map(service.Get(identity.Id));

            return View(profile);
        }

        [HttpPost]
        public ActionResult Edit(MvcProfile model, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (!ReferenceEquals(fileUpload,null))
                {
                    model.Image = new byte[fileUpload.ContentLength];
                    fileUpload.InputStream.Read(model.Image, 0, fileUpload.ContentLength);
                    model.ImageMimeType = fileUpload.ContentType; 
                }

                try
                {
                    service.Update(ProfileMapper.Map(model));
                    Logger.LogInfo("Profile( Id=" + model.Id + "was changed");
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }
            return Redirect("~/Profile/UserProfile");
        }

        public FileContentResult GetImage(int id)
        {
            var profile = service.Get(id);
            if (profile != null)
            {
                var photo = profile.Image;
                if (photo != null)
                    return File(photo, profile.ImageMimeType);
            }
            return null;
        }
    }
}