using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Models;


namespace MusicStore.WebApp.Controllers
{
    public class ItemController : Controller
    {
        private IItems _item;

        public ItemController(IItems item)
        {
            _item = item;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Items(string searchString, int pageNumber=1)
        {
            var items =  _item.GetBind();
            var paginatedList = await PaginatedList<Item>.CreateAsync(items, pageNumber, 5);
           
            return View(paginatedList);
            
        }
        
        [Authorize(Roles = "Admin")]
        public  IActionResult AddItem()
        {
           var types =  _item.GetTypes().ToList();
            SelectList selectList = new SelectList(types, "Id", "Type");
            ViewBag.Types = selectList;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddItem(Item item)
        {
            if (ModelState.IsValid)
            {
                await _item.Create(item);
                return Redirect("/Item/Items");
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditItem(int itemId)
        {
            var item = await _item.Get(itemId);
            var types =  _item.GetTypes().ToList();
            SelectList selectList = new SelectList(types, "Id", "Name", item.TypeId);
            ViewBag.Types = selectList;
            return View("EditItem", item);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditItem(Item itemDTO, int pageNumber=1)
        {
            if (ModelState.IsValid)
            {
                await _item.Update(itemDTO);
                var list = _item.GetAll();
                return View("Items",  await PaginatedList<Item>.CreateAsync(list, pageNumber, 5));
            }
            ViewBag.Message = string.Format("Input error!");
            // return RedirectToAction("EditPerson", new { personid = obj.id });
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteItem(int itemId, int pageNumber=1)
        {
            await _item.Remove(itemId);
            return View("Items",  await PaginatedList<Item>.CreateAsync(_item.GetAll(), pageNumber, 5));
        }
    }
}