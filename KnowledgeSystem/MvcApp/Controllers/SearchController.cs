using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using MvcApp.Infrastructure;

namespace MvcApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProfileService service;

        public SearchController(IProfileService service)
        {
            this.service = service;
        }
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchModel model, int page =1)
        {
            var bllProfiles = service.Search(SearchModelMapper.Map(model));
            var result = ProfileMapper.Map(bllProfiles);
            model.Profiles = new GenericPaginationModel<MvcProfile>(page,2,result.ToList());

            return View(model);

        }
    }
}