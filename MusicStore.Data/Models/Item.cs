using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        
    }
}