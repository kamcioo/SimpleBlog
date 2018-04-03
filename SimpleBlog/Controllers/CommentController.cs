using Microsoft.AspNet.Identity;
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
    public class CommentController : Controller
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IReplyRepo _replyRepo;
        public CommentController(ICommentRepo commentRepo, IReplyRepo replyRepo)
        {
            _commentRepo = commentRepo;
            _replyRepo = replyRepo;
        }
        [HttpPost]
        [Authorize]
        public ActionResult Add(PostDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(Request.UrlReferrer.ToString());
                //return RedirectToRoute("PostDetails", new { id = model.PostId, name = model.Post.GetTitleAsUrl() });
            }

            Comment comment = model.Comment;
            comment.DateAdded = DateTime.Now;
            comment.UserId = User.Identity.GetUserId();
            try
            {
                _commentRepo.AddComment(comment);
                _commentRepo.SaveChanges();
                string commentClass = "comment-" + comment.Id;
                TempData["scrollId"] = commentClass;
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddReply(PostDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = model.Reply;
            reply.DateAdded = DateTime.Now;
            reply.UserId = User.Identity.GetUserId();
            try
            {
                _replyRepo.AddReply(reply);
                _replyRepo.SaveChanges();
                string replyClass = "reply-" + reply.Id;
                TempData["scrollId"] = replyClass;
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Authorize]
        public ActionResult DeleteComment(int? commentId)
        {        
            Comment comment = _commentRepo.FindComment(commentId);
            if(comment == null)
            {
                return HttpNotFound();
            }

            if((comment.UserId == User.Identity.GetUserId()) || User.IsInRole("Admin"))
            {
                try
                {
                    _commentRepo.DeleteComment(comment);
                    _commentRepo.SaveChanges();
                    TempData["scrollId"] = "commentContent";
                    return Redirect(Request.UrlReferrer.ToString());
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
        }

        [Authorize]
        public ActionResult DeleteReply(int? replyId)
        {
            
            Reply reply = _replyRepo.FindReply(replyId);
            if (reply == null)
            {
                return HttpNotFound();
            }

            if ((reply.UserId == User.Identity.GetUserId()) || User.IsInRole("Admin"))
            {
                try
                {
                    _replyRepo.Delete(reply);
                    _replyRepo.SaveChanges();
                    string commentId = "comment-" + reply.CommentId;
                    TempData["scrollId"] = commentId;
                    return Redirect(Request.UrlReferrer.ToString());
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
        }
    }
}