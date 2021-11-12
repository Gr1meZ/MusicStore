using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(type))
            {
                items = items.Where(s => (s.Name.Contains(searchString)
                                               || s.Price.ToString().Contains(searchString))
                                               && s.TypeId.ToString() == type);
            }
            if (string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString)
                                          || s.Price.ToString().Contains(searchString));
            }
            else if (!string.IsNullOrEmpty(type) && string.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.TypeId.ToString() == type);
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var pagedList = new IndexViewModel();
            pagedList.Items = items.ToPagedList(pageNumber, pageSize);
            return View(pagedList);
            
        }
         [HttpGet]
         [AllowAnonymous]
         public async Task<IActionResult> AddToCart(int id)
         {

             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 if (userId == null)
                 {
                     var item = await _item.Get(id);
                     if (Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") == null)
                     {
                         List<Cart> cart = new List<Cart>();
                         cart.Add(new Cart
                         {
                             PriceId = id,
                             ItemId = id,
                             Item = item,
                             Price = item,
                             UserId = null
                         });
                         Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
                     }
                     else
                     {
                         List<Cart> cart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                         int ind = IsItemExist(id);
                         if (ind != -1)
                         {
                             return Redirect("/Cart/GetCart");
                         }
                         else
                         {
                             cart.Add(new Cart
                             {
                                 PriceId = id,
                                 ItemId = id,
                                 Item = item,
                                 Price = item,
                                 UserId = null
                             });
                         }
                         Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
                         
                     }
                 }
                 else
                 {
                     var usersCart = await PagedListExtensions.ToListAsync(_cart.GetCart(userId));
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
                 return Redirect("/Cart/GetCart");
         }
         [AllowAnonymous]
         [HttpGet]
         public async Task<ActionResult> GetCart(int pageNumber=1)
        {
            var pagedList = new IndexViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                var sessionCart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                pagedList.Cart = sessionCart;
                pagedList.ItemsQuantities = new List<int>();
                if (sessionCart != null)
                {
                    foreach (var item in sessionCart)
                        pagedList.ItemsQuantities.Add(1);
                    return View("Cart", pagedList);
                }
                else
                {
                    pagedList.Cart = await PagedListExtensions.ToListAsync(_cart.GetCart("0"));
                    return View("Cart", pagedList);
                }
            }
            else
            {
                var cart = _cart.GetCart(userId);
                pagedList.Cart = await PagedListExtensions.ToListAsync(cart);
                pagedList.ItemsQuantities = new List<int>();
                foreach (var item in cart)
                    pagedList.ItemsQuantities.Add(1);
                return View("Cart", pagedList);
            }
        }

         [HttpPost]
         [AllowAnonymous]
         public async Task<IActionResult> AnonymousSubmit(AnonymousViewModel model)
         {
             if (ModelState.IsValid)
             {
                 List<Order> orders = Session.GetObjectFromJson<List<Order>>(HttpContext.Session, "orders");
                 var orderId =  await _order.SubmitAnonymousOrder(orders, model.Email);
                 HttpContext.Session.Clear();
                 await SendEmail.Send(model.Email, "Order",
                     $"Your order №{orderId} has been submitted. Wait for status change in few days");
                 return View("OrderInfo", model);
             }

             return View("AnonymousSubmit");
         }
         [HttpGet]
         [AllowAnonymous]
         public async Task<IActionResult> AddOrder(IndexViewModel model)
         {
             Order Order;
             var guid = Guid.NewGuid();
             if (ModelState.IsValid)
             {
                 var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 if (userId == null)
                 {
                     List<Cart> cart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                     List<Order> orders = new List<Order>();
                     foreach (var item in cart)
                     {
                         Order = new Order();
                         Order.Id = item.Id;
                         Order.ItemId = item.ItemId;
                         Order.PriceId = item.ItemId;
                         Order.OrderId = guid;
                         orders.Add(Order); 
                     }
                 
                     for (int i = 0; i < orders.Count(); i++)
                         orders[i].Count = model.ItemsQuantities[i];
                     Session.SetObjectAsJson(HttpContext.Session, "orders", orders);
                     return View("AnonymousSubmit");
                 }
                 var userEmail = User.FindFirstValue(ClaimTypes.Email);
                 var usersCart = _cart.GetCart(userId);
                 var list = new List<Order>();
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
         [AllowAnonymous]
         public async Task<IActionResult> RemoveCartItem(int cartId)
         { 
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             if (userId == null)
             {
                 List<Cart> cart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                 int index = IsExist(cartId);
                 cart.RemoveAt(index);
                 Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
                 return Redirect("/Cart/GetCart");
             }
             else
             {
                 await _cart.Remove(cartId);
                 return Redirect("/Cart/GetCart");
             }
         }
         private int IsExist(int id)
         {
             List<Cart> cart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
             for (int i = 0; i < cart.Count; i++)
             {
                 if (cart[i].Id.Equals(id))
                 {
                     return i;
                 }
             }
             return -1;
         }
         private int IsItemExist(int id)
         {
             List<Cart> cart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
             for (int i = 0; i < cart.Count; i++)
             {
                 if (cart[i].ItemId.Equals(id))
                 {
                     return i;
                 }
             }
             return -1;
         }
    }
}