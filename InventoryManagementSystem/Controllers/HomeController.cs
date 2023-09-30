using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository.abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFiberspaceRepository _repository;

        public HomeController(ILogger<HomeController> logger, IFiberspaceRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            //var inventory = _repository.GetAllInventory();
            return Redirect("/Identity/Account/AuthUser");
        }
        [HttpGet]
        public IActionResult UserLogin()
        {

            return Redirect("/Identity/Account/Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetLocation(string Name)
        {
            var res = _repository.GetAllActiveLocations();
            var filteredLoc = res.Result.Where(l => l.ItemType.Trim().ToLower() == Name.Trim().ToLower());
            string selectOptions = "";
            foreach (var location in filteredLoc)
            {
                selectOptions += $"<option value='{location.LocationName}'>{location.LocationName}</option>";
            }
            return Ok(new {selectOptions });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}