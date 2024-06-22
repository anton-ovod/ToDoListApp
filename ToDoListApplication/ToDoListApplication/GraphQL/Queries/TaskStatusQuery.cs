using GraphQL.Types;
using ToDoListApplication.GraphQL.Types;
using ToDoListApplication.Repository.Infrastructure;

namespace ToDoListApplication.GraphQL.Queries
{
    public class TaskStatusQuery : ObjectGraphType
    {
        public TaskStatusQuery(ITaskStatusRepository repo) 
        {
            Field<ListGraphType<TaskStatusType>>("statuses")
                .ResolveAsync(async _ => await repo.GetAllStatuses());
        }
    }
}
