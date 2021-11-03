using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Extensions;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Migrations;
using MusicStore.Data.Models;

namespace MusicStore.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
          
        }
        public async Task SubmitOrder(List<Order> orderList, string UserId)
        {
            orderList.ForEach(i => _context.Orders.AddAsync(i));
            var order = orderList.First();
            var usersOrders = new UsersOrders();
            var Date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            usersOrders.OrderId = order.Id;
            usersOrders.UserId = UserId;
            usersOrders.status = OrderStatus.Sended;
            usersOrders.Date = Convert.ToDateTime(Date);
            await _context.UsersOrders.AddAsync(usersOrders);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeOrderStatus(UsersOrders orderDTO)
        {
            UsersOrders order = await GetOrder(orderDTO.id);
            order.status = orderDTO.status;
            _context.UsersOrders.Update(order);
            await _context.SaveChangesAsync();
        }


        public  Guid GetOrderId(int id)
        {
            var orderIdKey =  _context.Orders.First(i => i.Id == id);
            return orderIdKey.OrderId;
        }

        public async Task<UsersOrders> GetOrder(int id)
        {
            var orderIdKey =  await _context.UsersOrders.FirstAsync(i => i.id == id);
            return orderIdKey;
        }

        public  IQueryable<UsersOrders> GetUsersOrders(string id)
        {
          return _context.UsersOrders.Where(i => i.UserId == id);
            
        }

        public IQueryable<UsersOrders> GetUnproccessed()
        {
            return _context.UsersOrders
                .Include(i => i.User)
                .Where(i => i.status == OrderStatus.Sended || i.status == OrderStatus.Accepted);


        }

        public IQueryable<UsersOrders> GetLogs()
        {
            return _context.UsersOrders.Where( i =>
                i.status == OrderStatus.Finished);
        }

        public   IEnumerable<Order> OrderDetails(Guid OrderIdDto)
        {
           return _context.OrderDetailsExtension(OrderIdDto);
        }
    }
}