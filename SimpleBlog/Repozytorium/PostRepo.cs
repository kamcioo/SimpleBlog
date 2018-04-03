using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SimpleBlog.Repozytorium
{
    public class PostRepo : IPostRepo
    {
        private readonly IBlogDbContext _context;

        public PostRepo(IBlogDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await GetAllPosts().ToListAsync();
        }

        public IQueryable<Post> GetAllPosts()
        {
            return _context.Posts.AsQueryable().OrderByDescending(p => p.AddedDate);
        }

        public async Task<Post> GetPostAsync(int? id)
        {
            return await _context.Posts
                .FindAsync(id);
        }

        public Post GetPost(int? id)
        {
            Post post = _context.Posts
                .Where(p => p.Id == id)
                .Include(p => p.Comments.Select( r => r.Reply))
                .FirstOrDefault();
            return post;
        }

        public async Task<IEnumerable<Post>> GetUserPosts(string userName)
        {

            return await _context.Posts
                .Where(p => p.User.UserName == userName)
                .OrderBy(p => p.AddedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Posts
                .Where(p => p.Category.Id == categoryId)
                .OrderBy(p => p.AddedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByPhrase(string phrase)
        {
            var result = _context.Posts
                 .Where(p => p.PostTitle.Contains(phrase) || p.PostContent.Contains(phrase) || p.PostIntro.Contains(phrase))
                 .OrderByDescending(p => p.AddedDate)
                 .ToListAsync();
            return await result;
        }

        public IQueryable<Post> GetPostsByCategory(int categoryId)
        {
            return _context.Posts
                .Where(p => p.Category.Id == categoryId)
                .OrderByDescending(p => p.AddedDate);
        }

        public IEnumerable<Post> GetRecentPosts(int quantity)
        {
            var result = _context.Posts.OrderByDescending(p => p.AddedDate).Take(quantity);
            return result;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(Post post)
        {
            post.UpdatedDate = DateTime.Now;
            _context.Entry(post).State = EntityState.Modified;
        }

        public Boolean Delete(Post post)
        {
            _context.Posts.Remove(post);
            return true;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }


    }
}