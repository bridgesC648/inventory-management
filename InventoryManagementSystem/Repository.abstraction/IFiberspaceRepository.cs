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

    }
}
