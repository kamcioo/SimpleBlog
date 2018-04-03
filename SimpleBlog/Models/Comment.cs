using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        
        public string UserId { get; set; }
        public int PostId { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public ICollection<Reply> Reply { get; set; }
    }
}