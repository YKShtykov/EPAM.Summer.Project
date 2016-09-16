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
    [Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categories;

        public CategoryController(ICategoryService categories)
        {
            this.categories = categories;
        }

        [Route("Categories", Name = "Categories")]
        public ActionResult Categories(string SearchString, int page = 1)
        {
            var mvcCategories = GetAllOrFind(SearchString);

            ViewBag.SearchString = SearchString;
            var viewModel = new GenericPaginationModel<MvcCategory>(page, 2, mvcCategories);

            return View(viewModel);
        }

        [HttpGet]
        [Route("CreateCategory")]
        public ActionResult CreateCategory()
        {
            return View();
        }

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

        [HttpGet]
        [Route("EditCategory")]
        public ActionResult EditCategory(int id)
        {
            MvcCategory category = CategoryMapper.Map(categories.Get(id));
            return View(category);
        }

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
    }
}