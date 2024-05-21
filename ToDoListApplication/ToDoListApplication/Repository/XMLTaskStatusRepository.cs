using Microsoft.AspNetCore.Http;
using System.Xml;
using ToDoListApplication.Models;
using ToDoListApplication.Models.Data;

namespace ToDoListApplication.Repository
{
    public class XMLTaskStatusRepository : ITaskStatusRepository
    {
        private readonly XMLStorageContext _xmlcontext;
        public XMLTaskStatusRepository(XMLStorageContext xmlcontext) 
        {
            _xmlcontext = xmlcontext;
        }

        public Task<IEnumerable<TaskStatusModel>> GetAllStatuses()
        {
            var statuses = new List<TaskStatusModel>();

            // Load XML document
            XmlDocument doc = new XmlDocument();
            doc.Load(_xmlcontext.GetStoragePath());

            // Select all Status nodes using XPath
            XmlNodeList statusNodes = doc.SelectNodes("/ToDoApplication/Statuses/Status");

            foreach (XmlNode statusNode in statusNodes)
            {
                var status = new TaskStatusModel
                {
                    TaskStatusID = int.Parse(statusNode.SelectSingleNode("ID").InnerText),
                    TaskStatusName = statusNode.SelectSingleNode("Name").InnerText,
                    Description = statusNode.SelectSingleNode("Description").InnerText
                };
                statuses.Add(status);
            }

            return Task.FromResult(statuses.AsEnumerable());
        }
    }
}
