using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MusicStore.Data.Models;

namespace MusicStore.WebApp.Models
{
    public class ItemViewModel
    {
        [Required]
        public int  Id { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public decimal  Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TypeId { get; set; }
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }
        
        public IEnumerable<ItemViewModel> Items {get; set;}
    }
}