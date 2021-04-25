using ClothBazar.Entities;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
       // CategoriesService categoryService = new CategoriesService();
        


        // GET: Category
        public ActionResult Index()
        {
            
            return View();
        }

        //Category Table
        public ActionResult CategoryTable(string search,  int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecords = CategoriesService.Instance.GetCategoriesCount(search);

            model.Categories = CategoriesService.Instance.GetCategories(search, pageNo.Value);

            if (model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, int.Parse(ConfigurationsService.Instance.GetConfig("PageSize").Value));
                return PartialView("CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageURL = model.ImageURL;
                newCategory.IsFeatured = model.isFeatured;


                CategoriesService.Instance.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }

        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {

            EditCategoryViewModel model = new EditCategoryViewModel();
            var category = CategoriesService.Instance.GetCategory(ID);
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.IsFeatured;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = CategoriesService.Instance.GetCategory(model.ID);
                existingCategory.ID = model.ID;
                existingCategory.Name = model.Name;
                existingCategory.Description = model.Description;
                existingCategory.ImageURL = model.ImageURL;
                existingCategory.IsFeatured = model.isFeatured;

                CategoriesService.Instance.UpdateCategory(existingCategory);
                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }

        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var category = CategoriesService.Instance.GetCategory(ID);
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            CategoriesService.Instance.DeleteCategory(category.ID);
            return RedirectToAction("CategoryTable");
        }
    }
}