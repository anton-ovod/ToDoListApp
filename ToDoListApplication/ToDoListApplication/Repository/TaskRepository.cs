using ToDoListApplication.Models;
using ToDoListApplication.Models.Data;

namespace ToDoListApplication.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DapperDBContext _dbcontext;

        public TaskRepository(DapperDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Task<List<TaskModel>> GetAllTasks()
        {
            throw new NotImplementedException();
        }
    }
}
