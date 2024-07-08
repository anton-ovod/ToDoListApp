using GraphQL.Types;

namespace ToDoListApplication.GraphQL.Mutations
{
    public class RootMutation: ObjectGraphType
    {
        public RootMutation(TaskMutation taskMutation)
        {
            AddField(taskMutation.GetField("addTask"));
            AddField(taskMutation.GetField("deleteTask"));
            AddField(taskMutation.GetField("updateTask"));
            AddField(taskMutation.GetField("changeStatus"));


        }
    }
}
