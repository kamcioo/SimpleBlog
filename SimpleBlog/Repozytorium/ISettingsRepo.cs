using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Repozytorium
{
    public interface ISettingsRepo
    {
        BlogSettings GetSettings();
        void SaveChanges();
    }
}
