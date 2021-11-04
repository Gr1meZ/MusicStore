using System.Linq;
using System.Threading.Tasks;
using MusicStore.Data.Data;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;

namespace MusicStore.Data.Repositories
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ItemType> Get(int id)
        {
            return await _context.ItemTypes.FindAsync(id);
        }

        public async Task Create(ItemType typeDto)
        {
            await  _context.ItemTypes.AddAsync(typeDto);
             await _context.SaveChangesAsync();
        }

        public async Task Update(ItemType typeDto)
        {
            ItemType type = await Get(typeDto.Id);
            type.Type = typeDto.Type;
            _context.ItemTypes.Update(type);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            ItemType type = await Get(id);

            if (type != null)
            {
                _context.ItemTypes.Remove(type);
                await _context.SaveChangesAsync();
            }
            
        }
        public  IQueryable<ItemType> GetAll()
        {
            return  _context.ItemTypes;
        }
    }
}