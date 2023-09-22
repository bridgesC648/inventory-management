using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models.Search;
using InventoryManagementSystem.Repository.abstraction;

using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Database;
using System.Collections.Generic;

namespace InventoryManagementSystem.Controllers
{
    // [Authorize]
    public class SearchController : Controller
    {
        private readonly FiberspaceContext _context; // Replace with your DbContext
        public SearchController(FiberspaceContext context)
        {
            _context = context; // Replace with your DbContext
        }

        public ActionResult Index()
        {
            var items = _context.InventoryItems.ToList(); // Retrieve data from the database
            return View(items);
        }
    }
}
