using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoListApplication.Models;
using ToDoListApplication.ViewModels;
using ToDoListApplication.Repository.Infrastructure;
using Newtonsoft.Json;

namespace ToDoListApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskStatusRepository _taskStatusRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ITaskRepository taskRepository, ITaskStatusRepository taskStatusRepository,
                              ICategoryRepository categoryRepository)
        {
            _taskRepository = taskRepository;
            _taskStatusRepository = taskStatusRepository;
            _categoryRepository = categoryRepository; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            viewModel.Tasks = await _taskRepository.GetAllTasks();
            viewModel.Statuses = await _taskStatusRepository.GetAllStatuses();
            viewModel.Categories = await _categoryRepository.GetAllCategories();
            viewModel.StorageType = Request.Cookies["Storage-Type"]?.ToString() ?? "SQL";

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ChangeStorageType(string storageType)
        {
            Response.Cookies.Append("Storage-Type", storageType);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> InsertTask(
    string CategoriesJson,
    string TasksJson,
    string StatusesJson,
    IndexViewModel viewModel)
        {
            viewModel.Categories = JsonConvert.DeserializeObject<List<CategoryModel>>(CategoriesJson);
            viewModel.Tasks = JsonConvert.DeserializeObject<List<TaskModel>>(TasksJson);
            viewModel.Statuses = JsonConvert.DeserializeObject<List<TaskStatusModel>>(StatusesJson);

            if (ModelState.IsValid)
            {
                await _taskRepository.Insert(viewModel.Task);
                return RedirectToAction("Index");
            }

            return View("Index", viewModel);
        }

        public async Task<IActionResult> ChangeStatus(TaskModel task)
        {
            task.TaskStatusID += 1;
            await _taskRepository.Update(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTask(TaskModel task)
        {
            await _taskRepository.Delete(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateTask(TaskModel task)
        {
            await _taskRepository.Update(task);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

