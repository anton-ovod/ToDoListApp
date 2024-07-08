using GraphQL.Types;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;

namespace ToDoListApplication.GraphQL.Types
{
    public class TaskType : ObjectGraphType<TaskModel>
    {
        public TaskType() 
        {
            Field(x => x.TaskID, type: typeof(IdGraphType)).Description("The ID of the Task.");
            Field(x => x.Title).Description("The title of the Task.");
            Field(x => x.Description, nullable: true).Description("The description of the Task.");
            Field(x => x.TaskStatusID).Description("The completion status of the Task.");
            Field(x => x.DueDate, nullable: true).Description("The due date of the Task.");
            Field(x => x.TaskCategoryID, nullable: true).Description("The category id of the Task");

            Field<CategoryType>("category")
                .ResolveAsync(async context =>
                {
                    var task = context.Source;
                    var categories = await context.RequestServices.GetRequiredService<ICategoryRepository>().GetAllCategories();

                    return categories?.FirstOrDefault(category => category.TaskCategoryID == task.TaskCategoryID);
                }).Description("Category detailed data.");


            Field<TaskStatusType>("status")
                .ResolveAsync(async context =>
                {
                    var task = context.Source;
                    var statuses = await context.RequestServices.GetRequiredService<ITaskStatusRepository>().GetAllStatuses();

                    return statuses?.FirstOrDefault(status => status.TaskStatusID == task.TaskStatusID);
                }).Description("Status detailed data.");
        }
    }
}
