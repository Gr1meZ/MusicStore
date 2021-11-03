﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Data.Migrations;
using MusicStore.Data.Models;

namespace MusicStore.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task SubmitOrder(List<Order> orderList, string UserId);
        Task ChangeOrderStatus(UsersOrders order);
        Guid GetOrderId(int id);
        Task<UsersOrders> GetOrder(int id);
        IQueryable<UsersOrders> GetUsersOrders(string id);
        IQueryable<UsersOrders> GetUnproccessed();
        IQueryable<UsersOrders> GetLogs();
        IEnumerable<Order> OrderDetails(Guid OrderId);
    }
}