using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Helpers;
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
                                               || s.Type.Type.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(type))
            {
                items = items.Where(x => x.TypeId.ToString() == type);
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var pagedList = new IndexViewModel();
            pagedList.Items = items.ToPagedList(pageNumber, pageSize);
            return View(pagedList);
            
        }
         [HttpGet]
         [Authorize]
         public async Task<IActionResult> AddToCart(int id)
         {
            
                 var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 var usersCart = await _cart.GetCart(userId).ToListAsync();
                 var index = usersCart.FindIndex(item => item.ItemId == id);
                 if (index >= 0)
                 {
                     return Redirect("/Cart/GetCart");
                 }
                 else
                 {
                     var cartDto = new Cart();
                     cartDto.PriceId = id;
                     cartDto.ItemId = id;
                     cartDto.UserId = userId;
                     await _cart.AddToCart(cartDto);
                     return Redirect("/Cart/GetCart");
                 }
         }
         [Authorize]
         [HttpGet]
         public async Task<ActionResult> GetCart(int pageNumber=1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart =   _cart.GetCart(userId);
            var count = _cart.GetCart(userId).ToList().Count;
            var pagedList = new IndexViewModel();
            pagedList.Cart = await PaginatedList<Cart>.CreateAsync(cart, pageNumber, count);
            pagedList.ItemsQuantities = new List<int>();
            foreach (var item in cart)
                pagedList.ItemsQuantities.Add(1);
            return View("Cart",pagedList);
            
        }
         [HttpGet]
         [Authorize]
         public async Task<IActionResult> AddOrder(IndexViewModel model)
         {
             if (ModelState.IsValid)
             {
                 var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 var userEmail = User.FindFirstValue(ClaimTypes.Email);
                 var usersCart = _cart.GetCart(userId);
                 Order Order;
                 var list = new List<Order>();
                 var guid = System.Guid.NewGuid();
                 var itemsQuantities = model.ItemsQuantities;
                 foreach (var item in usersCart)
                 {
                     Order = new Order();
                     Order.Id = item.Id;
                     Order.ItemId = item.ItemId;
                     Order.PriceId = item.ItemId;
                     Order.OrderId = guid;
                     list.Add(Order); 
                 }
                 
                 for (int i = 0; i < list.Count(); i++)
                     list[i].Count = itemsQuantities[i];
                 
                 await _order.SubmitOrder(list, userId);
                 await _cart.RemoveRange(userId);
                 await SendEmail.Send(userEmail, "Order",
                     $"Your order №{list.First().Id.ToString()} has been submitted. Wait for status change in few days");
                 return Redirect("/Order/GetOrders");
             }
             return Redirect("/Item/Items");
         }
         [AllowAnonymous]
         [HttpGet]
         public IActionResult Details(int id)
         {
             var cart = _item.GetAll().FirstOrDefault(com => com.Id == id);
             var itemViewModel = new ItemViewModel()
             {
                 Id = cart.Id,
                 Name = cart.Name,
                 Price = cart.Price,
                 Description = cart.Description,
                 ImageName = cart.ImageName,
                 TypeId = cart.TypeId
             };
             return PartialView(itemViewModel);
            
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