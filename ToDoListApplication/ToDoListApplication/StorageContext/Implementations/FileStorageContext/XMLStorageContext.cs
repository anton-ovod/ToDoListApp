using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.StorageContext.Implementations.FileStorageContext
{
    public class XMLStorageContext : IFileStorageContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _storagePath;

        public XMLStorageContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _storagePath = _configuration.GetConnectionString("XMLStoragePath");
        }

        public string? GetStoragePath()
        {
            return _storagePath;
        }

    }
}
