using ToDoListApplication.Models.Data;
using ToDoListApplication.Repository;

namespace ToDoListApplication.Strategy
{
    public class XMLRepositoryStrategy : IRepositoryStrategy
    {
        private readonly XMLStorageContext _xmlcontext;

        public XMLRepositoryStrategy(XMLStorageContext xmlcontext)
        {
            _xmlcontext = xmlcontext;
        }

        public ICategoryRepository CreateCategoryRepository()
        {
            return new XMLCategoryRepository(_xmlcontext);
        }

        public ITaskRepository CreateTaskRepository()
        {
            return new XMLTaskRepository(_xmlcontext);
        }

        public ITaskStatusRepository CreateTaskStatusRepository()
        {
            return new XMLTaskStatusRepository(_xmlcontext);
        }
    }
}
