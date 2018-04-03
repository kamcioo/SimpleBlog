using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Repozytorium
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAll();
    }
}