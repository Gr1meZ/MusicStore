using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Business.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Helpers;
using MusicStore.WebApp.Models;
using X.PagedList;

namespace MusicStore.WebApp.Controllers
{
    public class CartController : Controller
    {
    
        private readonly ICartService _cartService;
        private readonly IItemService _itemService;

        public CartController(ICartService cartService, IItemService itemService)
        {
            _cartService = cartService;
            _itemService = itemService;
        }
        [AllowAnonymous]
        [HttpGet]
         public async Task<IActionResult> Items(string type, string searchString,string currentFilter, int? page)
         { 
             var homeModel = await _cartService.GetItems(type,  searchString);
            SelectList selectList = new SelectList(homeModel.Types, "Id", "Type");
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var pagedList = new IndexViewModel();
            pagedList.Items = homeModel.Items.ToPagedList(pageNumber, pageSize);
            return View(pagedList);
            
        }
         [HttpGet]
         [AllowAnonymous]
         public async Task<IActionResult> AddToCart(int id)
         {
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 if (userId == null)
                 {
                     var item = await _itemService.GetItem(id);
                     if (Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") == null)
                     {
                         List<Cart> cart = new List<Cart>();
                         _cartService.AddToAnonymousCart(cart, item, id);
                         Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
                     }
                     else
                     {
                         List<Cart> cart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                         int ind = _cartService.CheckItemInCart(cart, id);
                         if (ind != -1)
                         {
                             return Redirect("/Cart/GetCart");
                         }
                         else
                         {
                             _cartService.AddToAnonymousCart(cart, item, id);
                         }
                         Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
                     }
                 }
                 else
                 {
                     var usersCart = await PagedListExtensions.ToListAsync(_cartService.GetUsersCart(userId));
                     var index = usersCart.FindIndex(item => item.ItemId == id);
                     if (index >= 0)
                     {
                         return Redirect("/Cart/GetCart");
                     }
                     else
                     {
                         await _cartService.AddToCart(id, userId);
                         return Redirect("/Cart/GetCart");
                     }
                 }
                 return Redirect("/Cart/GetCart");
         }
         
         [AllowAnonymous]
         [HttpGet]
         public async Task<ActionResult> GetCart()
        {
            var pagedList = new IndexViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                var sessionCart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                pagedList.Cart = sessionCart;
               
                if (sessionCart != null)
                {
                    pagedList.ItemsQuantities = _cartService.SetQuantitiesForAnonymous(sessionCart);
                    return View("Cart", pagedList);
                }
                pagedList.Cart = await PagedListExtensions.ToListAsync(_cartService.GetUsersCart("0"));
                return View("Cart", pagedList);
            }
            var cart = _cartService.GetUsersCart(userId);
            pagedList.Cart = await PagedListExtensions.ToListAsync(cart);
            pagedList.ItemsQuantities = _cartService.SetQuantitiesForUser(cart);
            return View("Cart", pagedList);
        }

         [HttpPost]
         [AllowAnonymous]
         public async Task<IActionResult> AnonymousSubmit(AnonymousViewModel model)
         {
             if (ModelState.IsValid)
             {
                 List<Order> orders = Session.GetObjectFromJson<List<Order>>(HttpContext.Session, "orders");
                 await _cartService.SubmitForAnonymous(orders, model.Email);
                 HttpContext.Session.Clear();
                 return View("OrderInfo", model);
             }
             return View("AnonymousSubmit");
         }
         
         [HttpGet]
         [AllowAnonymous]
         public async Task<IActionResult> AddOrder(IndexViewModel model)
         {
            
             if (ModelState.IsValid)
             {
                 var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 if (userId == null)
                 {
                     List<Cart> cart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                     List<Order> orders = _cartService.AddCookieOrder(cart);
                     for (int i = 0; i < orders.Count(); i++)
                         orders[i].Count = model.ItemsQuantities[i];
                     Session.SetObjectAsJson(HttpContext.Session, "orders", orders);
                     return View("AnonymousSubmit");
                 }
                 var userEmail = User.FindFirstValue(ClaimTypes.Email);
                 var usersCart = await _cartService.GetUsersCart(userId).ToListAsync();
                 var itemsQuantities = model.ItemsQuantities;
                 var list = _cartService.AddCookieOrder(usersCart);
                 
                 for (int i = 0; i < list.Count(); i++)
                     list[i].Count = itemsQuantities[i];

                 await _cartService.SubmitOrder(list, userEmail, userId);
                 return Redirect("/Order/GetOrders");
             }
             return Redirect("/Item/Items");
         }
         [AllowAnonymous]
         [HttpGet]
         public IActionResult Details(int id)
         {
             var cart = _itemService.GetAllItems().FirstOrDefault(com => com.Id == id);
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
                 int index = _cartService.CheckCartValue(cart, cartId);
                 cart.RemoveAt(index);
                 Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
                 return Redirect("/Cart/GetCart");
             }
             else
             {
                 await _cartService.RemoveCartItem(cartId);
                 return Redirect("/Cart/GetCart");
             }
         }
    }
}