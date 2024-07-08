using ToDoListApplication.Factories.Infrastructure;
using ToDoListApplication.StorageContext.Implementations.DbStorageContext;
using ToDoListApplication.StorageContext.Implementations.FileStorageContext;
using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Factories.Implementations.StorageContext
{
    public class StorageContextFactory : IStorageContextFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public StorageContextFactory(IHttpContextAccessor httpContextAccessor,
                                     IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        public IStorageContext GetStorageContext()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null.");
            }

            //var storageType = httpContext.Items["Storage-Type"];
            var storageType = httpContext.Request.Headers["Storage-Type"].ToString();

            return storageType switch
            {
                "SQL" => _serviceProvider.GetRequiredService<DapperSQLContext>(),
                "XML" => _serviceProvider.GetRequiredService<XMLStorageContext>(),
                _ => throw new ArgumentException("Invalid storage type", nameof(storageType))
            };
        }
    }
}
