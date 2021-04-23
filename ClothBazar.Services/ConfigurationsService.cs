using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;
using ClothBazar.Entities;

namespace ClothBazar.Services
{
    public class ConfigurationsService
    {
        #region Singleton

        public static ConfigurationsService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigurationsService();
                }

                return instance;
            }
        }
        private static ConfigurationsService instance { get; set; }

        private ConfigurationsService()
        {

        }
        #endregion

        public List<Config> GetConfigs(int pageNo)
        {
            int pageSize = int.Parse(ConfigurationsService.Instance.GetConfig("PageSize").Value);
            using (var context = new CBContext())
            {
                
                return context.Configurations.OrderBy(c => c.Key)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)                
                    .ToList();
            }
        }

        public int GetConfigsCount()
        {
            using (var context = new CBContext())
            {
                return context.Configurations.Count();
            }
        }

        public void SaveConfig(Config config)
        {
            using (var context = new CBContext())
            {
                context.Configurations.Add(config);
                context.SaveChanges();
            }
        }

        public Config GetConfig(string Key)
        {
            using (var context = new CBContext())
            {
                return context.Configurations.Find(Key);
            }
        }

        public void UpdateConfig(Config config)
        {
            using (var context = new CBContext())
            {
                context.Entry(config).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteConfig(string Key)
        {
            using (var context = new CBContext())
            {
                var config = context.Configurations.Find(Key);
                context.Configurations.Remove(config);
                context.SaveChanges();
            }
        }
    }
}
