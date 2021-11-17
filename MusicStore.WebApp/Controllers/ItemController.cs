using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Business.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Helpers;
using MusicStore.WebApp.Models;
using X.PagedList;


namespace MusicStore.WebApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IWebHostEnvironment _webHostEnvironment;
   

        public ItemController(IItemService itemService,  IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _itemService = itemService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetItems(string sortOrder,string searchString,string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DescriptionParam = sortOrder == "Description" ? "Description_desc" : "Description";
            ViewBag.TypeParam = sortOrder == "Type" ? "Type_desc" : "Type";
            ViewBag.IdParam = sortOrder == "Id" ? "Id_desc" : "Id";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var items = _itemService.GetAllItems(sortOrder, searchString);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var pagedList = new IndexViewModel();
            pagedList.Items = items.ToPagedList(pageNumber, pageSize);
            return View("Items", pagedList);
            
        }
        
        [Authorize(Roles = "Admin")]
        public  IActionResult AddItem()
        {
           var types =  _itemService.GetAllTypes();
           SelectList selectList = new SelectList(types, "Id", "Type");
           ViewBag.Types = selectList;
           return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddItem(ItemViewModel itemDto)
        {
            var types = _itemService.GetAllTypes();
            SelectList selectList = new SelectList(types, "Id", "Type");
            ViewBag.Types = selectList;
            if (ModelState.IsValid)
            {
                string wwwRoothPath = _webHostEnvironment.WebRootPath;
                await ImageMapper.MapImage(itemDto, wwwRoothPath);
                var item = new Item
                {
                    Name = itemDto.Name,
                    Price = itemDto.Price,
                    Description = itemDto.Description,
                    ImageName = itemDto.ImageName,
                    TypeId = itemDto.TypeId
                };
                await _itemService.CreateItem(item);
                return Redirect("/Item/GetItems");
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditItem(int itemId)
        {
            var item = await _itemService.GetItem(itemId);
            var itemViewModel = new ItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Description = item.Description,
                ImageName = item.ImageName,
                TypeId = item.TypeId
            };
            return View("EditItem", itemViewModel);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditItem(ItemViewModel itemDto, int pageNumber=1)
        {
            if (ModelState.IsValid)
            {
                string wwwRoothPath = _webHostEnvironment.WebRootPath;
                await ImageMapper.MapImage(itemDto, wwwRoothPath);
                var item = new Item
                {
                    Id = itemDto.Id,
                    Name = itemDto.Name,
                    Price = itemDto.Price,
                    Description = itemDto.Description,
                    ImageName = itemDto.ImageName,
                    TypeId = itemDto.TypeId
                };
                await _itemService.UpdateItem(item);
                return Redirect("/Item/GetItems");
            }   
            ViewBag.Message = string.Format("Input error!");
            return View(itemDto);

        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteItem(int itemId, int pageNumber=1)
        {
            await _itemService.RemoveItem(itemId);
            return Redirect("/Item/GetItems");
        }
    }
}