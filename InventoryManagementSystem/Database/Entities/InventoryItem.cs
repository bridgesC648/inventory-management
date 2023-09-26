using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Database.Entities;

public partial class InventoryItem
{
    public Guid InventoryItemId { get; set; }

    public string ItemType { get; set; } = null!;

    public string ItemSerialNumber { get; set; } = null!;

    public string ItemMasterDescription { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public string ItemStatusCode { get; set; } = null!;

    public decimal PrimaryQuantity { get; set; }

    public string PrimaryUom { get; set; } = null!;

}
