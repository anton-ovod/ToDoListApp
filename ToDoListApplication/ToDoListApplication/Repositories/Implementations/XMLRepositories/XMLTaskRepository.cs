using System.IO.IsolatedStorage;
using System.Xml.Linq;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;
using ToDoListApplication.StorageContext.Implementations.FileStorageContext;
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

        public async Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            // Load XML document
            XDocument doc = XDocument.Load(_storagecontext.GetStoragePath());

            // Extract tasks from XML
            var tasks = doc.Descendants("Task")
                .Select(t => new TaskModel
                {
                    TaskID = int.Parse(t.Element("ID").Value),
                    Title = t.Element("Title").Value,
                    Description = t.Element("Description").Value,
                    DueDate = string.IsNullOrEmpty(t.Element("DueDate").Value) ? null : DateTime.Parse(t.Element("DueDate").Value),
                    TaskCategoryID = int.Parse(t.Element("CategoryID").Value),
                    TaskStatusID = int.Parse(t.Element("StatusID").Value)
                });

            return tasks;
        }

        public async Task Insert(TaskModel task)
        {
            // Load XML document
            XDocument doc = XDocument.Load(_storagecontext.GetStoragePath());

            // Increment the ID for the new task
            task.TaskID = Guid.NewGuid().GetHashCode();
            // Add new task to XML
            XElement newTask = new XElement("Task",
                new XElement("ID", task.TaskID),
                new XElement("Title", task.Title),
                new XElement("Description", task.Description),
                new XElement("DueDate", task.DueDate.ToString()),
                new XElement("CategoryID", task.TaskCategoryID),
                new XElement("StatusID", task.TaskStatusID));

            doc.Element("ToDoApplication").Element("Tasks").Add(newTask);

            // Save changes to XML file
            doc.Save(_storagecontext.GetStoragePath());
        }

        public async Task Update(TaskModel task)
        {
            // Load XML document
            XDocument doc = XDocument.Load(_storagecontext.GetStoragePath());

            // Find task node by ID
            XElement? taskToUpdate = doc.Descendants("Task")
                .SingleOrDefault(t => int.Parse(t.Element("ID").Value) == task.TaskID);

            if (taskToUpdate != null)
            {
                // Update task properties
                taskToUpdate.Element("Title").Value = task.Title;
                taskToUpdate.Element("Description").Value = task.Description;
                taskToUpdate.Element("CategoryID").Value = task.TaskCategoryID.ToString();
                taskToUpdate.Element("DueDate").Value = task.DueDate.ToString();
                taskToUpdate.Element("StatusID").Value = task.TaskStatusID.ToString();

                // Save changes to XML file
                doc.Save(_storagecontext.GetStoragePath());
            }
        }

        public async Task Delete(TaskModel task)
        {
            // Load XML document
            XDocument doc = XDocument.Load(_storagecontext.GetStoragePath());

            // Find task node by ID
            XElement? taskToDelete = doc.Descendants("Task")
                .SingleOrDefault(t => int.Parse(t.Element("ID").Value) == task.TaskID);

            // Remove task node
            taskToDelete?.Remove();

            // Save changes to XML file
            doc.Save(_storagecontext.GetStoragePath());
        }
    }
}
