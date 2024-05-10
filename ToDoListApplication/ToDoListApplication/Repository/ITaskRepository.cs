using ToDoListApplication.Models;

namespace ToDoListApplication.Repository
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetAllTasks();
    }
}
