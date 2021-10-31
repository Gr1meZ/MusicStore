using System.ComponentModel.DataAnnotations;

namespace MusicStore.Data.Models
{
    public class ItemType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}