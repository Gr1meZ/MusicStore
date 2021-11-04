using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task Update(Item itemDTO)
        {
            Item item = await Get(itemDTO.Id);
            item.Name = itemDTO.Name;
            item.Price = itemDTO.Price;
            item.Description = itemDTO.Description;
            item.type = itemDTO.type;
            item.ImageName = itemDTO.ImageName;
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

        public IQueryable<ItemType> GetTypes()
        {
            return _context.ItemTypes;
        }

        public IQueryable<Item> GetBind()
        {
            return _context.Items.Include(t => t.type);
        }
    }
}