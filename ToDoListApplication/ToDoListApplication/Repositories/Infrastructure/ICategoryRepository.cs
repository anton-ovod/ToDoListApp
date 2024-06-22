using ToDoListApplication.Models;

namespace ToDoListApplication.Repository.Infrastructure
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllCategories();
    }
}
