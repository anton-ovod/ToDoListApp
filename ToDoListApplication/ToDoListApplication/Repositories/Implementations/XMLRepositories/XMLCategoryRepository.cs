using System.Xml;
using ToDoListApplication.Models;
using ToDoListApplication.Repository.Infrastructure;
using ToDoListApplication.StorageContext.Implementations.FileStorageContext;
using ToDoListApplication.StorageContext.Infrastructure;

namespace ToDoListApplication.Repository.Implementations.XMLRepositories
{
    public class XMLCategoryRepository : ICategoryRepository
    {
        private readonly IFileStorageContext _storagecontext;
        public XMLCategoryRepository(IFileStorageContext storagecontext)
        {
            _storagecontext = storagecontext;
        }
        public Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            var categories = new List<CategoryModel>();

            // Load XML document
            XmlDocument doc = new XmlDocument();
            doc.Load(_storagecontext.GetStoragePath());

            // Select all Category nodes using XPath
            XmlNodeList? categoryNodes = doc.SelectNodes("/ToDoApplication/Categories/Category");

            foreach (XmlNode categoryNode in categoryNodes)
            {
                var category = new CategoryModel
                {
                    TaskCategoryID = int.Parse(categoryNode.SelectSingleNode("ID").InnerText),
                    TaskCategoryName = categoryNode.SelectSingleNode("Name").InnerText,
                    Description = categoryNode.SelectSingleNode("Description").InnerText
                };
                categories.Add(category);
            }

            return Task.FromResult(categories.AsEnumerable());
        }
    }
}
