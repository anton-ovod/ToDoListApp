using ToDoListApplication.Models;

namespace ToDoListApplication.Repository
{
    public class XMLCategoryRepository : ICategoryRepository
    {
        public Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            throw new NotImplementedException();
        }
    }
}
