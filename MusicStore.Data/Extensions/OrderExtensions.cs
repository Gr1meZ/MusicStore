using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Models;
using X.PagedList;

namespace MusicStore.Data.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<Order> OrderDetailsExtension(this ApplicationDbContext context, Guid OrderIdDto)
        {
            return  context.Orders
                .Include(i => i.Item)
                .Include(i => i.Price)
                .Where(u => u.OrderId == OrderIdDto);
        }
          
        }
    }

