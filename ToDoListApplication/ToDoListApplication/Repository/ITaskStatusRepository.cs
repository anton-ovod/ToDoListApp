using ToDoListApplication.Models;

namespace ToDoListApplication.Repository
{
    public interface ITaskStatusRepository
    {
        Task<IEnumerable<TaskStatusModel>> GetAllStatuses();
    }
}
