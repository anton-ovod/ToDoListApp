using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoListApplication.Models;
using ToDoListApplication.ViewModels;
using ToDoListApplication.Factory;
using Microsoft.Identity.Client.Extensions.Msal;

namespace ToDoListApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RepositoryFactory _repositoryFactory;
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger, RepositoryFactory repositoryFactory)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var storageType = _configuration["StorageType"];
            var repositoryStrategy = _repositoryFactory.CreateRepositoryStrategy(storageType);
            var taskRepository = repositoryStrategy.CreateTaskRepository();
            var categoryRepository = repositoryStrategy.CreateCategoryRepository();
            var statusRepository = repositoryStrategy.CreateTaskStatusRepository();

            var viewModel = new IndexViewModel();

            viewModel.Tasks = await taskRepository.GetAllTasks();
            viewModel.Categories = await categoryRepository.GetAllCategories();
            viewModel.Statuses = await statusRepository.GetAllStatuses();
            viewModel.StorageType = storageType;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ChangeStorageType(string storageType)
        {
            if(!string.IsNullOrEmpty(storageType))
            {
                _configuration["StorageType"] = storageType;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> InsertTask(IndexViewModel viewModel)
        {
            var storageType = _configuration["StorageType"];
            var repositoryStrategy = _repositoryFactory.CreateRepositoryStrategy(storageType);
            var taskRepository = repositoryStrategy.CreateTaskRepository();
            if (ModelState.IsValid)
            {
                await taskRepository.Insert(viewModel.Task);
                return RedirectToAction("Index");
            }
            var categoryRepository = repositoryStrategy.CreateCategoryRepository();
            var taskStatusRepository = repositoryStrategy.CreateTaskStatusRepository();

            viewModel.Tasks = await taskRepository.GetAllTasks();
            viewModel.Categories = await categoryRepository.GetAllCategories();
            viewModel.Statuses = await taskStatusRepository.GetAllStatuses();
            viewModel.StorageType = storageType;

            return View("Index", viewModel);
        }

        public async Task<IActionResult> ChangeStatus(TaskModel task)
        {
            task.TaskStatusID += 1;

            var storageType = _configuration["StorageType"];
            var repositoryStrategy = _repositoryFactory.CreateRepositoryStrategy(storageType);
            var taskRepository = repositoryStrategy.CreateTaskRepository();
            await taskRepository.Update(task);

            return RedirectToAction("Index", new { storageType });
        }

        public async Task<IActionResult> DeleteTask(TaskModel task)
        {
            var storageType = _configuration["StorageType"];
            var repositoryStrategy = _repositoryFactory.CreateRepositoryStrategy(storageType);
            var taskRepository = repositoryStrategy.CreateTaskRepository();
            await taskRepository.Delete(task);

            return RedirectToAction("Index", new { storageType });
        }

        public async Task<IActionResult> UpdateTask(TaskModel task)
        {
            var storageType = _configuration["StorageType"];
            var repositoryStrategy = _repositoryFactory.CreateRepositoryStrategy(storageType);
            var taskRepository = repositoryStrategy.CreateTaskRepository();
            await taskRepository.Update(task);

            return RedirectToAction("Index", new { storageType });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

