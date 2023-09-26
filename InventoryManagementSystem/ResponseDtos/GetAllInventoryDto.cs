using InventoryManagementSystem.Database.Entities;

namespace InventoryManagementSystem.ResponseDtos
{
    public class GetAllInventoryDto : Response
    {
        public List<InventoryItem> InventoryList { get; set; }
    }
}
