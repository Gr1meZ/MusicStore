using System;

namespace MusicStore.Data.Models
{
    public class AnonymousOrders
    {
        public int Id {get; set;}
        public string Email { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}