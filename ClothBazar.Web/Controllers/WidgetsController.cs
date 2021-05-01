using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using Microsoft.Ajax.Utilities;

namespace ClothBazar.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool IsLatestProduct, int? CategoryID = 0)
        {
            ProductsWidgetsViewModel model = new ProductsWidgetsViewModel();
            model.IsLatestProduct = IsLatestProduct;

            if (IsLatestProduct)
            {
                model.Products = ProductsService.Instance.GetLatestProducts(4);
                
            }
            else if (CategoryID.HasValue && CategoryID.Value > 0)
            {
                model.Products = ProductsService.Instance.GetRelatedProducts(CategoryID.Value, 4);
            }
            else
            {
                model.Products = ProductsService.Instance.GetProducts(1, 8);
            }
            return PartialView(model);
        }
    }
}