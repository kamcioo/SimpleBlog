using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Repozytorium
{
    public interface IPostRepo
    {
        Task<List<Post>> GetAllPostsAsync();
        IQueryable<Post> GetAllPosts();
        Task<IEnumerable<Post>> GetPostsByPhrase(string phrase);
        IEnumerable<Post> GetRecentPosts(int quantity);
        IQueryable<Post> GetPostsByCategory(int categoryId);
        Task<Post> GetPostAsync(int? id);
        Post GetPost(int? id);
        Task<IEnumerable<Post>> GetUserPosts(string userName);
        Task<IEnumerable<Post>> GetByCategoryAsync(int categoryId);
        Boolean Delete(Post post);    
        void AddPost(Post post);
        void Update(Post post);
        void SaveChanges();
        
    }
}
