using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class Category
    {
        public Category()
        {
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required!")]
        [StringLength(20, ErrorMessage = "Category name must be between {2} and {1} characters", MinimumLength = 1)]
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}