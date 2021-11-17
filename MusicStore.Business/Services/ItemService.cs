using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Business.Interfaces;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;

namespace MusicStore.Business.Services 
{
    public class ItemService : IItemService
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly IItemTypeRepository _itemsTypeRepository;

        public ItemService(IItemsRepository item, IItemTypeRepository itemsTypeRepository)
        {
            _itemsRepository = item;
            _itemsTypeRepository = itemsTypeRepository;
        }
        //This method allows to get all items and filters items list by sort orders and search string params
        public IQueryable<Item> GetAllItems(string sortOrder,string searchString)
        {
            var items =   _itemsRepository.GetBind();
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString)
                                         || s.Price.ToString().Contains(searchString)
                                         || s.Type.Type.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    items = items.OrderByDescending(s => s.Price);
                    break;
                case "Description":
                    items = items.OrderBy(s => s.Description);
                    break;
                case "Description_desc":
                    items = items.OrderByDescending(s => s.Description);
                    break;
                case "Type":
                    items = items.OrderBy(s => s.Type.Type);
                    break;
                case "Type_desc":
                    items = items.OrderByDescending(s => s.Type.Type);
                    break;
                case "Id":
                    items = items.OrderBy(s => s.Id);
                    break;
                case "Id_desc":
                    items = items.OrderByDescending(s => s.Id);
                    break;
                default:
                    items = items.OrderBy(s => s.Name);
                    break;
            }

            return items;
        }
        //overloading method that returns items with relations to type
        public IQueryable<Item> GetAllItems()
        {
            return _itemsRepository.GetBind();
        }
        public  IList<ItemType> GetAllTypes()
        {
            return _itemsTypeRepository.GetAll().ToList();
        }
        

        public async Task CreateItem(Item itemDto)
        {
            await _itemsRepository.Create(itemDto);
        }

        public async Task<Item> GetItem(int itemId)
        {
            return await _itemsRepository.Get(itemId);
        }

        public async Task UpdateItem(Item itemDto)
        {
            await _itemsRepository.Update(itemDto);
        }

        public async Task RemoveItem(int itemId)
        {
            await _itemsRepository.Remove(itemId);
        }
    }
}