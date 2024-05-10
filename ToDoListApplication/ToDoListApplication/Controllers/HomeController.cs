using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using ToDoListApplication.Models;
using ToDoListApplication.Repository;

namespace ToDoListApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskRepository _taskrepo;

        public HomeController(ILogger<HomeController> logger,
                              ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskrepo = taskRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tasklist = await _taskrepo.GetAllTasks();
            
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
