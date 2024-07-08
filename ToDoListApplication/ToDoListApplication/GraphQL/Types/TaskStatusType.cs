using GraphQL.Types;
using Microsoft.IdentityModel.Protocols;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;

namespace ToDoListApplication.GraphQL.Types
{
    public class TaskStatusType : ObjectGraphType<TaskStatusModel>
    {
        public TaskStatusType() 
        {
            Field(x => x.TaskStatusID, type: typeof(IdGraphType)).Description("Task Status ID.");
            Field(x => x.TaskStatusName).Description("Task Status Name");
            Field(x => x.Description, nullable: true).Description("Task Status Description");

            Field<ListGraphType<TaskType>>("tasks")
                .ResolveAsync(async context =>
                {
                    var status = context.Source;
                    var tasks = await context.RequestServices.GetRequiredService<ITaskRepository>().GetAllTasks();

                    return tasks?.Where( task => task.TaskStatusID == status.TaskStatusID);
                }).Description("All tasks for specific status.");
        }

    }
}
