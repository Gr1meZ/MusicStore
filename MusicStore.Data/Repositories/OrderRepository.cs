using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Data;
using MusicStore.Data.Extensions;
using MusicStore.Data.Interfaces;
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
        //submits order for authorized user
        public async Task SubmitOrder(List<Order> orderList, string userId)
        {
                //add each order of list to database 
                orderList.ForEach(i => _context.Orders.AddAsync(i));
                await _context.SaveChangesAsync();
                var orders =  _context.Orders.Where(i => i.OrderId == orderList.First().OrderId);
                var order = orders.First();
                var usersOrders = new UsersOrders();
                var date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                usersOrders.OrderId = order.Id;
                usersOrders.UserId = userId;
                usersOrders.Status = OrderStatus.Sended;
                usersOrders.Date = DateTime.ParseExact(date, "MM/dd/yyyy HH:mm:ss", null);
                await _context.UsersOrders.AddAsync(usersOrders);
                await _context.SaveChangesAsync();
          
        }

        public async Task<Order> GetSessionOrder(List<Order> orderList)
        {
            orderList.ForEach(i => _context.Orders.AddAsync(i));
            await _context.SaveChangesAsync();
            return await _context.Orders.FirstOrDefaultAsync(key => key.OrderId == orderList.First().OrderId);
        }
        public async Task<int> SubmitAnonymousOrder(List<Order> orderList, string email)
        {
            var order = await GetSessionOrder(orderList);
            var anonymousOrders = new AnonymousOrders();
            var date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            anonymousOrders.OrderId = order.Id;
            anonymousOrders.Email = email;
            anonymousOrders.Status = OrderStatus.Sended;
            anonymousOrders.Date = DateTime.ParseExact(date, "MM/dd/yyyy HH:mm:ss", null);
            await _context.AnonymousOrders.AddAsync(anonymousOrders);
            await _context.SaveChangesAsync();
            return order.Id;
        }
        public async Task ChangeOrderStatus(UsersOrders orderDto)
        {
            UsersOrders order = await GetOrder(orderDto.Id);
            order.Status = orderDto.Status;
            _context.UsersOrders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task ChangeAnonymousOrderStatus(AnonymousOrders orderDto)
        {
            AnonymousOrders order = await GetAnonymousOrder(orderDto.Id);
            order.Status = orderDto.Status;
            _context.AnonymousOrders.Update(order);
            await _context.SaveChangesAsync();
        }


        public  Guid GetOrderId(int id)
        {
            var orderIdKey =  _context.Orders.First(i => i.Id == id);
            return orderIdKey.OrderId;
        }

        public async Task<UsersOrders> GetOrder(int id)
        {
            var order =  await _context.UsersOrders.FirstAsync(i => i.Id == id);
            return order;
        }
        public async Task<AnonymousOrders> GetAnonymousOrder(int id)
        {
            var orderIdKey =  await _context.AnonymousOrders.FirstAsync(i => i.Id == id);
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
                .Where(i => i.Status == OrderStatus.Sended || i.Status == OrderStatus.Accepted);


        }
        public IQueryable<AnonymousOrders> GetUnproccessedAnonymous()
        {
            return _context.AnonymousOrders
                .Where(i => i.Status == OrderStatus.Sended || i.Status == OrderStatus.Accepted);
        }
        //get all authorized orders where status is equal to finish
        public IQueryable<UsersOrders> GetLogs()
        {
            return _context.UsersOrders
                .Include(i => i.User)
                .Where( i =>
                i.Status == OrderStatus.Finished);
        }
        //get all anonymous orders where status is equal to finish
        public IQueryable<AnonymousOrders> GetLogsAnonymous()
        {
            return _context.AnonymousOrders
                .Where( i =>
                    i.Status == OrderStatus.Finished);
        }

        public   IEnumerable<Order> OrderDetails(Guid orderIdDto)
        {
           return _context.OrderDetailsExtension(orderIdDto);
        }
    }
}