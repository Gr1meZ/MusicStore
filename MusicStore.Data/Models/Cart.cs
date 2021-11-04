using System.ComponentModel.DataAnnotations;

namespace MusicStore.Data.Models
{
    public class Cart
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int PriceId { get; set; }
        public Item Price { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}