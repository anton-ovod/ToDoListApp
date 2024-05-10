using System.ComponentModel.DataAnnotations;
using ToDoListApplication.Models;

namespace ToDoListApplication.ViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Title: ")]
        [Required(ErrorMessage = "You have to enter task title!")]
        public string Title { get; set; }

        [Display(Name = "Description: ")]
        public string? Description { get; set; }

        [Display(Name = "Due date: ")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Category: ")]
        public int? TaskCategoryID { get; set; } = null;

        public IEnumerable<TaskModel> Tasks { get; set; } = [];

        public IEnumerable<CategoryModel> Categories { get; set; } = [];
    }
}
