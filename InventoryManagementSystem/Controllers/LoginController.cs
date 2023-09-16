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

        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(string username, string password)
        {
            var userResult = await _repository.GetUserByUsernameOrEmail(username);
            if (userResult.Success && userResult.user != null)
            {
                if (userResult.user.Password == password)
                {
                    HttpContext.Session.SetString("Username", userResult.user.Username);
                    return RedirectToAction("Index", "User", new { username = userResult.user.Username });
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }


        public IActionResult Index(string username)
        {
            ViewBag.Username = username;
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