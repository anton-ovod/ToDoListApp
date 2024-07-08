using System.ComponentModel.DataAnnotations;

namespace ToDoListApplication.Models
{
    public class TaskModel
    {
        public Guid TaskID { get; set; }

        [Display(Name = "Title: ")]
        [Required(ErrorMessage = "You have to enter task title!")]
        public string Title { get; set; }

        [Display(Name = "Description: ")]
        public string? Description { get; set; }

        [Display(Name = "Due date: ")]
        public DateTime? DueDate { get; set; }

        public int TaskStatusID { get; set; } = 0;

        [Display(Name = "Category: ")]
        public int? TaskCategoryID { get; set; } = null;


    }
}
