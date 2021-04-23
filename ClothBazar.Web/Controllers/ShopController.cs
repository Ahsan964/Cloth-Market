using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
       // private ProductsService productsService = new ProductsService();
        // GET: Shop
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var cartProducts = Request.Cookies["CartProducts"];
            if (cartProducts != null)
            {
                model.CartProductIDs = cartProducts.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductIDs);
            }
            return View(model);
        }
    }
}