using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;

namespace MusicStore.Data.Repositories
{
    public class ItemsRepository : IItems
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

        public async Task<Item> Create(Item item)
        {
            await  _context.Items.AddAsync(item);
            item.Id = await _context.SaveChangesAsync();
            return item;
        }

        public async Task Update(Item itemDTO)
        {
            Item item = await Get(itemDTO.Id);
            item.Name = itemDTO.Name;
            item.Price = itemDTO.Price;
            item.Description = itemDTO.Description;
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
    }
}