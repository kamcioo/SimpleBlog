using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            this.AddCategoryModel = new AddCategoryViewModel();
            this.EditCategoryModel = new EditCategoryViewModel();
        }
        public AddCategoryViewModel AddCategoryModel { get; set; }
        public EditCategoryViewModel EditCategoryModel { get; set; }
    }

    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "Add a category name!")]
        public string CategoryName { get; set; }
    }

    public class EditCategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Category EditedCategory { get; set; }
    }
}