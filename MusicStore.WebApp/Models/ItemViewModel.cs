using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace MusicStore.WebApp.Models
{
    public class ItemViewModel
    {
        [Required]
        public int  Id { get; set; }
        [Required(ErrorMessage="NameRequired")]
        public string  Name { get; set; }
    
        [Required( ErrorMessage = "PriceRequired")]
        public decimal?  Price { get; set; }
        [Required(ErrorMessage="DescriptionRequired")]
        public string Description { get; set; }
        [Required]
        public int TypeId { get; set; }
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }
        
        public IEnumerable<ItemViewModel> Items {get; set;}
    }
}