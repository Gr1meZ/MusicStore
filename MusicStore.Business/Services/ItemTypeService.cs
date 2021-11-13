using System.Linq;
using System.Threading.Tasks;
using MusicStore.Business.Interfaces;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;

namespace MusicStore.Business.Services
{
    public class ItemTypeService : IItemTypeService
    {
        private readonly IItemTypeRepository _type;

        public ItemTypeService(IItemTypeRepository type)
        {
            _type = type;
        }

        public  IQueryable<ItemType> GetAllTypes()
        {
            return _type.GetAll();
        }

        public async Task CreateType(ItemType typeDto)
        {
            await _type.Create(typeDto);
        }
        
        public async Task<ItemType> GetType(int typeId)
        {
            return await _type.Get(typeId);
        }
        
        public async Task UpdateType(ItemType typeDto)
        {
            await _type.Update(typeDto);
        }

        public async Task RemoveType(int typeId)
        {
           await _type.Remove(typeId);
        }
        
    }
}