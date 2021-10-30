using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Items(int pageNumber=1)
        {

            var items =  _item.GetAll();
            return View(await PaginatedList<Item>.CreateAsync(items, pageNumber, 5));
            
    }
        [Authorize(Roles = "Admin")]
        public  IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddItem(Item item)
        {
            if (ModelState.IsValid)
            {
                await _item.Create(item);
                
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditItem(int itemId)
        {
            var item = await _item.Get(itemId);
            return View("EditItem", item);
        }
        
        [HttpPost]
        public async Task<IActionResult> EditItem(Item itemDTO)
        {
            if (ModelState.IsValid)
            {
                await _item.Update(itemDTO);
                var item = _item.Get(itemDTO.Id);
                var list = _item.GetAll().ToList();
                return View("Items",  await PaginatedList<Item>.CreateAsync(_item.GetAll(), 1, 5));
            }
            ViewBag.Message = string.Format("Input error!");
            // return RedirectToAction("EditPerson", new { personid = obj.id });
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            await _item.Remove(itemId);
            return View("Items",  await PaginatedList<Item>.CreateAsync(_item.GetAll(), 1, 5));
        }
    }
}