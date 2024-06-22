using ToDoListApplication.Strategy;
using ToDoListApplication.Factories.Infrastructure;
using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Factories.Implementations.Repository
{
    public class StrategyFactory : IStrategyFactory
    {
        private readonly IStorageContext _storagecontext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StrategyFactory(IStorageContext storagecontext,
                               IHttpContextAccessor httpContextAccessor)
        {
            _storagecontext = storagecontext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IRepositoryStrategy CreateRepositoryStrategy()
        {
            var storageType = _httpContextAccessor.HttpContext?.Request.Cookies["Storage-Type"];

            return storageType switch
            {
                "XML" => new XMLRepositoryStrategy((IFileStorageContext)_storagecontext),
                "SQL" => new SQLRepositoryStrategy((IDbStorageContext)_storagecontext),
                _ => throw new ArgumentException("Invalid storage type", nameof(storageType))
            };
        }
    }
}
