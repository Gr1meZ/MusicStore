using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Data;
using MusicStore.Data.Models;

namespace MusicStore.Data.Extensions
{
    public static class CartExtensions
    {
        public static IQueryable<Cart> GetCartExtension(this ApplicationDbContext context, string id)
        {
            return context.Cart
                .Include(i => i.Price)
                .Include(i => i.Item)
                .Where(u => u.UserId == id);
        }
       
    }
}

