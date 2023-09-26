using InventoryManagementSystem.Database.Entities;

namespace InventoryManagementSystem.ResponseDtos
{
    public class GetInventoryByIdDto: Response
    {
        public InventoryItem? Item { get; set; }
    }
}
