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
         public async Task<IActionResult> GetItems(string type, string searchString,string currentFilter, int? page)
         { 
             var homeModel = await _cartService.GetItems(type,  searchString);
             //select list that represents types of items
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
            return View("Items", pagedList);
            
        }
         [HttpGet]
         [AllowAnonymous]
         public async Task<IActionResult> AddToCart(int id)
         {
             //get user by id
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             //if user is not authorized
                 if (userId == null)
                 {
                     //find item in database by id
                     var item = await _itemService.GetItem(id);
                     //if cookie session is not created
                     if (Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") == null)
                     {
                         //create cart list, it pushed to cookie session and creates this cookie
                         List<Cart> cart = new List<Cart>();
                         _cartService.AddToAnonymousCart(cart, item, id);
                         Session.SetObjectAsJson(HttpContext.Session, "cart", cart);
                     }
                     //otherwise if cookie cart is already exist
                     else
                     {
                         //get all object from cookie and push it to list
                         List<Cart> cart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                         int ind = _cartService.CheckItemInCart(cart, id);
                         //check if item is already in cart
                         if (ind != -1) // if item is already in list than redirect user to cart page
                         {
                             return Redirect("/Cart/GetCart");
                         }
                         else
                         {
                             _cartService.AddToAnonymousCart(cart, item, id); // otherwise add it to cart
                         }
                         Session.SetObjectAsJson(HttpContext.Session, "cart", cart); //update cookie
                     }
                 }
                 else //if user is authorized
                 {
                     //create paginated list with user's cart in it
                     var usersCart = await PagedListExtensions.ToListAsync(_cartService.GetUsersCart(userId));
                     var index = usersCart.FindIndex(item => item.ItemId == id);
                     //if item already in cart
                     if (index >= 0)
                     {
                         return Redirect("/Cart/GetCart"); //redirect user to cart page
                     }
                     else
                     {
                         await _cartService.AddToCart(id, userId); //otherwise add it to database and also redirect
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
            //find user by id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if user is not authorized
            if (userId == null)
            {
                //get cart from cookie
                var sessionCart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                pagedList.Cart = sessionCart;
                //if cart is not empty
                if (sessionCart != null)
                {
                    pagedList.ItemsQuantities = _cartService.SetQuantitiesForAnonymous(sessionCart); //map item's quantities 
                    return View("Cart", pagedList); //return cart view
                }
                //if cart is empty than return nothing
                pagedList.Cart = await PagedListExtensions.ToListAsync(_cartService.GetUsersCart("0"));
                return View("Cart", pagedList);
            }
            //if user is authorized - get cart from database
            var cart = _cartService.GetUsersCart(userId);
            //create paginated list
            pagedList.Cart = await PagedListExtensions.ToListAsync(cart);
            //set default quantities for items
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
                 //if user is not authorized
                 if (userId == null)
                 {
                     //get cart from object
                     List<Cart> cart = Session.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                     //create order of list
                     List<Order> orders = _cartService.AddCookieOrder(cart);
                     //map quantities from cart to orders
                     for (int i = 0; i < orders.Count(); i++)
                         orders[i].Count = model.ItemsQuantities[i];
                     //create cookie that represents list of orders
                     Session.SetObjectAsJson(HttpContext.Session, "orders", orders);
                     return View("AnonymousSubmit");
                 }
                 //if user is authorized
                 var userEmail = User.FindFirstValue(ClaimTypes.Email); //get user's email
                 var usersCart = await _cartService.GetUsersCart(userId).ToListAsync(); // get cart
                 var itemsQuantities = model.ItemsQuantities; //again get quantities 
                 var list = _cartService.AddCookieOrder(usersCart); //create list of orders
                 
                 for (int i = 0; i < list.Count(); i++) //map cart quantities to order's quantities
                     list[i].Count = itemsQuantities[i];

                 await _cartService.SubmitOrder(list, userEmail, userId); //submit order
                 return Redirect("/Order/GetOrders");
             }
             return Redirect("/Item/Items");
         }
         
         [AllowAnonymous]
         [HttpGet]
         public IActionResult GetDetails(int id)
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
             return PartialView("Details",itemViewModel);
            
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