using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Business.Service_Models;
using MusicStore.Data.Models;

namespace MusicStore.Business.Interfaces
{
    public interface IOrderService
    {
        IQueryable<UsersOrders> GetUsersOrders(string userId);
        Guid GetUniqueOrderId(int orderId);
        IEnumerable<Order> GetOrderDetails(Guid orderId);
        IQueryable<AnonymousOrders> FiltrateAnonymousOrders(string sortOrder, string searchString, OrderFilterType type);
        IQueryable<UsersOrders> FiltrateUsersOrders(string sortOrder, string searchString, OrderFilterType type);
        Task<dynamic> GetFirstOrder(int itemId, OrderType type);
        Task ChangeAnonymousOrderStatus(AnonymousOrders order, string email);
        Task ChangeUsersOrderStatus(UsersOrders order, string email);
    }
}