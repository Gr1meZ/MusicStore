using System.Linq;
using System.Threading.Tasks;
using MusicStore.Data.Models;

namespace MusicStore.Business.Interfaces
{
    public interface IItemTypeService
    {
        IQueryable<ItemType> GetAllTypes();
        Task CreateType(ItemType typeDto);
        Task<ItemType> GetType(int typeId);
        Task UpdateType(ItemType typeDto);
        Task RemoveType(int typeId);
    }
}