using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Repozytorium
{
    public class SettingsRepo : ISettingsRepo
    {
        private readonly IBlogDbContext _context;
        public SettingsRepo(IBlogDbContext context)
        {
            _context = context;
        }

        public BlogSettings GetSettings()
        {
            return _context.BlogSettings.FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}