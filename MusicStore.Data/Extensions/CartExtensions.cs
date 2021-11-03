using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Models;

namespace MusicStore.Data.Extensions
{
    public static class CartExtensions
    {
        public static IQueryable<Cart> GetCartExtension(this ApplicationDbContext context, string id)
        {
            return context.Cart
                .Include(i => i.price)
                .Include(i => i.item)
                .Where(u => u.UserId == id);
        }
       
    }
}

