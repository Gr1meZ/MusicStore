using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Helpers;
using MusicStore.WebApp.Models;
using X.PagedList;


namespace MusicStore.WebApp.Controllers
{
    public class ItemController : Controller
    {
        private IItems _item;
        private readonly IWebHostEnvironment _webHostEnvironment;
   

        public ItemController(IItems item, IWebHostEnvironment webHostEnvironment)
        {
            _item = item;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Items(string sortOrder,string searchString,string currentFilter, int? page)
        {
            var items =   _item.GetBind();
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
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString)
                                               || s.Price.ToString().Contains(searchString)
                                               || s.type.Type.Contains(searchString));
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
                    items = items.OrderBy(s => s.type.Type);
                    break;
                case "Type_desc":
                    items = items.OrderByDescending(s => s.type.Type);
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
           
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
            
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
        public async Task<IActionResult> AddItem(Item itemDTO)
        {
            if (ModelState.IsValid)
            {
                string wwwRoothPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(itemDTO.ImageFile.FileName);
                string extension = Path.GetExtension(itemDTO.ImageFile.FileName);
                itemDTO.ImageName = fileName + extension;
                string path = Path.Combine(wwwRoothPath + "/Image/", fileName + ".jpg");
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await itemDTO.ImageFile.CopyToAsync(fileStream);
                }
                await _item.Create(itemDTO);
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
                string wwwRoothPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(itemDTO.ImageFile.FileName);
                string extension = Path.GetExtension(itemDTO.ImageFile.FileName);
                itemDTO.ImageName = fileName + extension;
                string path = Path.Combine(wwwRoothPath + "/Image/", fileName + ".jpg");
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await itemDTO.ImageFile.CopyToAsync(fileStream);
                }
                await _item.Update(itemDTO);
                var list = _item.GetAll();
                return Redirect("/Item/Items");
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
            return Redirect("/Item/Items");
        }
    }
}