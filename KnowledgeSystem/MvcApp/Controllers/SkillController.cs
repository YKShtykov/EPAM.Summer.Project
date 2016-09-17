﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;
using MvcApp.Infrastructure;

namespace MvcApp.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        private readonly IUserService service;
        private readonly ISkillService skills;
        private readonly ICategoryService categories;

        public SkillController(IUserService service, ISkillService skillService, ICategoryService categories)
        {
            this.service = service;
            this.skills = skillService;
            this.categories = categories;
        }

        [Route("YourSkills")]
        public ActionResult Index(int page = 1)
        {
            var id = ((CustomIdentity)User.Identity).Id;
            var userSkills = CategoryMapper.Map(service.GetSortedUserSkills(id, true));
            var viewModel = new GenericPaginationModel<MvcCategory>(page, 2, userSkills);

            return View(viewModel);
        }


        [HttpPost]
        [Route("YourSkills", Name = "UserSkills")]
        public ActionResult Index(List<MvcCategory> Entities,int? page, int currentPage)
        {
            var id = ((CustomIdentity)User.Identity).Id;
            if (ReferenceEquals(page,null))
            {
                service.UpdateUserSkills(id, CategoryMapper.Map(Entities));
                page = currentPage;
            }            
            if (Request.IsAjaxRequest())
            {
                var userSkills = CategoryMapper.Map(service.GetSortedUserSkills(id, true));
                var viewModel = new GenericPaginationModel<MvcCategory>((int)page, 2, userSkills);
                return PartialView("_Skills", viewModel);
            }
            else
            {
                return Redirect("~/YourSkills/?page=" + page);
            }
        }

        [Authorize(Roles = "Administrator")]
        [Route("Skills", Name = "Skills")]
        public ActionResult Skills(string FindSkill, string FindCategory, int page = 1)
        {
            var mvcCategories = FindSkills(FindSkill, FindCategory);
            var viewModel = new GenericPaginationModel<MvcCategory>(page, 2, mvcCategories);
            var categoryNames = new List<string>();

            foreach (var item in categories.GetAll())
            {
                categoryNames.Add(item.Name);
            }
            ViewBag.Categories = categoryNames;
            ViewBag.FindSkill = FindSkill;
            ViewBag.FindCategory = FindCategory;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SkillsRedactorPartial", viewModel);
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("CreateSkill")]
        public ActionResult CreateSkill(string category = "")
        {
            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<string> mvcCategories = allCategories.Select(c => c.Name).ToList();
            ViewBag.Categories = mvcCategories;
            ViewBag.Category = category;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("CreateSkill", Name = "CreateSkill")]
        public ActionResult CreateSkill(MvcSkill skill)
        {
            if (ModelState.IsValid)
            {
                skills.Create(SkillMapper.Map(skill));
                return RedirectToRoute("Skills");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("EditSkill")]
        public ActionResult EditSkill(int id)
        {
            MvcSkill skill = SkillMapper.Map(skills.Get(id));

            IEnumerable<BllCategory> allCategories = categories.GetAll();
            List<string> mvcCategories = allCategories.Select(c => c.Name).ToList();
            ViewBag.Categories = mvcCategories;

            return View(skill);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("EditSkill", Name = "EditSkill")]
        public ActionResult EditSkill(MvcSkill skill)
        {
            if (ModelState.IsValid)
            {
                skills.Update(SkillMapper.Map(skill));
                return RedirectToRoute("Skills");
            }
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [Route("RemoveSkill", Name = "RemoveSkill")]
        public ActionResult RemoveSkill(int id)
        {
            if (ModelState.IsValid)
            {
                skills.Delete(id);
            }
            return RedirectToRoute("Skills");
        }

        private List<MvcCategory> FindSkills(string skill, string category)
        {
            var mvcCategories = CategoryMapper.Map(categories.GetAll());

            if (!ReferenceEquals(category, null) && category != "")
            {
                mvcCategories = mvcCategories.Where(s => s.Name == category).ToList();
            }

            if (!ReferenceEquals(skill, null) && skill != "")
            {
                foreach (var item in mvcCategories)
                {
                    item.Skills = item.Skills.Where(s => s.Name.Contains(skill)).ToList();
                }
            }

            return mvcCategories.Where(c => c.Skills.Count > 0).ToList();
        }
    }
}