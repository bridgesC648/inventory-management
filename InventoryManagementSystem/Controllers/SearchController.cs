using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models.Search;
using InventoryManagementSystem.Repository.abstraction;
using InventoryManagementSystem.Database.Entities;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Controllers
{
    // [Authorize]
    public class SearchController : Controller
    {
        private readonly IFiberspaceRepository _repository;
        public SearchController(IFiberspaceRepository repo)
        {
            this._repository = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(SearchModel post)
        {
           /* InventoryItem item = new()
            {
                ItemType = post.Type
            };
            _repository.SearchInventoryItem()*/
            return Index();
        }
    }
}


