using ToDoListApplication.Models.Data;
using ToDoListApplication.Strategy;

namespace ToDoListApplication.Factory
{
    public class RepositoryFactory
    {
        private readonly DapperDBContext _dbContext;
        private readonly XMLStorageContext _xmlcontext;

        public RepositoryFactory(DapperDBContext dbContext, XMLStorageContext xmlcontext)
        {
            _dbContext = dbContext;
            _xmlcontext = xmlcontext;
        }

        public IRepositoryStrategy CreateRepositoryStrategy(string storageType)
        {
            return storageType switch
            {
                "XML" => new XMLRepositoryStrategy(_xmlcontext),
                "Database" => new DBRepositoryStrategy(_dbContext),
                _ => throw new ArgumentException("Invalid storage type", nameof(storageType))
            };
        }
    }
}
