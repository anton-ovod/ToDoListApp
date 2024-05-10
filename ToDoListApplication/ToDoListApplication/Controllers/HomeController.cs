using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using ToDoListApplication.Models;
using ToDoListApplication.ViewModels;
using ToDoListApplication.Repository;

namespace ToDoListApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskRepository _taskrepo;
        private readonly ICategoryRepository _categoryrepo;

        public HomeController(ILogger<HomeController> logger,
                              ITaskRepository taskRepository,
                              ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _taskrepo = taskRepository;
            _categoryrepo = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            viewModel.Tasks = await _taskrepo.GetAllTasks();
            viewModel.Categories = await _categoryrepo.GetAllCategories();
            
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
