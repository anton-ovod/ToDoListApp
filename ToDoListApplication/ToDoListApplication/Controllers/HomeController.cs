using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoListApplication.Models;
using ToDoListApplication.ViewModels;
using ToDoListApplication.Repository.Infrastructure;
using Newtonsoft.Json;
using ToDoListApplication.Enums;
using ToDoListApplication.Providers;

namespace ToDoListApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskStatusRepository _taskStatusRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly StorageTypeProvider _storageTypeProvider;

        public HomeController(ITaskRepository taskRepository, ITaskStatusRepository taskStatusRepository,
                              ICategoryRepository categoryRepository,
                              StorageTypeProvider storageTypeProvider)
        {
            _taskRepository = taskRepository;
            _taskStatusRepository = taskStatusRepository;
            _categoryRepository = categoryRepository; 
            _storageTypeProvider = storageTypeProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            viewModel.Tasks = await _taskRepository.GetAllTasks();
            viewModel.Statuses = await _taskStatusRepository.GetAllStatuses();
            viewModel.Categories = await _categoryRepository.GetAllCategories();
            viewModel.CurrentStorageType = HttpContext.Items["Storage-Type"]?.ToString() ?? "SQL";
            viewModel.StorageTypes = Enum.GetNames(typeof(StorageType)).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ChangeStorageType(string storageType)
        {
            _storageTypeProvider.StorageType = storageType;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> InsertTask(string CategoriesJson,
                                                    string TasksJson,
                                                    string StatusesJson,
                                                    string StorageTypesJson,
                                                    IndexViewModel viewModel)
        {
            viewModel.Categories = JsonConvert.DeserializeObject<List<CategoryModel>>(CategoriesJson);
            viewModel.Tasks = JsonConvert.DeserializeObject<List<TaskModel>>(TasksJson);
            viewModel.Statuses = JsonConvert.DeserializeObject<List<TaskStatusModel>>(StatusesJson);
            viewModel.StorageTypes = JsonConvert.DeserializeObject<List<string>>(StorageTypesJson);

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
            await _taskRepository.DeleteById(task.TaskID);
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

