using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using InventoryManagementSystem.Database;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Repository.abstraction;
using InventoryManagementSystem.Database.Entities;



namespace InventoryManagementSystem.Areas.Identity.Pages.Account
{
    [Authorize] // Ensure only authenticated users can access this page
    public class AuthUserModel : PageModel
    {

        private readonly IFiberspaceRepository _repository;

        public AuthUserModel(IFiberspaceRepository repository)
        {
            this._repository = repository;
        }

        [BindProperty]
        public string Location { get; set; }
        [BindProperty]
        public string Item { get; set; }
        public List<string> Items { get; set; } = new List<string>();
        public int CurrentItemNumber { get; set; } = 1;
        public int TotalInventoryCount { get; set; } // To store total inventory count for the pie chart
        [BindProperty]
        public List<Dictionary<Guid, string>> LocationMapping { get; set; }
        [BindProperty]
        public List<Location> Locations { get; set; }
        public Dictionary<string, int> LocationItemCounts { get; set; } = new Dictionary<string, int>();
        public int UpdatedTodayCount { get; set; }




        public async Task OnGetAsync()
        {
            UpdatedTodayCount = await _repository.GetUpdatedTodayCount();
            Location = string.Empty;
            Item = string.Empty;
            Items = new List<string>();
            var inventory = await this._repository.GetAllInventory();
            var lo = await this._repository.GetAllActiveLocations();
            Locations = lo;
            LocationMapping = new();
            foreach (var l in Locations)
            {
                Dictionary<Guid, string> temp = new()
                {
                    {l.LocationId, l.LocationName }
                };
                LocationMapping.Add(temp);
             }
            TotalInventoryCount = inventory.InventoryList.Count; // Use the repository method to get the count
            foreach (var location in Locations)
            {
                var locationItemCount = await _repository.GetItemCountAtLocation(location.LocationName);
                LocationItemCounts[location.LocationName] = locationItemCount;
            }
        }

        public IActionResult OnPostSetLocation()
        {
            return Page();
        }

        public IActionResult OnPostProcessSession()
        {
            
            return Page();
        }

        public IActionResult OnPostReset()
        {
            Items.Clear();
            Location = string.Empty;

            return Page();
        }

        public IActionResult OnPostExit()
        {
            return RedirectToPage("/Index");
        }

        public void OnPostAddItem(string item, string location)
        {
            Items.Add($"{CurrentItemNumber}. {item} - {location}");
            CurrentItemNumber++;
        }

        public void OnPostSetLocation(int itemNumber, string location)
        {
            Items[itemNumber - 1] = $"{itemNumber}. {Items[itemNumber - 1].Split(" - ")[0]} - {location}";
        }

        public void OnPostDeleteItem(int itemNumber)
        {
            Items.RemoveAt(itemNumber - 1);
            for (int i = itemNumber - 1; i < Items.Count; i++)
            {
                Items[i] = $"{i + 1}. {Items[i].Split(" - ")[0]} - {Items[i].Split(" - ")[1]}";
            }
            CurrentItemNumber--;
        }
    }
}
