using ToDoListApplication.GraphQL.Mutations;
using ToDoListApplication.GraphQL.Queries;
using GraphQlTypes = GraphQL.Types;
namespace ToDoListApplication.GraphQL.Schema
{
    public class RootSchema : GraphQlTypes.Schema
    {
        public RootSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
            Mutation = provider.GetRequiredService<RootMutation>();
        }
    }
}
