using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleBlog.Repozytorium;

namespace SimpleBlog.Models
{
    public class BlogDbContext : IdentityDbContext<User>, IBlogDbContext
    {
        public BlogDbContext()
            : base("DefaultConnection")
        {
        }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<BlogSettings> BlogSettings { get; set; }
    }
}