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
    /// <summary>
    /// Class for category logic in app. It consists category creating updating and removing methods
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categories;

        /// <summary>
        /// Create category controller
        /// </summary>
        /// <param name="categories"></param>
        public CategoryController(ICategoryService categories)
        {
            this.categories = categories;
        }

        /// <summary>
        /// Returns categories page
        /// </summary>
        /// <param name="SearchString"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("Categories", Name = "Categories")]
        public ActionResult Categories(string SearchString, int page = 1)
        {
            var mvcCategories = GetAllOrFind(SearchString);

            ViewBag.SearchString = SearchString;
            var viewModel = new GenericPaginationModel<MvcCategory>(page, 2, mvcCategories);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Categories", viewModel);
            }
            else
            {
                return View(viewModel);
            }           
        }

        /// <summary>
        /// Returns category creating page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CreateCategory")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        /// <summary>
        /// Consists logic for creating category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCategory", Name = "CreateCategory")]
        public ActionResult CreateCategory(MvcCategory category)
        {
            if (ModelState.IsValid)
            {
                categories.Create(CategoryMapper.Map(category));
                return RedirectToRoute("Categories");
            }

            return View();
        }

        /// <summary>
        /// Returns category editor view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("EditCategory")]
        public ActionResult EditCategory(int id)
        {
            MvcCategory category = CategoryMapper.Map(categories.Get(id));
            return View(category);
        }

        /// <summary>
        /// Consists logic for category editing
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditCategory", Name = "EditCategory")]
        public ActionResult EditCategory(MvcCategory category)
        {
            if (ModelState.IsValid)
            {
                categories.Update(CategoryMapper.Map(category));
                return RedirectToRoute("Categories");
            }

            return View();
        }

        /// <summary>
        /// Consists logic for category removing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RemoveCategory", Name = "RemoveCategory")]
        public ActionResult RemoveCategory(int id)
        {
            if (ModelState.IsValid)
            {
                categories.Delete(id);
            }

            return RedirectToRoute("Categories");
        }

        private IEnumerable<MvcCategory> GetAllOrFind(string searchString)
        {
            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<MvcCategory> mvcCategories = allCategories.Select(c => CategoryMapper.Map(c)).ToList();

            if (!ReferenceEquals(searchString, null) && searchString != "")
            {
                mvcCategories = mvcCategories.Where(s => s.Name.Contains(searchString)).ToList();
            }

            return mvcCategories;
        }

        /// <summary>
        /// Finds and returns categories with skills in json format
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult FindCategories(string term)
        {
            var mvcCategories = categories.Find(term).Select(c=>c.Name);

            return Json(mvcCategories.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}