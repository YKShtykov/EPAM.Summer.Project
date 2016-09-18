using System;
using System.Web;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using MvcApp.Infrastructure;

namespace MvcApp.Controllers
{
    /// <summary>
    /// Class for profile logic. Consists logic for edit and looking profiles
    /// </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService profiles;
        private readonly IUserService users;
        private readonly ICategoryService categories;

        /// <summary>
        /// Create profile controller
        /// </summary>
        /// <param name="service"></param>
        /// <param name="users"></param>
        /// <param name="categories"></param>
        public ProfileController(IProfileService service, IUserService users, ICategoryService categories)
        {
            this.profiles = service;
            this.users = users;
            this.categories = categories;
        }

        /// <summary>
        /// Returns user profile page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns user skills partial view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult UserSkills(int? id, int page = 1)
        {
            var identity = (CustomIdentity)User.Identity;
            if (ReferenceEquals(id, null)) id = identity.Id;

            var userSkills = CategoryMapper.Map(users.GetSortedUserSkills((int)id, false));
            var mvcCategories = new GenericPaginationModel<MvcCategory>(page, 1, userSkills);

            ViewBag.ProfileId = id;

            return PartialView("_ProfileSkills",mvcCategories);
        }

        /// <summary>
        /// Returns profile edit page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Edit")]
        public ActionResult Edit()
        {
            var identity = (CustomIdentity)User.Identity;
            MvcProfile profile = ProfileMapper.Map(profiles.Get(identity.Id));

            return View(profile);
        }

        /// <summary>
        /// Consists logic for profile editing
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns profile image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns json profile info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserDetails(int id)
        {
            return Json(profiles.Get(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates pdf document with profile info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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