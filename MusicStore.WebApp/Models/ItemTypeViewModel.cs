using System.ComponentModel.DataAnnotations;

namespace MusicStore.WebApp.Models
{
    public class ItemTypeViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}