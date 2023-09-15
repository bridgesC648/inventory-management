using InventoryManagementSystem.Database.Entities;
using InventoryManagementSystem.ResponseDtos;

namespace InventoryManagementSystem.Repository.abstraction
{
    public interface IFiberspaceRepository
    {
        Task<GetAllInventoryDto> GetAllInventory();
        Task<GetInventoryByIdDto> GetInventoryById(int id);
        Task<GetUserDto> GetUser(int id);
        

    }
}
