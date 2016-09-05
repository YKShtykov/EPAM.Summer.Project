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
    public class CategoryController : Controller
    {
        private readonly ICategoryService categories;

        public CategoryController(ICategoryService categories)
        {
            this.categories = categories;
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Categories(string SearchString, int page = 1)
        {
            var mvcCategories = GetAllOrFind(SearchString);

            ViewBag.SearchString = SearchString;
            var viewModel = new GenericPaginationModel<MvcCategory>(page, 2, mvcCategories);

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateCategory(MvcCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categories.Create(CategoryMapper.Map(category));
                    return Redirect("~/Category/Categories");
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditCategory(int id)
        {
            MvcCategory category = CategoryMapper.Map(categories.Get(id));
            return View(category);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditCategory(MvcCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categories.Update(CategoryMapper.Map(category));
                    return Redirect("~/Category/Categories");
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveCategory(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categories.Delete(id);
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }
            return Redirect("~/Category/Categories");
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