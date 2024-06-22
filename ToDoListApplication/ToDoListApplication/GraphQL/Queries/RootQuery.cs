using GraphQL.Types;


namespace ToDoListApplication.GraphQL.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(TaskQuery taskQuery, CategoryQuery categoryQuery,
                         TaskStatusQuery taskStatusQuery) 
        {
            AddField(taskQuery.GetField("tasks"));
            AddField(categoryQuery.GetField("categories"));
            AddField(taskStatusQuery.GetField("statuses"));

        }
    }
}
