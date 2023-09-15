using InventoryManagementSystem.Database.Entities;
using InventoryManagementSystem.Database;
using InventoryManagementSystem.Repository.abstraction;
using InventoryManagementSystem.ResponseDtos;

namespace InventoryManagementSystem.Repository
{
    public class FiberspaceRepository : IFiberspaceRepository
    {
        private readonly FiberspaceContext _context;
        public FiberspaceRepository(FiberspaceContext context)
        {
            _context = context;
        }

        public async Task<GetAllInventoryDto> GetAllInventory()
        {
            GetAllInventoryDto result = new GetAllInventoryDto();
            result.InventoryList = new();
            try
            {
                result.InventoryList = _context.InventoryItems.ToList();
                result.Success = true;
            }
            catch(Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<GetInventoryByIdDto> GetInventoryById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserDto> GetUser(int id)
        {
            throw new NotImplementedException();
        }

    }
}
