using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Factories.Infrastructure
{
    public interface IStorageContextFactory
    {
        IStorageContext GetStorageContext();
    }
}
