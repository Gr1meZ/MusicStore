using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MusicStore.Data.Models;

namespace MusicStore.WebApp.Models
{
    public class AnonymousViewModel
    {
        [EmailAddress]
        [Required]
         public string Email { get; set; }
        
    }
}