using System.Collections.Generic;
using MusicStore.Data.Models;
using X.PagedList;

namespace MusicStore.WebApp.Models
{
    public class IndexViewModel
    {
        public IPagedList<Item> Items {get; set;}
        public List<Cart> Cart {get; set;}
        public PaginatedList<ItemType> Types {get; set;}
        public IPagedList<Order> Orders {get; set;}
        public IPagedList<UsersOrders> BootstrapUsersOrders {get; set;}
        public PaginatedList<UsersOrders> UsersOrders {get; set;}
        public List<int> ItemsQuantities { get; set; }
    }
}