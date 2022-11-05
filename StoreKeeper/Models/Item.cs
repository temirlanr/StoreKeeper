using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreKeeper.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public List<StorageItem> StorageItems { get; set; }
        [JsonIgnore]
        public List<TransferItem> TransferItems { get; set; }
    }
}
