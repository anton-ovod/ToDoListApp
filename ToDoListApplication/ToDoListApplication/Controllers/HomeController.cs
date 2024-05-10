using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> InsertTask(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newTask = new TaskModel(model.Title, model.Description, model.DueDate, model.TaskCategoryID);
                await _taskrepo.Insert(newTask);
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
