using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task SubmitOrder(List<Order> orderList, string UserId)
        {
            orderList.ForEach(i => _context.Orders.AddAsync(i));
            var order = orderList.First();
            var usersOrders = new UsersOrders();
            usersOrders.OrderId = order.Id;
            usersOrders.UserId = UserId;
            await _context.UsersOrders.AddAsync(usersOrders);
            await _context.SaveChangesAsync();
        }

     

        public Task ChangeOrderStatus(int id)
        {
            throw new System.NotImplementedException();
        }

        public  IQueryable<Order> GetUsersOrders(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}