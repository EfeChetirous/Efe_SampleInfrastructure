using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Entity.Models;
using Infrastructure.Service;

namespace EfeCetirSampleProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var AllCategories = _categoryService.GetAll().ToList();
            return View(AllCategories);
        }

        public ActionResult Edit(int id)
        {
            var Category = _categoryService.GetById(id);
            return View(Category);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Update(model);
                return RedirectToAction("Index");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            _categoryService.Insert(model);
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
            _categoryService.Delete(category);
            var AllCategories = _categoryService.GetAll();
            return View("Index", AllCategories);
        }

    }
}