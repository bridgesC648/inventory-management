using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Database.Entities
{
 
    public class Location
    {
        [Key]
        public Guid LocationId { get; set; }

        [Required]
        [StringLength(20)]
        public string LocationName { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("^(YARN|CARPET)$", ErrorMessage = "ItemType must be 'YARN' or 'CARPET'.")]
        public string ItemType { get; set; }
    }
}
