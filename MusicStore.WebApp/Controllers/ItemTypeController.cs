using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Models;


namespace MusicStore.WebApp.Controllers
{
    public class ItemTypeController : Controller
    {
        private IItemType _type;

        public ItemTypeController(IItemType type)
        {
            _type = type;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Types(int pageNumber=1)
        {

            var types =  _type.GetAll();
            var paginatedList = await PaginatedList<ItemType>.CreateAsync(types, pageNumber, 5);
           
            return View(paginatedList);
            
        }
        
        [Authorize(Roles = "Admin")]
        public  IActionResult AddType()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddType(ItemType type)
        {
            if (ModelState.IsValid)
            {
                await _type.Create(type);
                
            }
            return View("AddType");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditType(int typeId)
        {
            var type = await _type.Get(typeId);
            return View("EditType", type);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditType(ItemType typeDTO, int pageNumber=1)
        {
            if (ModelState.IsValid)
            {
                await _type.Update(typeDTO);
                var list = _type.GetAll();
                return View("Types",  await PaginatedList<ItemType>.CreateAsync(list, pageNumber, 5));
            }
            ViewBag.Message = string.Format("Input error!");
            // return RedirectToAction("EditPerson", new { personid = obj.id });
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteType(int typeId, int pageNumber=1)
        {
            await _type.Remove(typeId);
            return View("Types",  await PaginatedList<ItemType>.CreateAsync(_type.GetAll(), pageNumber, 5));
        }
    }
}