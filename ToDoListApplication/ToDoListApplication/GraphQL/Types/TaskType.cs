using GraphQL.Reflection;
using GraphQL.Types;
using ToDoListApplication.Models;

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
            Field(x => x.TaskCategoryID, nullable: true).Description("The category of the Task");
        }
    }
}
