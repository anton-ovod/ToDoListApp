using Dapper;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;
using ToDoListApplication.StorageContext.Implementations.DbStorageContext;
using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Repository.Implementations.SQLRepositories
{
    public class SQLTaskStatusRepository : ITaskStatusRepository
    {
        private readonly IDbStorageContext _storagecontext;
        public SQLTaskStatusRepository(IDbStorageContext storagecontext)
        {
            _storagecontext = storagecontext;
        }

        public async Task<IEnumerable<TaskStatusModel>> GetAllStatuses()
        {
            var query = "Select TaskStatusID, TaskStatusName, Description from TaskStatus";

            using (var connection = _storagecontext.CreateConnection())
            {
                var statusesList = await connection.QueryAsync<TaskStatusModel>(query);
                return statusesList.ToList();
            }
        }
    }
}
