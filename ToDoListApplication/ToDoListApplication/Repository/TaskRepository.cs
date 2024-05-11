﻿using Dapper;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
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

        public async Task Delete(TaskModel task)
        {
            var query = "Delete From Task Where TaskID=@TaskID";

            using(var connection = _dbcontext.CreateConnection()) 
            {
                await connection.ExecuteAsync(query, task);
            }
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            var query = "Select TaskID, Title, Description, DueDate, TaskStatusID, TaskCategoryID from Task";
            using(var connection = _dbcontext.CreateConnection())
            {
                var tasklist = await connection.QueryAsync<TaskModel>(query);
                return tasklist.ToList();
            }
        }

        public async Task Insert(TaskModel task)
        {
            var query = "insert into Task (Title, Description, DueDate, TaskStatusID, TaskCategoryID)" +
                " values (@Title, @Description, @DueDate, @TaskStatusID, @TaskCategoryID)";

            using( var connection = _dbcontext.CreateConnection())
            {
                await connection.ExecuteAsync(query, task);
            }
        }

        public async Task Update(TaskModel task)
        {
            var query = "UPDATE Task "+
                        "SET Title=@Title, Description=@Description, DueDate=@DueDate, TaskStatusID=@TaskStatusID, TaskCategoryID=@TaskCategoryID "+
                        "WHERE TaskID=@TaskID";

            using(var connection = _dbcontext.CreateConnection())
            {
                await connection.ExecuteAsync(query, task);
            }

        }
    }
}
