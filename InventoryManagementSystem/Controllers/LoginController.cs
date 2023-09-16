using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository.abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventoryManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IFiberspaceRepository _repository;

        public LoginController(ILogger<LoginController> logger, IFiberspaceRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult UserLogin()
        {
            ViewBag.Email = TempData["Email"];
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            TempData["Email"] = email;
            return RedirectToAction("EmailSent", new { email = email });
        }

        public IActionResult EmailSent(string email)
        {
            TempData["Email"] = email;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}