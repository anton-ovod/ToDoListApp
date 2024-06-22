using ToDoListApplication.Repository.Infrastructure;
using ToDoListApplication.Repository.Implementations.XMLRepositories;
using ToDoListApplication.StorageContext.Infrastructure;
namespace ToDoListApplication.Strategy
{
    public class XMLRepositoryStrategy : IRepositoryStrategy
    {
        private readonly IFileStorageContext _storagecontext;

        public XMLRepositoryStrategy(IFileStorageContext storagecontext)
        {
            _storagecontext = storagecontext;
        }

        public ICategoryRepository CreateCategoryRepository()
        {
            return new XMLCategoryRepository(_storagecontext);
        }

        public ITaskRepository CreateTaskRepository()
        {
            return new XMLTaskRepository(_storagecontext);
        }

        public ITaskStatusRepository CreateTaskStatusRepository()
        {
            return new XMLTaskStatusRepository(_storagecontext);
        }
    }
}
