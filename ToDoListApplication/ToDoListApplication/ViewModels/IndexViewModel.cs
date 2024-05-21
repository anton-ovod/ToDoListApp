using System.ComponentModel.DataAnnotations;
using ToDoListApplication.Models;

namespace ToDoListApplication.ViewModels
{
    public class IndexViewModel
    {
        public TaskModel Task { get; set; } = new ();

        public IEnumerable<TaskModel> Tasks { get; set; } = [];

        public IEnumerable<CategoryModel> Categories { get; set; } = [];

        public IEnumerable<TaskStatusModel> Statuses { get; set; } = [];
    }
}
