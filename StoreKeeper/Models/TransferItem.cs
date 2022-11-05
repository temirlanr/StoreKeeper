using System.ComponentModel.DataAnnotations;

namespace StoreKeeper.Models
{
    public class TransferItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Transfer Transfer { get; set; }
        [Required]
        public int TransferId { get; set; }
    }
}
