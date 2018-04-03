using PagedList;
using SimpleBlog.Models;
using SimpleBlog.Repozytorium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPostRepo _postRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ISettingsRepo _settingsRepo;

        public HomeController(IPostRepo postRepo, ICategoryRepo categoryRepo, ISettingsRepo settingsRepo)
        {
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
            _settingsRepo = settingsRepo;
        }

        [Route("")]
        public async Task<ActionResult> Index(int? page)
        {
            int currentPage = page ?? 1;
            int quantityOfPostsOnPage = _settingsRepo.GetSettings().PostsOnSite;
            try
            {
                var posts = await _postRepo.GetAllPostsAsync();
                return View(posts.ToPagedList<Post>(currentPage, quantityOfPostsOnPage));
            } catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }    
        }
    }
}