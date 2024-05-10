namespace ToDoListApplication.Models
{
    public class TaskModel
    {
        public int TaskID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public int TaskStatusID { get; set; } = 0;

        public int? TaskCategoryID { get; set; } = null;

        public TaskModel(string title, string? description, DateTime? duedate,
                         int? taskCategoryID)
        {
            Title = title;
            Description = description;
            DueDate = duedate;
            TaskCategoryID = taskCategoryID;
        }

        public TaskModel() { }


    }
}
