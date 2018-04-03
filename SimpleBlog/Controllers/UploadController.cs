using ImageResizer;
using SimpleBlog.ImageValidator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if(file.ContentLength > 0 && HttpPostedFileBaseExtensions.IsImage(file))
                {
                    ResizerImage.UploadImage(file);
                    ViewBag.Message = "File uploaded successfully!";
                    return View();
                }
                else
                {
                    ViewBag.Message = "File upload failed!";
                    return View();
                }
                
            } catch(Exception e)
            {
                ViewBag.Message = "File upload failed!";
                return View();
            }
        }
    }
}