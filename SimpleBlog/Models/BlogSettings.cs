using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class BlogSettings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Posts on site")]
        public int PostsOnSite { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Recent posts quantity")]
        public int RecentPosts { get; set; }
    }
}