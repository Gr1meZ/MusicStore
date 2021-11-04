using System;
using System.ComponentModel.DataAnnotations;


namespace MusicStore.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        
        public int PriceId { get; set; }
        public Item Price {get; set; }
        public Guid OrderId { get; set; }
    }
}