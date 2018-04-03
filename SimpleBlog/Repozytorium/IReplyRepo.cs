using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Repozytorium
{
    public interface IReplyRepo
    {
        void AddReply(Reply reply);
        Reply FindReply(int? id);
        void Delete(Reply reply);
        void SaveChanges();
    }
}