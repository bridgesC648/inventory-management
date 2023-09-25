using InventoryManagementSystem.Database.Entities;
using InventoryManagementSystem.Database;
using InventoryManagementSystem.Repository.abstraction;
using InventoryManagementSystem.ResponseDtos;
using LinqKit;

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
                var test = _context.Locations.ToList();
                result.InventoryList = _context.InventoryItems.ToList();
                result.Success = true;
            }
            catch(Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<GetInventoryByIdDto> GetInventoryById(Guid id)
        {
            GetInventoryByIdDto result = new();
            result.Item = new();
            try
            {
                result.Item = _context.InventoryItems.Where(i => i.InventoryItemId == id).FirstOrDefault();
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<GetUserDto> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserDto> GetUserByUsernameOrEmail(string usernameOrEmail)
        {
            GetUserDto result = new();
            try
            {
                result.user = _context.Users.FirstOrDefault(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
                result.Success = result.user != null;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
            }
            return result;
        }

        public async Task<GetAllInventoryDto> SearchInventoryItem(InventoryItem item)
        {
            GetAllInventoryDto result = new();
            result.InventoryList = new();
         
            try
            {
                var inventoryList = _context.InventoryItems.ToList();
                if (item.ItemSerialNumber != null)
                {
                    inventoryList = inventoryList.Where(i => i.ItemSerialNumber.ToLower().Contains(item.ItemSerialNumber.ToLower())).ToList();
                }
                if (item.ItemStatusCode != null)
                {
                    inventoryList = inventoryList.Where(i => i.ItemStatusCode.ToLower().Contains(item.ItemStatusCode.ToLower())).ToList();
                }
                if (item.ItemMasterDescription != null)
                {
                    inventoryList = inventoryList.Where(i => i.ItemMasterDescription.ToLower().Contains(item.ItemMasterDescription.ToLower())).ToList();
                }
                if (item.LocationName != null)
                {
                    inventoryList = inventoryList.Where(i => i.LocationName.ToLower().Contains(i.LocationName.ToLower())).ToList();
                }
                if (item.ItemType != null)
                {
                    inventoryList = inventoryList.Where(i => i.ItemType.ToLower().Contains(i.ItemType.ToLower())).ToList();
                }
                result.InventoryList = inventoryList;
                result.Success = true;
                
            }
            catch(Exception e)
            {
                result.Success=false;
                result.Message = e.Message;
            }
            return result;
        }
    }
}
