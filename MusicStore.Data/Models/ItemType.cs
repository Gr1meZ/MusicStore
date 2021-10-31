using System.ComponentModel.DataAnnotations;

namespace MusicStore.Data.Models
{
    public class ItemType
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Required]
        public string Type { get; set; }
    }
}