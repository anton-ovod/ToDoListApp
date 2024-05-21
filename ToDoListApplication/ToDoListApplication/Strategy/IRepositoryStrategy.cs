using ToDoListApplication.Repository;

namespace ToDoListApplication.Strategy
{
    public interface IRepositoryStrategy
    {
        ITaskRepository CreateTaskRepository();
        ICategoryRepository CreateCategoryRepository();
        ITaskStatusRepository CreateTaskStatusRepository();
    }
}
