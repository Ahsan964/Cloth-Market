using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClothBazar.Entities;

namespace ClothBazar.Web.ViewModels
{
    //For Category Table
    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }

    }

    public class NewCategoryViewModel
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ImageURL { get; set; }

    
        [Display(Name = "Is Featured?")]
        public bool isFeatured { get; set; }
    }

    public class EditCategoryViewModel
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ImageURL { get; set; }

        [Display(Name = "Is Featured?")]
        public bool isFeatured { get; set; }
    }
}