using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Models
{
    public class UserPostDetailsViewModel
    {

        public int Id;

        [Display(Name = "Post title:")]
        public string PostTitle { get; set; }

        [Display(Name = "Added date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddedDate { get; set; }

        [Display(Name = "Updated date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Number of likes:")]
        public int? NumberOfLikes { get; set; }

        public string GetTitleAsUrl()
        {
            return PostTitle.Trim().ToLower().Replace(" ", "-").Replace(".", "").Replace(",", "");
        }
    } 

    public class PostDetailsViewModel
    {
        public PostDetailsViewModel()
        {
            this.Comment = new Comment();
            this.Reply = new Reply();
        }
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public Reply Reply { get; set; }
        //public Post Post { get; set; }
        //[Required]
        //public string CommentContent { get; set; }
        //[Required]
        //public int PostId { get; set; }
    }

    public class UserPostAddViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 5)]
        [Display(Name = "Post title")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(450, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 300)]
        [Display(Name ="Post introduction")]
        public string PostIntro { get; set; }

        [Required(ErrorMessage = "The {0} field is required!")]
        [MinLength(500, ErrorMessage = "{0} must be at least {1} characters long.")]
        [Display(Name = "Post content")]
        [AllowHtml]
        public string PostContent { get; set; }

        [Display(Name = "Category")]
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string ImagePath { get; set; }
        public int SelectedCategory { get; set; }
        [Required(ErrorMessage = "Image is required! The file must be .jpg, .jpeg, .png, .gif extension!")]
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }


        public string GetTitleAsUrl()
        {
            return PostTitle.Trim().ToLower().Replace(" ", "-").Replace(".", "").Replace(",", "");
        }
    }

    public class UserPostEditViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 5)]
        [Display(Name = "Post title")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "The {0} field is required!")]
        [StringLength(450, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 300)]
        [Display(Name = "Post introduction")]
        public string PostIntro { get; set; }

        [Required(ErrorMessage = "The {0} field is required!")]
        [MinLength(500, ErrorMessage = "{0} must be at least {1} characters long.")]
        [Display(Name = "Post content")]
        [AllowHtml]
        public string PostContent { get; set; }

        [Display(Name = "Category")]
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string ImagePath { get; set; }
        public int SelectedCategory { get; set; }
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }


        public string GetTitleAsUrl()
        {
            return PostTitle.Trim().ToLower().Replace(" ", "-").Replace(".", "").Replace(",", "");
        }
    }

    public class CategoriesWithLastPosts
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Post> RecentPosts { get; set; }
    }
}