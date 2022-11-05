using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreKeeper.Models
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int FromStorageId { get; set; }
        public Storage FromStorage { get; set; }
        [Required]
        public int ToStorageId { get; set; }
        public Storage ToStorage { get; set; }
        public List<TransferItem> TransferItems { get; set; }
    }
}
