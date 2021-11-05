

using Microsoft.AspNetCore.Http;

namespace MusicStore.Data.Models
{
    public class Item
    {

        public int Id { get; set; }
        public string  Name { get; set; }
        public decimal  Price { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public ItemType Type { get; set; }
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}