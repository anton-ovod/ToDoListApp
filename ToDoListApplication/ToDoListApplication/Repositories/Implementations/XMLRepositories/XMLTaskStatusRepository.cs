﻿using System.Xml;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;
using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Repository.Implementations.XMLRepositories
{
    public class XMLTaskStatusRepository : ITaskStatusRepository
    {
        private readonly IFileStorageContext _storagecontext;
        public XMLTaskStatusRepository(IFileStorageContext storagecontext)
        {
            _storagecontext = storagecontext;
        }

        public Task<IEnumerable<TaskStatusModel>> GetAllStatuses()
        {
            var statuses = new List<TaskStatusModel>();

            try
            {
                // Load XML document
                XmlDocument doc = new XmlDocument();
                doc.Load(_storagecontext.GetStoragePath());

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
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new Exception($"Error parsing XML and fetching task statuses: {ex.Message}");
            }
        }
    }
}
