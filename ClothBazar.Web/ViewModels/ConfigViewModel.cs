using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClothBazar.Entities;

namespace ClothBazar.Web.ViewModels
{
    public class ConfigViewModel
    {
        public List<Config> Configs { get; set; }
        public Pager Pager { get; set; }
    }
}