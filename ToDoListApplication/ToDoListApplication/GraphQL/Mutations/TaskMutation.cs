using GraphQL;
using GraphQL.Types;
using ToDoListApplication.GraphQL.InputTypes;
using ToDoListApplication.GraphQL.Types;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;

namespace ToDoListApplication.GraphQL.Mutations
{
    public class TaskMutation : ObjectGraphType
    {
        public TaskMutation(ITaskRepository repo)
        {
            Field<StringGraphType>("addTask")
                .Argument<NonNullGraphType<TaskInputType>>("task")
                .ResolveAsync(async (context) =>
                {
                    try
                    {
                        var task = context.GetArgument<TaskModel>("task");
                        await repo.Insert(task);
                        return "Task has been successfully added";
                    }
                    catch (Exception ex)
                    {
                        throw new ExecutionError("An error occurred while adding the task. Please try again later.", ex);
                    }
                });

            Field<StringGraphType>("updateTask")
                .Arguments(
                    new QueryArguments(
                        new QueryArgument<NonNullGraphType<IdGraphType>>
                        {
                            Name = "id",
                            Description = "The ID of the task to update"
                        },
                        new QueryArgument<NonNullGraphType<TaskInputType>>
                        {
                            Name = "task",
                            Description = "The updated task data"
                        }
                    )
                )
                .ResolveAsync(async (context) =>
                {
                    try
                    {
                        var taskId = context.GetArgument<Guid>("id");
                        var updatedTask = context.GetArgument<TaskModel>("task");
                        updatedTask.TaskID = taskId;

                        await repo.Update(updatedTask);
                        return "Task updated successfully!";
                    }
                    catch (Exception ex)
                    {
                        return "Error updating task." + ex.Message;
                    }
                });

            Field<StringGraphType>("deleteTask").Argument<NonNullGraphType<IdGraphType>>("id")
    .ResolveAsync(async (context) =>
    {
        try
        {
            var id = context.GetArgument<Guid>("id");
            await repo.DeleteById(id);
            return $"Task with id = {id} has been successfully deleted";
        }
        catch (Exception ex)
        {
            return "Error deleting task." + ex.Message;
        }
    });

            Field<StringGraphType>("changeStatus").Argument<NonNullGraphType<IdGraphType>>("id")
    .ResolveAsync(async (context) =>
    {
        try
        {
            var id = context.GetArgument<Guid>("id");
            var task = await repo.GetTaskById(id);

            if (task == null)
            {
                context.Errors.Add(new ExecutionError("Task not found."));
                return null;
            }

            task.TaskStatusID = task.TaskStatusID < 2 ? task.TaskStatusID + 1 : task.TaskStatusID;
            await repo.Update(task);

            return $"Status of task with id {id} has been successfully updated";
        }
        catch (Exception ex)
        {
            return "Error while changing task status." + ex.Message;
        }
    });
        }
    }
}
