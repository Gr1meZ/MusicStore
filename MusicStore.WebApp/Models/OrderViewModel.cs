using System.Collections.Generic;
using MusicStore.Data.Models;

namespace MusicStore.WebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public OrderStatus Status { get; set; }
        public string UserId { get; set; }
        public int orderId { get; set; }
    }
}