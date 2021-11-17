using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Business.Interfaces;
using MusicStore.Business.Service_Models;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;

namespace MusicStore.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _order;

        public OrderService(IOrderRepository order)
        {
            _order = order;
        }

        public IQueryable<UsersOrders> GetUsersOrders(string userId)
        {
            return _order.GetUsersOrders(userId);
        }

        public Guid GetUniqueOrderId(int orderId)
        {
            return _order.GetOrderId(orderId);
        }

        public IEnumerable<Order> GetOrderDetails(Guid orderId)
        {
            return _order.OrderDetails(orderId);
        }
        //This method allows to get all anonymous orders and filters list of themselves by sorting and searching
        public IQueryable<AnonymousOrders> FiltrateAnonymousOrders(string sortOrder, string searchString, OrderFilterType type)
        {
            IQueryable<AnonymousOrders> orders = type == OrderFilterType.Logs ? _order.GetLogsAnonymous() : _order.GetUnproccessedAnonymous();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Date.ToString().Contains(searchString)
                                           || s.OrderId.ToString().Contains(searchString)
                                           || s.Email.Contains(searchString)
                                           || s.Id.ToString().Contains(searchString));
                
            }
            switch (sortOrder)
            {
                case "IdDesc":
                    orders = orders.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    orders = orders.OrderBy(s => s.Id);
                    break;
                case "EmailDesc":
                    orders = orders.OrderByDescending(s => s.Email);
                    break;
                case "Email":
                    orders = orders.OrderBy(s => s.Email);
                    break;
                case "OrderIdDesc":
                    orders = orders.OrderByDescending(s => s.OrderId);
                    break;
                case "OrderId":
                    orders = orders.OrderBy(s => s.OrderId);
                    break;
                case "StatusDesc":
                    orders = orders.OrderByDescending(s => s.Status);
                    break;
                case "Status":
                    orders = orders.OrderBy(s => s.Status);
                    break;
                default:
                    orders = orders.OrderBy(s => s.Date);
                    break;
            }
    
            return orders;
        }
        
        //This method allows to get all authorized orders and filters list of themselves by sorting and searching
        public IQueryable<UsersOrders> FiltrateUsersOrders(string sortOrder, string searchString, OrderFilterType type)
        {
            var orders =   type == OrderFilterType.Unproccessed ? _order.GetUnproccessed() : _order.GetLogs();
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Date.ToString().Contains(searchString)
                                           || s.OrderId.ToString().Contains(searchString)
                                           || s.User.Email.Contains(searchString)
                                           || s.Id.ToString().Contains(searchString));
                
            }
            switch (sortOrder)
            {
                case "IdDesc":
                    orders = orders.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    orders = orders.OrderBy(s => s.Id);
                    break;
                case "UserIdDesc":
                    orders = orders.OrderByDescending(s => s.UserId);
                    break;
                case "UserId":
                    orders = orders.OrderBy(s => s.UserId);
                    break;
                case "OrderIdDesc":
                    orders = orders.OrderByDescending(s => s.OrderId);
                    break;
                case "OrderId":
                    orders = orders.OrderBy(s => s.OrderId);
                    break;
                case "StatusDesc":
                    orders = orders.OrderByDescending(s => s.Status);
                    break;
                case "Status":
                    orders = orders.OrderBy(s => s.Status);
                    break;
                default:
                    orders = orders.OrderBy(s => s.Date);
                    break;
            }

            return orders;
        }

        public async Task<dynamic> GetFirstOrder(int itemId, OrderType type)
        {
            //If user is authorized than get authorized order 
            if (type == OrderType.Authorized)
            {
                var order = await _order.GetOrder(itemId);
                return order;
            }
            //otherwise get anonymous order
            else
            {
                var order = await _order.GetAnonymousOrder(itemId);
                return order;
            }
        }
        //changes status for anonymous order
        public async Task ChangeAnonymousOrderStatus(AnonymousOrders order, string email)
        {
            await _order.ChangeAnonymousOrderStatus(order);
            await SendEmail.Send(email, "Status change",
                $"Your order №{order.Id} has been changed to status {order.Status}");
        }
        //changes status for authorized order
        public async Task ChangeUsersOrderStatus(UsersOrders order, string email)
        {
            await _order.ChangeOrderStatus(order);
            await SendEmail.Send(email, "Status change",
                $"Your order №{order.Id} has been changed to status {order.Status}");
        }
    }
}