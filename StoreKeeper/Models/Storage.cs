using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreKeeper.Models
{
    public class Storage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<StorageItem> StorageItems { get; set; }
    }
}
