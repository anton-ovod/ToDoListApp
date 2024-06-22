using System.Data;

namespace ToDoListApplication.StorageContext.Infrastructure
{
    public interface IDbStorageContext : IStorageContext
    {
        IDbConnection CreateConnection();
    }
}
