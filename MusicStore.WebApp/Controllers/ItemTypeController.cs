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
        private readonly IItemTypeRepository _type;

        public ItemTypeController(IItemTypeRepository type)
        {
            _type = type;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Types(int pageNumber=1)
        {

            var types =  _type.GetAll();
            var paginatedList = await PaginatedList<ItemType>.CreateAsync(types, pageNumber, 5);
            var pagedList = new IndexViewModel();
            pagedList.Types = await PaginatedList<ItemType>.CreateAsync(types, pageNumber, 5);
            return View(pagedList);
            
        }
        
        [Authorize(Roles = "Admin")]
        public  IActionResult AddType()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
       
        public async Task<IActionResult> AddType(ItemTypeViewModel typeDto)
        {
            if (ModelState.IsValid)
            {
                var type = new ItemType()
                {
                    Type = typeDto.Type
                };
                await _type.Create(type);
                return Redirect("/ItemType/Types");
                
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditType(int typeId)
        {
            var type = await _type.Get(typeId);
            var itemView = new ItemTypeViewModel()
            {
                Id = type.Id,
                Type = type.Type
            };
            return View("EditType", itemView);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditType(ItemTypeViewModel typeDto, int pageNumber=1)
        {
            if (ModelState.IsValid)
            {
                var type = new ItemType()
                {
                    Id = typeDto.Id,
                    Type = typeDto.Type
                };
                await _type.Update(type);
                var list = _type.GetAll();
                return Redirect("/ItemType/Types");
            }
            ViewBag.Message = string.Format("Input error!");
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteType(int typeId, int pageNumber=1)
        {
            await _type.Remove(typeId);
            return Redirect("/ItemType/Types");
        }
    }
}