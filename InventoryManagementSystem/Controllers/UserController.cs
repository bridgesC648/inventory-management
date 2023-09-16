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

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ViewBag.Username = username;
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}