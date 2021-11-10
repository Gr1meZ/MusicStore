﻿using System;

namespace MusicStore.Data.Models
{
    public class UsersOrders
    {
        public int Id {get; set;}
        public string UserId { get; set; }
        public int OrderId { get; set; }
         public ApplicationUser User { get; set; }
        public Order Order { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}