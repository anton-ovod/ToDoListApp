using GraphQL.Types;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;

namespace ToDoListApplication.GraphQL.Types
{
    public class CategoryType: ObjectGraphType<CategoryModel>
    {
        public CategoryType()
        {
            Field(x => x.TaskCategoryID, type: typeof(IdGraphType)).Description("Category ID.");
            Field(x => x.TaskCategoryName).Description("Category Name");
            Field(x => x.Description, nullable: true).Description("Category Description");

            Field<ListGraphType<TaskType>>("tasks")
                .ResolveAsync(async context =>
                {
                    var category = context.Source;
                    var tasks = await context.RequestServices.GetRequiredService<ITaskRepository>().GetAllTasks();

                    return tasks?.Where(task => task.TaskCategoryID == category.TaskCategoryID);
                }).Description("All tasks for specific status.");
        }
    }
}
