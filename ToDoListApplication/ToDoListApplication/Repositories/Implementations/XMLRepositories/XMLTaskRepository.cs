using System.Xml.Linq;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;
using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Repository.Implementations.XMLRepositories
{
    public class XMLTaskRepository : ITaskRepository
    {
        private readonly IFileStorageContext _storagecontext;
        public XMLTaskRepository(IFileStorageContext storagecontext)
        {
            _storagecontext = storagecontext;
        }

        
        public async Task Insert(TaskModel task)
        {
            await Task.Run(() =>
            {
                try
                {
                    // Load XML document
                    XDocument doc = XDocument.Load(_storagecontext.GetStoragePath());

                    // Assign a new Guid to the task
                    task.TaskID = Guid.NewGuid();

                    // Add new task to XML
                    XElement newTask = new XElement("Task",
                        new XElement("ID", task.TaskID),
                        new XElement("Title", task.Title),
                        new XElement("Description", task.Description),
                        new XElement("DueDate", task.DueDate?.ToString()),
                        new XElement("CategoryID", task.TaskCategoryID),
                        new XElement("StatusID", task.TaskStatusID));

                    doc.Element("ToDoApplication").Element("Tasks").Add(newTask);

                    doc.Save(_storagecontext.GetStoragePath());
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while inserting the task. Please try again later.", ex);
                }
            });
        }

        public async Task Update(TaskModel task)
        {
            await Task.Run(() =>
            {
                // Load XML document
                XDocument doc = XDocument.Load(_storagecontext.GetStoragePath());

                // Find task node by ID
                XElement? taskToUpdate = doc.Descendants("Task")
                    .SingleOrDefault(t => Guid.Parse(t.Element("ID").Value) == task.TaskID);

                if (taskToUpdate != null)
                {
                    // Update task properties
                    taskToUpdate.Element("Title").Value = task.Title;
                    taskToUpdate.Element("Description").Value = string.IsNullOrEmpty(task.Description) ? "" : task.Description;
                    taskToUpdate.Element("CategoryID").Value = task.TaskCategoryID.ToString();
                    taskToUpdate.Element("DueDate").Value = task.DueDate?.ToString() ?? "";
                    taskToUpdate.Element("StatusID").Value = task.TaskStatusID.ToString();

                    // Save changes to XML file
                    doc.Save(_storagecontext.GetStoragePath());
                }
                else
                {
                    throw new Exception("Task to update not found.");
                }
            });
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            try
            {
                return await Task.Run(() =>
                {
                    // Load XML document
                    XDocument doc = XDocument.Load(_storagecontext.GetStoragePath());

                    // Extract tasks from XML
                    var tasks = doc.Descendants("Task")
                            .Select(t => new TaskModel
                            {
                                TaskID = Guid.Parse(t.Element("ID").Value),
                                Title = t.Element("Title").Value,
                                Description = t.Element("Description").Value,
                                DueDate = string.IsNullOrEmpty(t.Element("DueDate").Value) ? null : DateTime.Parse(t.Element("DueDate").Value),
                                TaskCategoryID = string.IsNullOrEmpty(t.Element("CategoryID").Value) ? null : int.Parse(t.Element("CategoryID").Value),
                                TaskStatusID = int.Parse(t.Element("StatusID").Value)
                            })
                            .ToList();

                    return tasks;
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching tasks. Please try again later.", ex);
            }
        }

        public async Task DeleteById(Guid taskId)
        {
            try
            {
                await Task.Run(() =>
                {
                    // Load XML document
                    XDocument doc = XDocument.Load(_storagecontext.GetStoragePath());

                    // Find task node by ID
                    XElement? taskToDelete = doc.Descendants("Task")
                        .SingleOrDefault(t => Guid.Parse(t.Element("ID").Value) == taskId);

                    if (taskToDelete == null)
                    {
                        throw new Exception("Task not found");
                    }

                    // Remove task node
                    taskToDelete.Remove();

                    // Save changes to XML file
                    doc.Save(_storagecontext.GetStoragePath());
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the task. Please try again later.", ex);
            }
        }
        public async Task<TaskModel> GetTaskById(Guid taskId)
        {
            try
            {
                return await Task.Run(() =>
                {
                    XDocument doc = XDocument.Load(_storagecontext.GetStoragePath());

                    // Convert the taskId (Guid) to a string representation
                    string taskIdStr = taskId.ToString();

                    var taskElement = doc.Descendants("Task")
                        .FirstOrDefault(t => t.Element("ID")?.Value == taskIdStr);

                    if (taskElement == null)
                    {
                        return null;
                    }

                    var task = new TaskModel
                    {
                        TaskID = Guid.Parse(taskElement.Element("ID").Value),
                        Title = taskElement.Element("Title").Value,
                        Description = taskElement.Element("Description").Value,
                        DueDate = string.IsNullOrEmpty(taskElement.Element("DueDate").Value) ? null : DateTime.Parse(taskElement.Element("DueDate").Value),
                        TaskCategoryID = string.IsNullOrEmpty(taskElement.Element("CategoryID").Value) ? null : int.Parse(taskElement.Element("CategoryID").Value),
                        TaskStatusID = int.Parse(taskElement.Element("StatusID").Value)
                    };

                    return task;
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the task. Please try again later.", ex);
            }
        }
    }
}
