using ToDoListApplication.Models;

namespace ToDoListApplication.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllCategories();
    }
}
