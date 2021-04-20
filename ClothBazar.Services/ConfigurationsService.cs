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
        public List<Config> GetConfigs()
        {
            using (var context = new CBContext())
            {
                return context.Configurations.ToList();
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
