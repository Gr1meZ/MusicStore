using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddToCart(Cart cartDTO)
        {
            await  _context.Cart.AddAsync(cartDTO);
            
            await _context.SaveChangesAsync();
        }

        public  IQueryable<Cart> GetCart(string id)
        {
            
            return _context.GetCartExtension(id);
        }

        public Task<Cart> Submit(int id)
        {
            throw new System.NotImplementedException();
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


        public async Task RemoveRange(string userIdDto)
        {
            var list = _context.Cart.Where(i => i.UserId == userIdDto).ToList();
            _context.Cart.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}