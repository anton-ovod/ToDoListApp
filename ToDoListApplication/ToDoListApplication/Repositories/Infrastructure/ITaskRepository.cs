using ToDoListApplication.Models;

namespace ToDoListApplication.Repository.Infrastructure
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetAllTasks();

        Task Insert(TaskModel task);
        Task Update(TaskModel task);
        Task Delete(TaskModel task);

    }
}
