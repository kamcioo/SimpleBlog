using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Repozytorium
{
    public class UserRepo : IUserRepo
    {
        private readonly BlogDbContext _context;
        public UserRepo(IBlogDbContext context)
        {
            _context = (BlogDbContext) context;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }
    }
}