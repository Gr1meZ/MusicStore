using System.Linq;
using MusicStore.Data.Models;

namespace MusicStore.Data.Interfaces
{
    public interface IItemsRepository : IRepository<Item> 
    {
        IQueryable<ItemType> GetTypes();
       IQueryable<Item> GetBind();
    }
}