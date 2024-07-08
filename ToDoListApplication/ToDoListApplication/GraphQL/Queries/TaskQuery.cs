using GraphQL;
using GraphQL.Types;
using ToDoListApplication.GraphQL.Types;
using ToDoListApplication.Repository.Infrastructure;

namespace ToDoListApplication.GraphQL.Queries
{
    public class TaskQuery : ObjectGraphType
    {
        public TaskQuery(ITaskRepository repo) 
        {
            Field<ListGraphType<TaskType>>("tasks")
                .ResolveAsync(async _ => await repo.GetAllTasks());
        }
    }
}
