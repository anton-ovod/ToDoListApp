using Microsoft.Data.SqlClient;
using System.Data;

using ToDoListApplication.StorageContext.Infrastructure;
namespace ToDoListApplication.StorageContext.Implementations.DbStorageContext
{
    public class DapperSQLContext : IDbStorageContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public DapperSQLContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DatabaseConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
