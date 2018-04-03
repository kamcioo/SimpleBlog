using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Models
{
    public class Post
    {
        public Post()
        {
            this.NumberOfLikes = 0;
            this.AddedDate = DateTime.Now;
        }

        [Display(Name = "Id")]
        public int Id { get; set; }


        [Display(Name = "Post title")]
        [Required]
        public string PostTitle { get; set; }

        [Display(Name = "Post introduction")]
        [Required]
        public string PostIntro { get; set; }

        [Display(Name = "Post content")]
        [AllowHtml]
        [Required]
        public string PostContent { get; set; }

        [Display(Name = "Added date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddedDate { get; set; }

        [Display(Name = "Updated date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Number of likes")]
        public int? NumberOfLikes { get; set; }

        public string ImagePath { get; set; }

        public string UserId { get; set; }

        public string GetTitleAsUrl()
        {
            return PostTitle.Trim().ToLower().Replace(" ", "-").Replace(".", "").Replace(",", "");
        }

        public virtual User User { get; set; }
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public virtual Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
    

}