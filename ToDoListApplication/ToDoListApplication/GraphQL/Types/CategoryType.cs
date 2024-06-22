using GraphQL.Types;
using ToDoListApplication.Models;

namespace ToDoListApplication.GraphQL.Types
{
    public class CategoryType: ObjectGraphType<CategoryModel>
    {
        public CategoryType()
        {
            Field(x => x.TaskCategoryID, type: typeof(IdGraphType)).Description("Category ID.");
            Field(x => x.TaskCategoryName).Description("Category Name");
            Field(x => x.Description, nullable: true).Description("Category Description");
        }
    }
}
