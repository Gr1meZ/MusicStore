using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Data;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;

namespace MusicStore.Data.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemsRepository(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public async Task<Item> Get(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task Create(Item item)
        {
            await  _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Item itemDto)
        {
            Item item = await Get(itemDto.Id);
            item.Name = itemDto.Name;
            item.Price = itemDto.Price;
            item.Description = itemDto.Description;
            item.Type = itemDto.Type;
            item.ImageName = itemDto.ImageName;
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            Item item = await Get(id);

            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            
        }
        public  IQueryable<Item> GetAll()
        {
            return  _context.Items;
        }
        
        //get all items with relation to types
        public IQueryable<Item> GetBind()
        {
            return _context.Items.Include(t => t.Type);
        }
    }
}