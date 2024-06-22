using GraphQL;
using GraphQL.Types;
using ToDoListApplication.GraphQL.Types;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;

namespace ToDoListApplication.GraphQL.Queries
{
    public class TaskQuery : ObjectGraphType
    {
        public TaskQuery(ITaskRepository repo) 
        {
            Field<ListGraphType<TaskType>>("tasks")
                .ResolveAsync(async _ => await repo.GetAllTasks());

           /* Field<TaskType>("task")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }))
                .ResolveAsync(async context =>
                {
                var id = context.GetArgument<Guid>("id");

                return await repo.GetById(id);
                });*/
        }
    }
}
