﻿using Dapper;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;
using ToDoListApplication.StorageContext.Implementations.DbStorageContext;
using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Repository.Implementations.SQLRepositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly IDbStorageContext _storagecontext;
        public SQLCategoryRepository(IDbStorageContext storagecontext)
        {
            _storagecontext = storagecontext;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            try
            {
                var query = "Select TaskCategoryID, TaskCategoryName, Description from TaskCategory";
                using (var connection = _storagecontext.CreateConnection())
                {
                    var tasklist = await connection.QueryAsync<CategoryModel>(query);
                    return tasklist.ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new Exception($"Error fetching categories from database: {ex.Message}");
            }
        }
    }
}
