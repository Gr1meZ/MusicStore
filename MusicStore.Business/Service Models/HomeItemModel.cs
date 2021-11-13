using System.Collections.Generic;
using System.Linq;
using MusicStore.Data.Models;

namespace MusicStore.Business.Service_Models
{
    public class HomeItemModel
    {
        public IQueryable<Item> Items { get; set; }
        public List<ItemType> Types { get; set; }
    }
}