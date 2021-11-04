using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;
using MusicStore.WebApp.Models;
using X.PagedList;

namespace MusicStore.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _order;
        private readonly ICartRepository _cart;

        public OrderController(IOrderRepository order, ICartRepository cart)
        {
            _order = order;
            _cart = cart;
        }
    
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrders(int pageNumber=1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _order.GetUsersOrders(userId);
            var paginatedList = await PaginatedList<UsersOrders>.CreateAsync(orders, pageNumber, 5);
            return View("MyOrders",paginatedList);

        }
        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOrder = _order.GetUsersOrders(userId).FirstOrDefault(i => i.OrderId == id);
            var OrderIdKey = _order.GetOrderId(userOrder.OrderId);
            var items = _order.OrderDetails(OrderIdKey);
            return PartialView(items);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUnproccessed(string sortOrder,string searchString,string currentFilter, int? page)
        {
            var orders =   _order.GetUnproccessed();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Date = String.IsNullOrEmpty(sortOrder) ? "date" : "";
            ViewBag.Status = sortOrder == "Status" ? "StatusDesc" : "Status";
            ViewBag.OrderId = sortOrder == "OrderId"? "OrderIdDesc" : "OrderId";
            ViewBag.UserId = sortOrder == "UserId" ? "UserIdDesc" : "UserId";
            ViewBag.Id = sortOrder == "Id" ? "IdDesc" : "Id";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            int count = orders.Count();
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Date.ToString().Contains(searchString)
                                           || s.OrderId.ToString().Contains(searchString)
                                           || s.User.Email.Contains(searchString)
                                           || s.id.ToString().Contains(searchString));
                
            }
            switch (sortOrder)
            {
                case "IdDesc":
                    orders = orders.OrderByDescending(s => s.id);
                    break;
                case "Id":
                    orders = orders.OrderBy(s => s.id);
                    break;
                case "UserIdDesc":
                    orders = orders.OrderByDescending(s => s.UserId);
                    break;
                case "UserId":
                    orders = orders.OrderBy(s => s.UserId);
                    break;
                case "OrderIdDesc":
                    orders = orders.OrderByDescending(s => s.OrderId);
                    break;
                case "OrderId":
                    orders = orders.OrderBy(s => s.OrderId);
                    break;
                case "StatusDesc":
                    orders = orders.OrderByDescending(s => s.status);
                    break;
                case "Status":
                    orders = orders.OrderBy(s => s.status);
                    break;
                default:
                    orders = orders.OrderBy(s => s.Date);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View("Unproccessed",orders.ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Finished(string sortOrder,string searchString,string currentFilter, int? page)
        {
            var orders =   _order.GetLogs();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Date = String.IsNullOrEmpty(sortOrder) ? "date" : "";
            ViewBag.Status = sortOrder == "Status" ? "StatusDesc" : "Status";
            ViewBag.OrderId = sortOrder == "OrderId"? "OrderIdDesc" : "OrderId";
            ViewBag.UserId = sortOrder == "UserId" ? "UserIdDesc" : "UserId";
            ViewBag.Id = sortOrder == "Id" ? "IdDesc" : "Id";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            int count = orders.Count();
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Date.ToString().Contains(searchString)
                                           || s.OrderId.ToString().Contains(searchString) 
                                           || s.User.Email.Contains(searchString)
                                           || s.id.ToString().Contains(searchString));;
            }
            switch (sortOrder)
            {
                case "IdDesc":
                    orders = orders.OrderByDescending(s => s.id);
                    break;
                case "Id":
                    orders = orders.OrderBy(s => s.id);
                    break;
                case "UserIdDesc":
                    orders = orders.OrderByDescending(s => s.UserId);
                    break;
                case "UserId":
                    orders = orders.OrderBy(s => s.UserId);
                    break;
                case "OrderIdDesc":
                    orders = orders.OrderByDescending(s => s.OrderId);
                    break;
                case "OrderId":
                    orders = orders.OrderBy(s => s.OrderId);
                    break;
                case "StatusDesc":
                    orders = orders.OrderByDescending(s => s.status);
                    break;
                case "Status":
                    orders = orders.OrderBy(s => s.status);
                    break;
                default:
                    orders = orders.OrderBy(s => s.Date);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View("Finished",orders.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(int itemId)
        {
            var order = await _order.GetOrder(itemId);
       
            return View("Status", order);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(UsersOrders orderDTO)
        {
            await _order.ChangeOrderStatus(orderDTO);
            return Redirect("/Order/GetUnproccessed");
        }
        }
   
    }
