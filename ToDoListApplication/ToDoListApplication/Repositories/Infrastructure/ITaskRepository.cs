using ToDoListApplication.Models;

namespace ToDoListApplication.Repository.Infrastructure
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetAllTasks();
        Task<TaskModel> GetTaskById(Guid taskId);
        Task Insert(TaskModel task);
        Task Update(TaskModel task);
        Task DeleteById(Guid taskId);

    }
}
