using GraphQL.Types;
using Microsoft.IdentityModel.Protocols;

namespace ToDoListApplication.GraphQL.InputTypes
{
    public class TaskInputType: InputObjectGraphType
    {
        public TaskInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("description");
            Field<DateTimeGraphType>("dueDate");
            Field<IntGraphType>("taskCategoryID");
        }
    }
}
