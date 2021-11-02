using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Models;
using X.PagedList;

namespace MusicStore.WebApp.Controllers
{
    public class CartController : Controller
    {
        private ICartRepository _cart;
        private IItems _item;
        private readonly IOrderRepository _order;

        public CartController(ICartRepository cart ,IItems items, IOrderRepository order)
        {
            _cart = cart;
            _item = items;
            _order = order;
        }
        [Authorize]
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
           
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
            
        }
         [HttpGet]
         [Authorize]
         public async Task<IActionResult> AddToCart(int id)
         {
                 var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 var cartDTO = new Cart();
                 cartDTO.priceId = id;
                 cartDTO.ItemId = id;
                 cartDTO.UserId = userId;
                 await _cart.AddToCart(cartDTO);
                 return Redirect("/Cart/GetCart");
         }
         [Authorize]
        [HttpGet]
         public async Task<ActionResult> GetCart(int pageNumber=1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart =   _cart.GetCart(userId);
            var paginatedList = await PaginatedList<Cart>.CreateAsync(cart, pageNumber, 5);
            return View("Cart",paginatedList);
            
        }
         [HttpGet]
         [Authorize]
         public async Task<IActionResult> AddOrder()
         {
             if (ModelState.IsValid)
             {
                 var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 var usersCart = _cart.GetCart(userId);
                 Order Order;
                 var list = new List<Order>();
                 var Date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                 var Guid = System.Guid.NewGuid();
                 foreach (var item in usersCart)
                 {
                     Order = new Order();
                     Order.Id = item.Id;
                     Order.ItemId = item.ItemId;
                     Order.Price = item.ItemId;
                     Order.OrderId = Guid;
                     list.Add(Order);
                 }

                 var count = list.Count;
                 await _order.SubmitOrder(list, userId);
                 return Redirect("");
             }
             return Redirect("/Item/Items");
         }
         [Authorize]
         [HttpGet]
         public IActionResult Details(int id)
         {
             var cart = _item.GetAll().FirstOrDefault(com => com.Id == id);
             return PartialView(cart);
            
         }
         [HttpGet]
         [Authorize]
         public async Task<IActionResult> RemoveCartItem(int cartId)
         {
             await _cart.Remove(cartId);
             return Redirect("/Cart/GetCart");
         }
    }
}