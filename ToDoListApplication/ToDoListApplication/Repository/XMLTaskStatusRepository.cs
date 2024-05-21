using ToDoListApplication.Models;

namespace ToDoListApplication.Repository
{
    public class XMLTaskStatusRepository : ITaskStatusRepository
    {
        public Task<IEnumerable<TaskStatusModel>> GetAllStatuses()
        {
            throw new NotImplementedException();
        }
    }
}
