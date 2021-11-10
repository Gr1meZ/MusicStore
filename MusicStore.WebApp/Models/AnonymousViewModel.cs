using System.ComponentModel.DataAnnotations;

namespace MusicStore.WebApp.Models
{
    public class AnonymousViewModel
    {
        [EmailAddress]
        [Required]
         public string Email { get; set; }
        
    }
}