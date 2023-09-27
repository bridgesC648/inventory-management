using System;

namespace InventoryManagementSystem.Database.Entities
{
    public class CycleHistory
    {
        public Guid Id { get; set; }
        public string ItemSerialNumber { get; set; }
        public string ItemType { get; set; }
        public string LocationName { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Employee { get; set; }
        public string Comment { get; set; }
        public bool RelocateInd { get; set; }
        public bool FoundInd { get; set; }
    
    }
}
