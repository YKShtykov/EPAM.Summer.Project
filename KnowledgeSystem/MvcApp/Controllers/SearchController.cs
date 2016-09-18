using System.Linq;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;

namespace MvcApp.Controllers
{
    /// <summary>
    /// Class consists user search logic
    /// </summary>
    public class SearchController : Controller
    {
        private readonly IProfileService profiles;

        /// <summary>
        /// Create search controller
        /// </summary>
        /// <param name="service"></param>
        public SearchController(IProfileService service)
        {
            profiles = service;
        }

        /// <summary>
        /// Returns search page
        /// </summary>
        /// <returns></returns>
        [Route("Search")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Consists logic for user searsh
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Search", Name = "Search")]
        public ActionResult Index(SearchModel model, int page = 1)
        {
            var bllProfiles = profiles.Find(model.StringKey, model.City);
            var result = ProfileMapper.Map(bllProfiles);
            model.Profiles = new GenericPaginationModel<MvcProfile>(page, 1, result.ToList());
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Search", model);
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Returns json autocomplete help
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult FindUsers(string term)
        {
            var mvcProfiles = profiles.Find(term).Select(p => p.FirstName + " " + p.LastName);

            return Json(mvcProfiles.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}