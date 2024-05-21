using System.Xml;
using ToDoListApplication.Models;
using ToDoListApplication.Models.Data;

namespace ToDoListApplication.Repository
{
    public class XMLCategoryRepository : ICategoryRepository
    {
        private readonly XMLStorageContext _xmlcontext;
        public XMLCategoryRepository(XMLStorageContext xmlcontext)
        {
            _xmlcontext = xmlcontext;
        }
        public Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            var categories = new List<CategoryModel>();

            // Load XML document
            XmlDocument doc = new XmlDocument();
            doc.Load(_xmlcontext.GetStoragePath());

            // Select all Category nodes using XPath
            XmlNodeList categoryNodes = doc.SelectNodes("/ToDoApplication/Categories/Category");

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
