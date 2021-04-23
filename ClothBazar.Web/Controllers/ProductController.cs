using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        //ProductsService productsService = new ProductsService();

       // CategoriesService categoriesService = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        #region Product Table

        /**
         * Product Partial View
         * This method is use for ajax reloading data
         *
         */
        public ActionResult ProductTable(string search, int? pageNo)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value :1 : 1;
            model.Products = ProductsService.Instance.GetProducts(search, pageNo.Value);
            var totalRecords = ProductsService.Instance.GetProductsCount(search);
            if (model.Products != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, int.Parse(ConfigurationsService.Instance.GetConfig("PageSize").Value));
                return PartialView("ProductTable", model);
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        #endregion

        #region Create Product

        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();
            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.ImageURL = model.ImageURL;
            newProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);

            ProductsService.Instance.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }
        #endregion

        #region Edit Product

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();
            var product = ProductsService.Instance.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductsService.Instance.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;

            existingProduct.Category = null; //mark it null. Because the referncy key is changed below
            existingProduct.CategoryID = model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            ProductsService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }
        #endregion

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsService.Instance.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }
    }
}