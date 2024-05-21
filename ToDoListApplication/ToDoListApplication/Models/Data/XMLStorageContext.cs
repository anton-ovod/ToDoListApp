using Microsoft.Data.SqlClient;
using System.Data;

namespace ToDoListApplication.Models.Data
{
    public class XMLStorageContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _storagePath;

        public XMLStorageContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _storagePath = _configuration.GetConnectionString("XMLStoragePath");
        }

        public string GetStoragePath()
        {
            return _storagePath;
        }

    }
}
