using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Repozytorium
{
    public class CommentRepo : ICommentRepo
    {
        private readonly IBlogDbContext _context;
        public CommentRepo(IBlogDbContext BlogDbContext)
        {
            _context = BlogDbContext;
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
        }
        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
        }
        public Comment FindComment(int? id)
        {
            return _context.Comments.Find(id);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}