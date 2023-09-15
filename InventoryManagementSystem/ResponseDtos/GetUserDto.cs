using InventoryManagementSystem.Database.Entities;

namespace InventoryManagementSystem.ResponseDtos
{
    public class GetUserDto : Response
    {
        public User user { get; set; }
    }
}
