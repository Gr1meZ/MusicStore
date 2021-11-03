using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Data.Models
{
    public class UsersOrders
    {
        [Key]
        public int id {get; set;}
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public ApplicationUser User { get; set; }
        public Order Order { get; set; }
        public OrderStatus status { get; set; }
        public DateTime Date { get; set; }
    }
}