using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SimpleBlog.Models;
using System.Data.Entity;

namespace SimpleBlog.Repozytorium
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly IBlogDbContext _context;

        public CategoryRepo(IBlogDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            var result = _context.Categories;
            return result;
        }

        public Category GetById(int id)
        {
            var result = _context.Categories.FirstOrDefault(i => i.Id == id);
            return result;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}