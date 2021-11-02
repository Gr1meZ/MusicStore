using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Data.Models;

namespace MusicStore.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task SubmitOrder(List<Order> orderList, string UserId);
        Task ChangeOrderStatus(int id);
        IQueryable<Order> GetUsersOrders(int id);
    }
}