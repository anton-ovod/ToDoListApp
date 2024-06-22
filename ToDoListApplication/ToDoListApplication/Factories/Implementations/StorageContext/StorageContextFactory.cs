using Azure;
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
            var response = _httpContextAccessor.HttpContext?.Response;
            var request = _httpContextAccessor.HttpContext?.Request;

            var storageType = "SQL";

            if (request is not null &&
                !string.IsNullOrEmpty(request.Cookies["Storage-Type"]))
            {
                storageType = request.Cookies["Storage-Type"];
            }
            else response.Cookies.Append("Storage-Type", storageType);

            return storageType.ToString() switch
            {
                "SQL" => new DapperSQLContext(_serviceProvider.GetRequiredService<IConfiguration>()),
                "XML" => new XMLStorageContext(_serviceProvider.GetRequiredService<IConfiguration>()),
                _ => throw new ArgumentException("Invalid storage type", nameof(storageType))
            };
        }
    }
}
