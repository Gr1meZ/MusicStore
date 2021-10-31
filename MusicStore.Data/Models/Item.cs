using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Data.Models
{
    public class Item
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Required]
        public string  Name { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal  Price { get; set; }
        [DataType(DataType.Text)]
        [Required]
        public string Description { get; set; }
        public int TypeId { get; set; }
        public ItemType type { get; set; }

    }
}