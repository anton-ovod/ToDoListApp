using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;
using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Repository.Implementations.SQLRepositories
{
    public class SQLTaskRepository : ITaskRepository
    {
        private readonly IDbStorageContext _storagecontext;

        public SQLTaskRepository(IDbStorageContext storagecontext)
        {
            _storagecontext = storagecontext;
        }

        public async Task DeleteById(Guid taskId)
        {
            try
            {
                var query = "DELETE FROM Task WHERE TaskID = @TaskID";

                using (var connection = _storagecontext.CreateConnection())
                {
                    await connection.ExecuteAsync(query, new { TaskID = taskId });
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("A database error occurred while deleting the task. Please try again later.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the task. Please try again later.", ex);
            }
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            try
            {
                var query = "SELECT TaskID, Title, Description, DueDate, TaskStatusID, TaskCategoryID FROM Task";

                using (var connection = _storagecontext.CreateConnection())
                {
                    var tasklist = await connection.QueryAsync<TaskModel>(query);
                    return tasklist.ToList();
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("A database error occurred while fetching tasks. Please try again later.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching tasks. Please try again later.", ex);
            }
        }

        public async Task<TaskModel> GetTaskById(Guid taskId)
        {
            try
            {
                var query = "SELECT TaskID, Title, Description, DueDate, TaskStatusID, TaskCategoryID FROM Task WHERE TaskID = @TaskID";

                using (var connection = _storagecontext.CreateConnection())
                {
                    return await connection.QueryFirstOrDefaultAsync<TaskModel>(query, new { TaskID = taskId });
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("A database error occurred while fetching the task. Please try again later.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the task. Please try again later.", ex);
            }
        }

        public async Task Insert(TaskModel task)
        {
            try
            {
                task.TaskID = Guid.NewGuid();
                var query = "INSERT INTO Task (TaskID, Title, Description, DueDate, TaskStatusID, TaskCategoryID)" +
                            " VALUES (@TaskID, @Title, @Description, @DueDate, @TaskStatusID, @TaskCategoryID)";

                using (var connection = _storagecontext.CreateConnection())
                {
                    await connection.ExecuteAsync(query, task);
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("A database error occurred while inserting the task. Please try again later.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting the task. Please try again later.", ex);
            }
        }

        public async Task Update(TaskModel task)
        {
            var query = "UPDATE Task " +
                        "SET Title=@Title, Description=@Description, DueDate=@DueDate, TaskStatusID=@TaskStatusID, TaskCategoryID=@TaskCategoryID " +
                        "WHERE TaskID=@TaskID";

            try
            {
                using (var connection = _storagecontext.CreateConnection())
                {
                    await connection.ExecuteAsync(query, task);
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Error updating task in database.", sqlEx);
            }
            catch (Exception ex) 
            {
                throw new Exception("Unexpected error updating task.", ex);
            }
        }
    }
}
