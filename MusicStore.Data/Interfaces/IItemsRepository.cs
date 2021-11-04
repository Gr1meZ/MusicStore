using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using MusicStore.Data.Models;

namespace MusicStore.Data.Interfaces
{
    public interface IItemsRepository : IRepository<Item> 
    {
        IQueryable<ItemType> GetTypes();
       IQueryable<Item> GetBind();
    }
}