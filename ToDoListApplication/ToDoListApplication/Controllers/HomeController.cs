using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoListApplication.Models;
using ToDoListApplication.ViewModels;
using ToDoListApplication.Repository;
using System.Reflection.Metadata.Ecma335;

namespace ToDoListApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskRepository _taskrepo;
        private readonly ICategoryRepository _categoryrepo;
        private readonly ITaskStatusRepository _taskstatusrepo;

        public HomeController(ILogger<HomeController> logger,
                              ITaskRepository taskRepository,
                              ICategoryRepository categoryRepository,
                              ITaskStatusRepository taskStatusRepository)
        {
            _logger = logger;
            _taskrepo = taskRepository;
            _categoryrepo = categoryRepository;
            _taskstatusrepo = taskStatusRepository;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            viewModel.Tasks = await _taskrepo.GetAllTasks();
            viewModel.Categories = await _categoryrepo.GetAllCategories();
            viewModel.Statuses = await _taskstatusrepo.GetAllStatuses();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTask(IndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _taskrepo.Insert(viewModel.Task);
                return RedirectToAction("Index");
            }
            viewModel.Categories = await _categoryrepo.GetAllCategories();
            viewModel.Tasks = await _taskrepo.GetAllTasks();
            viewModel.Statuses = await _taskstatusrepo.GetAllStatuses();

            return View("Index", viewModel);
        }


        public async Task<IActionResult> ChangeStatus(TaskModel task)
        {
            task.TaskStatusID += 1;
            await _taskrepo.Update(task);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTask(TaskModel task)
        {
            await _taskrepo.Delete(task);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateTask(TaskModel task)
        {
            await _taskrepo.Update(task);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
