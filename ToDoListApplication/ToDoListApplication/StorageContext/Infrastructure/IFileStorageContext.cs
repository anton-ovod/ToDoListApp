namespace ToDoListApplication.StorageContext.Infrastructure
{
    public interface IFileStorageContext : IStorageContext
    {
        string? GetStoragePath();
    }
}
