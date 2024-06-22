using ToDoListApplication.Strategy;

namespace ToDoListApplication.Factories.Infrastructure
{
    public interface IStrategyFactory
    {
        IRepositoryStrategy CreateRepositoryStrategy();
    }
}
