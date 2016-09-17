using System.Linq;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;

namespace MvcApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProfileService profiles;

        public SearchController(IProfileService service)
        {
            profiles = service;
        }
        [Route("Search")]
        public ActionResult Index()
        {
            return View();
        }


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

        public ActionResult FindUsers(string term)
        {
            var mvcProfiles = profiles.Find(term).Select(p => p.FirstName + " " + p.LastName);

            return Json(mvcProfiles.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}