using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models.Search;
using InventoryManagementSystem.Repository.abstraction;
using InventoryManagementSystem.Database.Entities;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Database;
using SQLitePCL;

namespace InventoryManagementSystem.Controllers
{
    // [Authorize]
    public class SearchController : Controller
    {
        private readonly FiberspaceContext _context;
        public SearchController(FiberspaceContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShowHistory( string Location, string ItemType, DateTime? StartDate, DateTime? EndDate)
        {
            var history = _context.CycleHistories.Where(ch => ch.ItemType == ItemType && ch.LocationName == Location &&
                         ch.CreateDateTime >= StartDate && ch.CreateDateTime <= EndDate)
                    .ToList();
            List<CycleHistory> temp = new();
            foreach(var item in history)
            {
                if(item.Comment.Contains(','))
                {
                    item.Comment.Replace(',', ':');
                }
                temp.Add(item);

            }

            return PartialView("_CycleHistoryPartial", temp);
            
        }
    }
}


