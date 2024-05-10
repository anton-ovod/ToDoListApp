﻿using Dapper;
using ToDoListApplication.Models;
using ToDoListApplication.Models.Data;

namespace ToDoListApplication.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperDBContext _dbcontext;
        public CategoryRepository(DapperDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            var query = "Select TaskCategoryID, TaskCategoryName, Description from TaskCategory";
            using (var connection = _dbcontext.CreateConnection())
            {
                var tasklist = await connection.QueryAsync<CategoryModel>(query);
                return tasklist.ToList();
            }
        }
    }
}
