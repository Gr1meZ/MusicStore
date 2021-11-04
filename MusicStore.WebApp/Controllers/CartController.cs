using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Models;
using X.PagedList;

namespace MusicStore.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cart;
        private readonly IItemsRepository _item;
        private readonly IOrderRepository _order;

        public CartController(ICartRepository cart ,IItemsRepository items, IOrderRepository order)
        {
            _cart = cart;
            _item = items;
            _order = order;
        }
        [AllowAnonymous]
        [HttpGet]
         public ActionResult Items(string type, string searchString,string currentFilter, int? page)
        {
            var items =   _item.GetBind();
            var types =  _item.GetTypes().ToList();
            SelectList selectList = new SelectList(types, "Id", "Type");
            ViewBag.Types = selectList;
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
            if (!string.IsNullOrEmpty(type))
            {
                items = items.Where(x => x.TypeId.ToString() == type);
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
                 var Guid = System.Guid.NewGuid();
                 foreach (var item in usersCart)
                 {
                     Order = new Order();
                     Order.Id = item.Id;
                     Order.ItemId = item.ItemId;
                     Order.PriceId = item.ItemId;
                     Order.OrderId = Guid;
                     list.Add(Order);
                 }
                 
                 await _order.SubmitOrder(list, userId);
                 await _cart.RemoveRange(userId);
                 return Redirect("/Order/GetOrders");
             }
             return Redirect("/Item/Items");
         }
         [AllowAnonymous]
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