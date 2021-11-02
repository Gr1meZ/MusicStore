using System.ComponentModel.DataAnnotations;

namespace MusicStore.Data.Models
{
    public class Cart
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item item { get; set; }
        public int priceId { get; set; }
        public Item price { get; set; }
        public string UserId { get; set; }
        public ApplicationUser user { get; set; }
    }
}