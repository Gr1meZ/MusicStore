using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Data.Models;

namespace MusicStore.WebApp.Models
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public SelectList Types { get; set; }
        public string Type { get; set; }
    }
}