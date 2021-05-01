using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClothBazar.Entities;
using System.Data.Entity;

namespace ClothBazar.Web.ViewModels
{
    public class ProductSearchViewModel
    {
        public int PageNo { get; set; }
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }
    // Create Product
    public class NewProductViewModel
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }


        public int CategoryID { get; set; }
        public string ImageURL { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

    //Edit product
    public class EditProductViewModel
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        public int CategoryID { get; set; }
        public string ImageURL { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

    // Details Product
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
    }
}