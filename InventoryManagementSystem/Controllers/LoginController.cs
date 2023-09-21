using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository.abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

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
        public IActionResult Index()
        {
            return RedirectToAction("UserLogin", "Login");
        }

        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(string username, string password, string selectedItemType)
        {
            var userResult = await _repository.GetUserByUsernameOrEmail(username);
            if (userResult.Success && userResult.user != null)
            {
                if (userResult.user.Password == password)
                {
                    HttpContext.Session.SetString("Username", userResult.user.Username);
                    HttpContext.Session.SetString("SelectedItemType", selectedItemType);
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
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("briangomezsre@gmail.com", "xcxsfronmifljpbx"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("briangomezsre@gmail.com"),
                    Subject = "Reset Your Password",
                    Body = "<p>Click the link below to reset your password</p><p><a href='https://google.com" + "'>Reset Password</a></p>",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);

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