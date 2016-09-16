using System;
using System.Web;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using MvcApp.Infrastructure;

namespace MvcApp.Controllers
{
    [Authorize]

    public class ProfileController : Controller
    {
        private readonly IProfileService profiles;
        private readonly IUserService users;
        private readonly ICategoryService categories;

        public ProfileController(IProfileService service, IUserService users, ICategoryService categories)
        {
            this.profiles = service;
            this.users = users;
            this.categories = categories;
        }

        //[HttpGet]
        [Route("User", Name = "User")]
        public ActionResult UserProfile(int? id, int page = 1)
        {
            var fullProfileInfo = new FullProfileInfo();
            var identity = (CustomIdentity)User.Identity;
            if (ReferenceEquals(id, null)) id = identity.Id;

            fullProfileInfo.Profile = ProfileMapper.Map(profiles.Get((int)id));
            var userSkills = CategoryMapper.Map(users.GetSortedUserSkills((int)id, false));
            fullProfileInfo.Categories = new GenericPaginationModel<MvcCategory>(page, 1, userSkills);

            ViewBag.ProfileId = fullProfileInfo.Profile.Id;

            return View(fullProfileInfo);
        }

        public ActionResult UserSkills(int? id, int page = 1)
        {
            var identity = (CustomIdentity)User.Identity;
            if (ReferenceEquals(id, null)) id = identity.Id;

            var userSkills = CategoryMapper.Map(users.GetSortedUserSkills((int)id, false));
            var mvcCategories = new GenericPaginationModel<MvcCategory>(page, 1, userSkills);

            ViewBag.ProfileId = id;

            return PartialView("_ProfileSkills",mvcCategories);
        }

        [HttpGet]
        [Route("Edit")]
        public ActionResult Edit()
        {
            var identity = (CustomIdentity)User.Identity;
            MvcProfile profile = ProfileMapper.Map(profiles.Get(identity.Id));

            return View(profile);
        }

        [HttpPost]
        [Route("Edit", Name = "Edit")]
        public ActionResult Edit(MvcProfile model, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                SetProfileImage(model, fileUpload);
                profiles.Update(ProfileMapper.Map(model));
            }

            return RedirectToRoute("User");
        }

        [AllowAnonymous]
        public FileContentResult GetImage(int id)
        {
            var profile = profiles.Get(id);
            if (profile != null)
            {
                var photo = profile.Image;
                if (photo != null)
                {
                    return File(photo, profile.ImageMimeType);
                }
                else
                {
                    return StandartImage();
                }

            }
            return null;
        }

        private void SetProfileImage(MvcProfile model, HttpPostedFileBase fileUpload)
        {
            if (!ReferenceEquals(fileUpload, null))
            {
                model.Image = new byte[fileUpload.ContentLength];
                fileUpload.InputStream.Read(model.Image, 0, fileUpload.ContentLength);
                model.ImageMimeType = fileUpload.ContentType;
            }
        }

        private FileContentResult StandartImage()
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/identicon.png");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "image/png");
        }

        public ActionResult UserDetails(int id)
        {
            return Json(profiles.Get(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserInfoPdf(int id)
        {
            var fullProfileInfo = new FullProfileInfo();

            fullProfileInfo.Profile = ProfileMapper.Map(profiles.Get((int)id));
            var userSkills = CategoryMapper.Map(users.GetSortedUserSkills(id, false));
            fullProfileInfo.Categories = new GenericPaginationModel<MvcCategory>(1, 100, userSkills);
            var memoryStream = PdfManager.CreateUserInfoPdf(fullProfileInfo);


            return new FileStreamResult(memoryStream, "application/pdf");
        }
    }
}