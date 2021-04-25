using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Config
    {
        [Key]
        [Display(Name = "Name")]
        public string Key { get; set; }

        [Required]
        [Display(Name = "URL OR Setting")]
        public string Value { get; set; }
    }
}
