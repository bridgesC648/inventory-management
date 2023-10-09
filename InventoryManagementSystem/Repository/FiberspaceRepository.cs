using InventoryManagementSystem.Database.Entities;
using InventoryManagementSystem.Database;
using InventoryManagementSystem.Repository.abstraction;
using InventoryManagementSystem.ResponseDtos;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repository
{
    public class FiberspaceRepository : IFiberspaceRepository
    {
        private readonly FiberspaceContext _context;
        public FiberspaceRepository(FiberspaceContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetAllActiveLocations()
        {
            List<Location> locations = new List<Location>();    
            try
            {
                var res = _context.Locations.Where(l => l.Active).ToList();
                locations = res;
            }
            catch(Exception e)
            {

            }
            return locations;
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

        public async Task<GetAllInventoryDto> GetAllItemsAtLocation(string LocationName)
        {

            GetAllInventoryDto result = new GetAllInventoryDto();
            result.InventoryList = new();
            try
            {
                result.InventoryList = _context.InventoryItems.Where(i => i.LocationName == LocationName).ToList();
                result.Success = true;
            }
            catch (Exception e)
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

        public async Task<GetInventoryByIdDto> GetItem(string itemSerialNumber)
        {
            GetInventoryByIdDto result = new();
            result.Item = new();
            try
            {
                result.Item = _context.InventoryItems.Where(i => i.ItemSerialNumber.Trim() == itemSerialNumber.Trim()).FirstOrDefault();
                if (result.Item != null)
                {
                    result.Success = true;
                }
               
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<Location> GetLocation(string locationName)
        {
            Location result = new();
            try
            {
                result = _context.Locations.Where(l => l.LocationName.Trim() == locationName.Trim()).FirstOrDefault();
            }
            catch (Exception e)
            {
            }
            return result;
        }

        public async Task<int> GetItemCountAtLocation(string locationName)
        {
            try
            {
                return await _context.InventoryItems.CountAsync(i => i.LocationName == locationName);
            }
            catch (Exception e)
            {
                return 0;
            }
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
        //processCycleSession: Takes an array of items in the format of the CycleHistory table, and saves everything from the current session
        public async Task<Response> ProcessCycleSession(List<CycleHistory> CycleItems)
        {
            Response result = new();
            try
            {
                foreach(var cycleItem in CycleItems)
                {
                    CycleHistory insert = new()
                    {
                        ItemSerialNumber = cycleItem.ItemSerialNumber,
                        ItemType = cycleItem.ItemType,
                        Comment = cycleItem.Comment,
                        Employee = cycleItem.Employee,
                        FoundInd = cycleItem.FoundInd,
                        LocationName = cycleItem.LocationName,
                        RelocateInd = cycleItem.RelocateInd,
                        CreateDateTime = DateTime.Now,
                    };
                    _context.Add(insert);
                }
                _context.SaveChanges();
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<int> GetUpdatedTodayCount()
        {
            try
            {
                DateTime today = DateTime.Today;

                return await _context.CycleHistories.CountAsync(ch =>
                    ch.CreateDateTime.HasValue && ch.CreateDateTime.Value.Date == today);
            }
            catch (Exception e)
            {
                return 0;
            }
        }



        public async Task<GetInventoryByIdDto> RelocateItem(InventoryItem item, string newLocationName)
        {
            GetInventoryByIdDto result = new();
            try
            {
               var editItem = _context.InventoryItems.Where(i => i.InventoryItemId == item.InventoryItemId).FirstOrDefault();
                if(editItem != null)
                {
                    editItem.LocationName = newLocationName;
                    _context.InventoryItems.Update(editItem);
                    _context.SaveChanges();
                }
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

        public async Task<GetInventoryByIdDto> UpdateItemStatusCode(InventoryItem item, string newStatusCode)
        {
            GetInventoryByIdDto result = new();
            try
            {
                var editItem = _context.InventoryItems.Where(i => i.InventoryItemId == item.InventoryItemId).FirstOrDefault();
                if (editItem != null)
                {
                    editItem.ItemStatusCode = newStatusCode;
                    _context.InventoryItems.Update(editItem);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
            }
            return result;
        }
    }
}
