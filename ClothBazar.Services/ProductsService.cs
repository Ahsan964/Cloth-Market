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
        #region Singlton

        public static ProductsService Instance
        { 
            get{
                if (instance == null)
                {
                        instance = new ProductsService();
                }

                return instance;
            }
        }
        private static ProductsService instance { get; set; }

        private ProductsService()
        {
            
        }
        #endregion

        // Save Product
        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product.Category).State = EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        // Products Count
        public int GetProductsCount(string search)
        {
            using (var context = new CBContext())
            {
                if (string.IsNullOrEmpty(search) == false)
                {
                    return context.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower()))
                        .Count();
                }
                else
                {
                    return context.Products.Count();
                }

            }
        }

        //All Products Page Wise
        public List<Product> GetProducts(string search, int pageNo)
        {
            int pageSize = int.Parse(ConfigurationsService.Instance.GetConfig("PageSize").Value);

            using (var context = new CBContext())
            {
                if (string.IsNullOrEmpty(search) == false)
                {
                     return context.Products.Where(p => p.Name != null && p.Name.ToLower().
                             Contains(search.ToLower())).
                         OrderBy(c => c.ID).
                         Skip((pageNo - 1) * pageSize).
                         Take(pageSize).
                         Include(c => c.Category).
                         ToList();
                }
                else
                {
                    return context.Products.
                        OrderBy(c => c.ID).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        Include(c => c.Category).
                        ToList();
                }
            }

        }

        // Latest Products
        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new CBContext())
            {
              
                    return context.Products.
                        OrderByDescending(c => c.ID).
                        Take(numberOfProducts).
                        Include(c => c.Category).
                        ToList();
            }

        }

        // Related Products
        // Products Of All Categories
        public List<Product> GetRelatedProducts(int CategoryID, int pageSize)
        {

            using (var context = new CBContext())
            {

                return context.Products.Where(c => c.Category.ID == CategoryID).OrderByDescending(c => c.ID).Take(pageSize)
                    .Include(c => c.Category).ToList();


            }
        }

        // Products Of All Categories
        public List<Product> GetProducts(int pageNo, int pageSize)
        {

            using (var context = new CBContext())
            {

                return context.Products.OrderByDescending(c => c.ID).Skip((pageNo - 1) * pageSize).Take(pageSize)
                    .Include(c => c.Category).ToList();


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

        // Update Product
        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        // Delete Product
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

