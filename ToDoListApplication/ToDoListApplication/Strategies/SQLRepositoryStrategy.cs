using ToDoListApplication.Repository.Infrastructure;
using ToDoListApplication.Repository.Implementations.SQLRepositories;
using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Strategy
{
    public class SQLRepositoryStrategy : IRepositoryStrategy
    {
        private readonly IDbStorageContext _storagecontext;

        public SQLRepositoryStrategy(IDbStorageContext storagecontext)
        {
            _storagecontext = storagecontext;
        }

        public ICategoryRepository CreateCategoryRepository()
        {
            return new SQLCategoryRepository(_storagecontext);
        }

        public ITaskRepository CreateTaskRepository()
        {
            return new SQLTaskRepository(_storagecontext);
        }

        public ITaskStatusRepository CreateTaskStatusRepository()
        {
            return new SQLTaskStatusRepository(_storagecontext);
        }
    }
}
