using GraphQL.Types;
using Microsoft.IdentityModel.Protocols;
using ToDoListApplication.Models;

namespace ToDoListApplication.GraphQL.Types
{
    public class TaskStatusType : ObjectGraphType<TaskStatusModel>
    {
        public TaskStatusType() 
        {
            Field(x => x.TaskStatusID, type: typeof(IdGraphType)).Description("Task Status ID.");
            Field(x => x.TaskStatusName).Description("Task Status Name");
            Field(x => x.Description, nullable: true).Description("Task Status Description");
        }

    }
}
