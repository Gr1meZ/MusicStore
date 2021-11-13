using System.Linq;
using MusicStore.Data.Models;

namespace MusicStore.Data.Interfaces
{
    public interface IItemsRepository : IRepository<Item> 
    {
        IQueryable<Item> GetBind();
    }
}