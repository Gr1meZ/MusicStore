using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Data.Models;

namespace MusicStore.Business.Interfaces
{
    public interface IItemService
    {
        IQueryable<Item> GetAllItems(string sortOrder, string searchString);
        IQueryable<Item> GetAllItems();
        IList<ItemType> GetAllTypes();
        Task CreateItem(Item itemDto);
        Task<Item> GetItem(int itemId);
        Task UpdateItem(Item itemDto);
        Task RemoveItem(int itemId);
    }
}