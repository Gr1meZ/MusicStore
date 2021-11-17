using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Business.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Models;


namespace MusicStore.WebApp.Controllers
{
    public class ItemTypeController : Controller
    {
        private readonly IItemTypeService _typeService;

        public ItemTypeController(IItemTypeService typeService)
        {
            _typeService = typeService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetTypes(int pageNumber=1)
        {

            var types =  _typeService.GetAllTypes();
            var pagedList = new IndexViewModel();
            pagedList.Types = await PaginatedList<ItemType>.CreateAsync(types, pageNumber, 5);
            return View("Types",pagedList);
            
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
                await _typeService.CreateType(type);
                return Redirect("/ItemType/GetTypes");
                
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditType(int typeId)
        {
            var type = await _typeService.GetType(typeId);
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
                await _typeService.UpdateType(type);
                return Redirect("/ItemType/GetTypes");
            }
            ViewBag.Message = string.Format("Input error!");
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteType(int typeId, int pageNumber=1)
        {
            await _typeService.RemoveType(typeId);
            return Redirect("/ItemType/GetTypes");
        }
    }
}