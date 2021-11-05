﻿using System.Collections.Generic;
using System.Linq;
using MusicStore.Data.Models;
using X.PagedList;

namespace MusicStore.WebApp.Models
{
    public class IndexViewModel
    {
        public IPagedList<Item> Items {get; set;}
        public PaginatedList<Cart> Cart {get; set;}
        public PaginatedList<ItemType> Types {get; set;}
        public IPagedList<Order> Orders {get; set;}
        public IPagedList<UsersOrders> BootstrapUsersOrders {get; set;}
        public PaginatedList<UsersOrders> UsersOrders {get; set;}
    }
}