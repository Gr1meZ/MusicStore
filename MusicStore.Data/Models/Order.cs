using System;


namespace MusicStore.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        
        public Item Item { get; set; }
        
        public int Count { get; set; }
        public int PriceId { get; set; }
        public Item Price {get; set; }
        public Guid OrderId { get; set; }
    }
}