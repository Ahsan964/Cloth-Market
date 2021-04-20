using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entities;
using ClothBazar.Services;

namespace ClothBazar.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        ConfigurationsService ConfigurationsService = new ConfigurationsService();
        
        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }

        // Configuration Table Data
        public ActionResult ConfigTable()
        {
            var configs = ConfigurationsService.GetConfigs();
            return PartialView(configs);
        }

        // Create Configuration
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Config config)
        {
            ConfigurationsService.SaveConfig(config);
            return RedirectToAction("ConfigTable");
        }

        //Edit & Update
        [HttpGet]
        public ActionResult Edit(string Key)
        {
            var config = ConfigurationsService.GetConfig(Key);
            return PartialView(config);
        }

        [HttpPost]
        public ActionResult Edit(Config config)
        {
            ConfigurationsService.UpdateConfig(config);
            return RedirectToAction("ConfigTable");
        }

        // Delete
        [HttpPost]
        public ActionResult Delete(string Key)
        {
            ConfigurationsService.DeleteConfig(Key);
            return RedirectToAction("ConfigTable");
        }
    }
}