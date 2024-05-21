using ToDoListApplication.Models.Data;
using ToDoListApplication.Repository;

namespace ToDoListApplication.Strategy
{
    public class DBRepositoryStrategy : IRepositoryStrategy
    {
        private readonly DapperDBContext _dbContext;

        public DBRepositoryStrategy(DapperDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICategoryRepository CreateCategoryRepository()
        {
            return new DBCategoryRepository(_dbContext);
        }

        public ITaskRepository CreateTaskRepository()
        {
            return new DBTaskRepository(_dbContext);
        }

        public ITaskStatusRepository CreateTaskStatusRepository()
        {
            return new DBTaskStatusRepository(_dbContext);
        }
    }
}
