using System.ComponentModel.DataAnnotations;

namespace MusicStore.WebApp.Models
{
    public class ItemTypeViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "TypeReqired")]
        public string Type { get; set; }
    }
}