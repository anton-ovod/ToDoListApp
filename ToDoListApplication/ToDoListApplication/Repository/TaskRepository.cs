using Dapper;
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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            var query = "Select Title, Description, DueDate, TaskStatusID, TaskCategoryID from Task";
            using(var connection = _dbcontext.CreateConnection())
            {
                var tasklist = await connection.QueryAsync<TaskModel>(query);
                return tasklist.ToList();
            }
        }

        public async Task Insert(TaskModel task)
        {
            object taskToInsert = new { task.Title, task.Description, task.DueDate, task.TaskStatusID, task.TaskCategoryID};

            var query = "insert into Task (Title, Description, DueDate, TaskStatusID, TaskCategoryID)" +
                " values (@Title, @Description, @DueDate, @TaskStatusID, @TaskCategoryID)";

            using( var connection = _dbcontext.CreateConnection())
            {
                await connection.ExecuteAsync(query, taskToInsert);
            }
        }

        public Task Update(TaskModel task)
        {
            throw new NotImplementedException();
        }
    }
}
