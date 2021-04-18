﻿using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class CategoriesService
    {

        public void SaveCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public List<Category> GetCategories(string search)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null &&
                                                                category.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(x => x.ID)
                        .Include(x => x.Products)
                        .ToList();
                }
                else
                {
                    return context.Categories
                        .OrderBy(x => x.ID)
                        .Include(x => x.Products)
                        .ToList();
                }
            }
        }

        // Available Categories
        public List<Category> GetAllCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories
                    .ToList();
            }
        }

        //Geting Featured Categories
        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.Where(c => c.IsFeatured && c.ImageURL != null).ToList();
            }
        }

        public Category GetCategory(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID);
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int ID)
        {
            using (var context = new CBContext())
            {
                var category = context.Categories.Where(x => x.ID == ID).Include(x => x.Products).FirstOrDefault();

                context.Products.RemoveRange(category.Products); //first delete products of this category
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
