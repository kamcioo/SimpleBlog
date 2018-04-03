using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SimpleBlog.ImageValidator;
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
    public class PostController : Controller
    {
        private readonly IPostRepo _postRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ISettingsRepo _settingsRepo;
        private readonly IMapper _mapper;

        public PostController(IPostRepo postRepo, IMapper mapper, ICategoryRepo categoryRepo, ISettingsRepo settingsRepo)
        {
            _postRepo = postRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _settingsRepo = settingsRepo;
        }

        [Route("{id:int}/{name}", Name = "PostDetails")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = _postRepo.GetPost(id);
            PostDetailsViewModel model = new PostDetailsViewModel();
            model.Post = post;
            
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, Moderator")]
        [Route("user-posts", Name = "UserPosts")]
        public async Task<ActionResult> UserPosts(int? page, string sortOrder)
        {
            int currentPage = page ?? 1;
            int quantityOfPostsOnPage = 10;

            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.UpdatedDateSortParm = sortOrder == "UpdatedDate" ? "updated_date_desc" : "UpdatedDate";
            List<UserPostDetailsViewModel> mapedPosts = new List<UserPostDetailsViewModel>();

            try
            {
                var userName = User.Identity.Name;
                var userPosts = _postRepo.GetUserPosts(userName);
                mapedPosts = _mapper.Map<IEnumerable<Post>, List<UserPostDetailsViewModel>>(await userPosts);
            }
            catch (Exception e)
            {
                return View();
            }

            switch (sortOrder)
            {
                case "title_desc":
                    mapedPosts = mapedPosts.OrderByDescending(m => m.PostTitle).ToList();
                    break;
                case "Date":
                    mapedPosts = mapedPosts.OrderBy(m => m.AddedDate).ToList();
                    break;
                case "date_desc":
                    mapedPosts = mapedPosts.OrderByDescending(m => m.AddedDate).ToList();
                    break;
                case "UpdatedDate":
                    mapedPosts = mapedPosts.OrderBy(m => m.UpdatedDate).ToList();
                    break;
                case "updated_date_desc":
                    mapedPosts = mapedPosts.OrderByDescending(m => m.UpdatedDate).ToList();
                    break;
                default:
                    mapedPosts = mapedPosts.OrderBy(m => m.PostTitle).ToList();
                    break;
            }
            return View(mapedPosts.ToPagedList<UserPostDetailsViewModel>(currentPage, quantityOfPostsOnPage));

        }

        [Authorize]
        //[Route("post/edit/{id}")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var post = await _postRepo.GetPostAsync(id);


                if (post == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (!(post.User.Id == User.Identity.GetUserId() || User.IsInRole("Admin")))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var mapedPost = _mapper.Map<Post, UserPostEditViewModel>(post);
                var categories = _categoryRepo.GetAll();
                mapedPost.SelectedCategory = post.CategoryId;
                mapedPost.Categories = categories;
                return View(mapedPost);

            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind]UserPostEditViewModel postFromView)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Edit");
            }

            try
            {
                var originalPost = await _postRepo.GetPostAsync(postFromView.Id);
                var path = originalPost.ImagePath;
                if (originalPost.User.Id == User.Identity.GetUserId() || User.IsInRole("Admin"))
                {
                    var maped = _mapper.Map<UserPostEditViewModel, Post>(postFromView, originalPost);
                    maped.CategoryId = postFromView.SelectedCategory;
                    if (postFromView.ImageFile != null)
                    {
                        string imagePath = ResizerImage.UploadImage(postFromView.ImageFile);
                        maped.ImagePath = imagePath;
                    }
                    else
                    {
                        maped.ImagePath = path;
                    }        
                    _postRepo.Update(maped);
                    _postRepo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return View(postFromView);
            }
            TempData["editError"] = "false";
            return RedirectToRoute("PostDetails", new { id = postFromView.Id, name = postFromView.GetTitleAsUrl() });
        }

        [Authorize]
        //[Route("post/add")]
        public ActionResult Add()
        {
            if (!(User.IsInRole("Admin") || User.IsInRole("Moderator")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categories = _categoryRepo.GetAll();
            var model = new UserPostAddViewModel();
            model.Categories = categories;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Add([Bind]UserPostAddViewModel post)
        {
            if (!(User.IsInRole("Admin") || User.IsInRole("Moderator")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            if (!ModelState.IsValid && !HttpPostedFileBaseExtensions.IsImage(post.ImageFile))
            {
                var categories = _categoryRepo.GetAll();
                var model = new UserPostAddViewModel();
                model.Categories = categories;
                return RedirectToAction("Add");
            }

            var mapedPost = _mapper.Map<UserPostAddViewModel, Post>(post);
            try
            {
                mapedPost.UserId = User.Identity.GetUserId();
                mapedPost.CategoryId = post.SelectedCategory;
                string imagePath = ResizerImage.UploadImage(post.ImageFile);
                mapedPost.ImagePath = imagePath;
                _postRepo.AddPost(mapedPost);
                _postRepo.SaveChanges();
            }
            catch (Exception e)
            {
                return RedirectToAction("Add");
            }
            TempData["addError"] = "false";
            return RedirectToRoute("PostDetails", new { id = mapedPost.Id, name = post.GetTitleAsUrl() });
        }

        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var post = await _postRepo.GetPostAsync(id);
                _postRepo.Delete(post);
                _postRepo.SaveChanges();
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["deleteError"] = "false";
            return RedirectToAction("Index", "Home");
        }

        [Route("search")]
        public async Task<ActionResult> GetPostsByPhrase(string phrase, int? page)
        {
            int currentPage = page ?? 1;
            int quantityOfPostsOnPage = _settingsRepo.GetSettings().PostsOnSite;
            try
            {
                var posts = await _postRepo.GetPostsByPhrase(phrase);
                ViewBag.Phrase = phrase;
                return View("ByPhrase", posts.ToPagedList<Post>(currentPage, quantityOfPostsOnPage));
            } catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
        }
    }
}