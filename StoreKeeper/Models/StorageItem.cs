using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreKeeper.Models
{
    public class StorageItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
