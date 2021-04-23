using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class ConfigurationController : Controller
    {
       // ConfigurationsService ConfigurationsService = new ConfigurationsService();
        
        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }

        // Configuration Table Data
        public ActionResult ConfigTable(int? pageNo)
        {
            ConfigViewModel model = new ConfigViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.Configs = ConfigurationsService.Instance.GetConfigs(pageNo.Value);
            var totalRecords = ConfigurationsService.Instance.GetConfigsCount();

            if (model.Configs != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, int.Parse(ConfigurationsService.Instance.GetConfig("PageSize").Value));
                return PartialView("ConfigTable", model);
            }
            else
            {
                return HttpNotFound();
            }
            
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
            ConfigurationsService.Instance.SaveConfig(config);
            return RedirectToAction("ConfigTable");
        }

        //Edit & Update
        [HttpGet]
        public ActionResult Edit(string Key)
        {
            var config = ConfigurationsService.Instance.GetConfig(Key);
            return PartialView(config);
        }

        [HttpPost]
        public ActionResult Edit(Config config)
        {
            ConfigurationsService.Instance.UpdateConfig(config);
            return RedirectToAction("ConfigTable");
        }

        // Delete
        [HttpPost]
        public ActionResult Delete(string Key)
        {
            ConfigurationsService.Instance.DeleteConfig(Key);
            return RedirectToAction("ConfigTable");
        }
    }
}