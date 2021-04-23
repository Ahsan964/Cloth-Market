using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using System.Data.Entity;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class HomeController : Controller
    {
        //CategoriesService categoryService = new CategoriesService();
        HomeViewModel model = new HomeViewModel();

        public ActionResult Index()
        {
            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}