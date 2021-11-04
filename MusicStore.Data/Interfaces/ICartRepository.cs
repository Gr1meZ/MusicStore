using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Data.Models;

namespace MusicStore.Data.Interfaces
{
    public interface ICartRepository
    {
        Task AddToCart(Cart cartDTO);
        IQueryable<Cart> GetCart(string id);
        Task Remove(int id);
        Task RemoveRange(string userIdDto);
        
    }
}