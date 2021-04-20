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
    public class ProductsService
    {
        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product.Category).State = EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        //All Products
        public List<Product> GetProducts()
        {
            using (var context = new CBContext())
            {
                return context.Products.Include(c => c.Category).ToList();
            }
        }

        // All Cart Products
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(c => IDs.Contains(c.ID)).ToList();
            }
        }

        // Single Product
        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                var product = context.Products.Find(ID);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
