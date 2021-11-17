﻿
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Data.Data;
using MusicStore.Data.Extensions;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;

namespace MusicStore.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public async Task AddToCart(Cart cartDto)
        {
            await  _context.Cart.AddAsync(cartDto);
            
            await _context.SaveChangesAsync();
        }

        public  IQueryable<Cart> GetCart(string id)
        {
            return _context.GetCartExtension(id);
        }

    

        public async Task Remove(int id)
        {
            Cart cart =  await _context.Cart.FindAsync(id);

            if (cart != null)
            {
                _context.Cart.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        //removes all items in user's cart
        public async Task RemoveRange(string userIdDto)
        {
            var list = _context.Cart.Where(i => i.UserId == userIdDto).ToList();
            _context.Cart.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}