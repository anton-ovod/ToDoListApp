using System.Xml.Linq;
using ToDoListApplication.Models;
using ToDoListApplication.Models.Data;

namespace ToDoListApplication.Repository
{
    public class XMLTaskRepository : ITaskRepository
    {
        private readonly XMLStorageContext _xmlcontext;
        public XMLTaskRepository(XMLStorageContext xmlcontext)
        {
            _xmlcontext = xmlcontext;
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            // Load XML document
            XDocument doc = XDocument.Load(_xmlcontext.GetStoragePath());

            // Extract tasks from XML
            var tasks = doc.Descendants("Task")
                .Select(t => new TaskModel
                {
                    TaskID = int.Parse(t.Element("ID").Value),
                    Title = t.Element("Title").Value,
                    Description = t.Element("Description").Value,
                    TaskCategoryID = int.Parse(t.Element("CategoryID").Value),
                    TaskStatusID = int.Parse(t.Element("StatusID").Value)
                });

            return tasks;
        }

        public async Task Insert(TaskModel task)
        {
            // Load XML document
            XDocument doc = XDocument.Load(_xmlcontext.GetStoragePath());

            // Add new task to XML
            XElement newTask = new XElement("Task",
                new XElement("ID", task.TaskID),
                new XElement("Title", task.Title),
                new XElement("Description", task.Description),
                new XElement("CategoryID", task.TaskCategoryID),
                new XElement("StatusID", task.TaskStatusID));

            doc.Element("ToDoApplication").Element("Tasks").Add(newTask);

            // Save changes to XML file
            doc.Save(_xmlcontext.GetStoragePath());
        }

        public async Task Update(TaskModel task)
        {
            // Load XML document
            XDocument doc = XDocument.Load(_xmlcontext.GetStoragePath());

            // Find task node by ID
            XElement taskToUpdate = doc.Descendants("Task")
                .SingleOrDefault(t => int.Parse(t.Element("ID").Value) == task.TaskID);

            if (taskToUpdate != null)
            {
                // Update task properties
                taskToUpdate.Element("Title").Value = task.Title;
                taskToUpdate.Element("Description").Value = task.Description;
                taskToUpdate.Element("CategoryID").Value = task.TaskCategoryID.ToString();
                taskToUpdate.Element("StatusID").Value = task.TaskStatusID.ToString();

                // Save changes to XML file
                doc.Save(_xmlcontext.GetStoragePath());
            }
        }

        public async Task Delete(TaskModel task)
        {
            // Load XML document
            XDocument doc = XDocument.Load(_xmlcontext.GetStoragePath());

            // Find task node by ID
            XElement taskToDelete = doc.Descendants("Task")
                .SingleOrDefault(t => int.Parse(t.Element("ID").Value) == task.TaskID);

            // Remove task node
            taskToDelete?.Remove();

            // Save changes to XML file
            doc.Save(_xmlcontext.GetStoragePath());
        }
    }
}
