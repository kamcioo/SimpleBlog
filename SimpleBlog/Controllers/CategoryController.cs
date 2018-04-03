using AutoMapper;
using PagedList;
using SimpleBlog.Models;
using SimpleBlog.Repozytorium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly IPostRepo _postRepo;
        private readonly ISettingsRepo _settingsRepo;
        public CategoryController(ICategoryRepo categoryRepo, IMapper mapper, IPostRepo postRepo, ISettingsRepo settingsRepo)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _postRepo = postRepo;
            _settingsRepo = settingsRepo;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public PartialViewResult RightSearchWindow()
        {
            var categories = _categoryRepo.GetAll();
            CategoriesWithLastPosts model = new CategoriesWithLastPosts();
            model.Categories = categories;
            var recentPosts = _postRepo.GetRecentPosts(_settingsRepo.GetSettings().RecentPosts);
            model.RecentPosts = recentPosts;
            return PartialView("_RightSearchWindow", model);
        }

        [Route("{id:int}/{category}/posts", Name = "PostsByCategory")]
        public ActionResult PostsByCategory(int id, int? page, string category)
        {
            int currentPage = page ?? 1;
            int quantityOfPostsOnPage = _settingsRepo.GetSettings().PostsOnSite;
            try
            {
                var posts = _postRepo.GetPostsByCategory(id).ToList();
                ViewBag.Category = category;
                return View(posts.ToPagedList<Post>(currentPage, quantityOfPostsOnPage));
            } catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult ChangeName(EditCategoryViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ManageCategories", "Admin");
                //return Redirect(Request.UrlReferrer.ToString());
            }
            try
            {
                var category =_categoryRepo.GetById(model.EditedCategory.Id);
                category.Name = model.EditedCategory.Name;
                _categoryRepo.SaveChanges();
                return RedirectToAction("ManageCategories", "Admin");
            } catch(Exception e)
            {
                return RedirectToAction("ManageCategories", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ManageCategories", "Admin");
            }

            try
            {
                Category category = new Category();
                category.Name = model.CategoryName;
                _categoryRepo.Add(category);
                _categoryRepo.SaveChanges();
                return RedirectToAction("ManageCategories", "Admin");
            } catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? categoryId)
        {
            try
            {
                Category category = _categoryRepo.GetById((int)categoryId);
                _categoryRepo.Delete(category);
                _categoryRepo.SaveChanges();
                return RedirectToAction("ManageCategories", "Admin");
            } catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}