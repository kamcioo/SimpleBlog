using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SimpleBlog.Models
{
    public class UserViewModel : User
    {
        public IList<string> UserRoles { get; set; }
    }

    public class UserRoleViewModel
    {
        public IEnumerable<UserViewModel> UserModel { get; set; }
        public List<IdentityRole> RolesInApp { get; set; }
    }
}