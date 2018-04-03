using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Repozytorium
{
    public interface IBlogDbContext
    {
        DbSet<Post> Posts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Reply> Replies { get; set; }
        DbSet<BlogSettings> BlogSettings { get; set; }

        int SaveChanges();

        DbEntityEntry Entry(object entity);
    }
}
