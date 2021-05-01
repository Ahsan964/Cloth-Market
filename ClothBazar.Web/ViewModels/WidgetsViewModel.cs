using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClothBazar.Entities;

namespace ClothBazar.Web.ViewModels
{
    public class ProductsWidgetsViewModel
    {
        public List<Product> Products { get; set; }
        public bool IsLatestProduct { get; set; }
    }
}