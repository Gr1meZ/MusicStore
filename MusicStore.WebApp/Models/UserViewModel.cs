using System.ComponentModel.DataAnnotations;

namespace MusicStore.WebApp.Models
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        
    }
}