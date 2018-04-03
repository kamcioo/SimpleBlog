using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SimpleBlog.Models;
using SimpleBlog.Repozytorium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ISettingsRepo _settingsRepo;
        private UserManager _userManager;
        public AdminController(IUserRepo userRepo, IMapper mapper, ICategoryRepo categoryRepo, ISettingsRepo settingsRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _settingsRepo = settingsRepo;
        }

        public UserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Route("admin/panel", Name = "AdminPanel")]
        public ActionResult MainPanel()
        {
            return RedirectToAction("ManageUsers");
        }

        [Route("admin/manage-users", Name = "ManageUsers")]
        public async Task<ActionResult> ManageUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            try
            {
                var users = _userRepo.GetAll();
                var usersWithRole = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
                foreach(var u in usersWithRole)
                {
                    var userRoles = await UserManager.GetRolesAsync(u.Id);
                    u.UserRoles = userRoles;
                }
                var rolesInApp = roleManager.Roles.ToList();

                UserRoleViewModel viewModel = new UserRoleViewModel();
                viewModel.UserModel = usersWithRole;
                viewModel.RolesInApp = rolesInApp;

                return View(viewModel);
            } catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }

        [Route("admin/manage-categories", Name = "ManageCategories")]
        public ActionResult ManageCategories()
        {
            CategoryViewModel model = new CategoryViewModel();
            try
            {               
                var categories = _categoryRepo.GetAll();
                model.EditCategoryModel.Categories = categories;
                return View(model);
            } catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        [Authorize]
        [Route("admin/global-settings", Name = "ManageGlobalSettings")]
        public ActionResult ManageGlobalSettings()
        {
            var model = _settingsRepo.GetSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditGlobalSettings([Bind]BlogSettings model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ManageGlobalSettings");
            }
            try
            {
                var settings = _settingsRepo.GetSettings();
                settings.PostsOnSite = model.PostsOnSite;
                settings.RecentPosts = model.RecentPosts;
                _settingsRepo.SaveChanges();

            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("ManageGlobalSettings", "Admin");
        }
    }
}