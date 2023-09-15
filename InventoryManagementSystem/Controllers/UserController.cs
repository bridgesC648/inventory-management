using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository.abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventoryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IFiberspaceRepository _repository;

        public UserController(ILogger<UserController> logger, IFiberspaceRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}