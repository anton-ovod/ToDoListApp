using ToDoListApplication.Models;

namespace ToDoListApplication.ViewModels
{
    public class IndexViewModel
    {
        public TaskModel NewTask;
        public IEnumerable<TaskModel> Tasks { get; set; } = [];

        public IEnumerable<CategoryModel> Categories { get; set; } = [];
    }
}
