using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.ImageValidator
{
    public static class ResizerImage
    {
        public static string UploadImage(HttpPostedFileBase file)
        {
            try
            {
                string prefix = DateTime.Now.ToString("MMddyyyyHHmmss");
                string filename = prefix  + "_" + Path.GetFileName(file.FileName);
                string path = "/UploadedFiles/PostImages";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~" + path), filename);
                string postDetailVersion = "width=1200&height=400&mode=stretch&format=jpeg&scale=both";
                //file.SaveAs(fullPath);
                string absolutePath =  path  + "/" + filename + ".jpg";

                    file.InputStream.Seek(0, SeekOrigin.Begin);
                    ImageBuilder.Current.Build(
                        new ImageJob(
                            file.InputStream,
                            fullPath,
                            new Instructions(postDetailVersion),
                            false,
                            true));
                
                return absolutePath;
            } catch(Exception e)
            {
                return null;
            }
            
        }
    }
}