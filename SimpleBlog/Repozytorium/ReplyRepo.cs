using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Models;

namespace SimpleBlog.Repozytorium
{
    public class ReplyRepo : IReplyRepo
    {
        private readonly IBlogDbContext _context;
        public ReplyRepo(IBlogDbContext context)
        {
            _context = context;
        }

        public void AddReply(Reply reply)
        {           
            _context.Replies.Add(reply);
        }
        public Reply FindReply(int? id)
        {
            return _context.Replies.Find(id);
        }

        public void Delete(Reply reply)
        {
            _context.Replies.Remove(reply);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}