using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Data;
using MusicStore.Data.Models;

namespace MusicStore.Data.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<Order> OrderDetailsExtension(this ApplicationDbContext context, Guid orderIdDto)
        {
            //returns orders with mapped item table 
            return  context.Orders
                .Include(i => i.Item)
                .Include(i => i.Price)
                .Where(u => u.OrderId == orderIdDto);
        }
          
        }
    }

