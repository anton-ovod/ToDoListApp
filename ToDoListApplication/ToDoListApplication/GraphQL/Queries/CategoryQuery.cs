using GraphQL.Types;
using ToDoListApplication.GraphQL.Types;
using ToDoListApplication.Repository.Infrastructure;

namespace ToDoListApplication.GraphQL.Queries
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(ICategoryRepository repo) 
        {
            Field<ListGraphType<CategoryType>>("categories")
                .ResolveAsync(async _ => await repo.GetAllCategories());
        }
    }
}
