using ToDoListApplication.Models;

namespace ToDoListApplication.Repository.Infrastructure
{
    public interface ITaskStatusRepository
    {
        Task<IEnumerable<TaskStatusModel>> GetAllStatuses();
    }
}
