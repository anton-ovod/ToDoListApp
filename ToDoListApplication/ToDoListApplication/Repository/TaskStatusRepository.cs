using Dapper;
using ToDoListApplication.Models;
using ToDoListApplication.Models.Data;

namespace ToDoListApplication.Repository
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly DapperDBContext _dbcontext;
        public TaskStatusRepository(DapperDBContext dbcontext) 
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<TaskStatusModel>> GetAllStatuses()
        {
            var query = "Select TaskStatusID, TaskStatusName, Description from TaskStatus";

            using(var connection = _dbcontext.CreateConnection()) 
            {
                var statusesList = await connection.QueryAsync<TaskStatusModel>(query);
                return statusesList.ToList();
            }
        }
    }
}
