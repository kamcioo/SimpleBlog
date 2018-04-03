using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Repozytorium
{
    public interface ICommentRepo
    {
        void AddComment(Comment comment);
        void SaveChanges();
        void DeleteComment(Comment comment);

        Comment FindComment(int? id);

    }
}