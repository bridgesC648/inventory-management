using InventoryManagementSystem.Database.Entities;
using InventoryManagementSystem.ResponseDtos;

namespace InventoryManagementSystem.Repository.abstraction
{
    public interface IFiberspaceRepository
    {
        Task<GetAllInventoryDto> GetAllInventory();
        Task<GetInventoryByIdDto> GetInventoryById(Guid id);
        Task<GetAllInventoryDto> SearchInventoryItem(InventoryItem item);
        Task<GetUserDto> GetUser(int id);
        Task<GetUserDto> GetUserByUsernameOrEmail(string usernameOrEmail);
        Task<Location> GetLocation(string locationName);
        Task<GetInventoryByIdDto> GetItem(string itemSerialNumber);
        Task<GetInventoryByIdDto> RelocateItem(InventoryItem item, string newLocationName);
        Task<GetInventoryByIdDto> UpdateItemStatusCode(InventoryItem item, String newStatusCode);
        Task<GetUserDto> ProcessCycleSession();
        Task<GetAllInventoryDto> GetAllItemsAtLocation(String LocationName);

       

    }
}
