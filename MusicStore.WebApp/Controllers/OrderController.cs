using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var pagedList = new IndexViewModel();
            pagedList.UsersOrders = await PaginatedList<UsersOrders>.CreateAsync(orders, pageNumber, 5);
            return View("MyOrders",pagedList);

        }
        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOrder = _order.GetUsersOrders(userId).FirstOrDefault(i => i.OrderId == id);
            var orderIdKey = _order.GetOrderId(userOrder.OrderId);
            var items = _order.OrderDetails(orderIdKey);
            return PartialView(new OrderViewModel
            {
                Orders = items
            });
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
            
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Date.ToString().Contains(searchString)
                                           || s.OrderId.ToString().Contains(searchString)
                                           || s.User.Email.Contains(searchString)
                                           || s.Id.ToString().Contains(searchString));
                
            }
            switch (sortOrder)
            {
                case "IdDesc":
                    orders = orders.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    orders = orders.OrderBy(s => s.Id);
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
                    orders = orders.OrderByDescending(s => s.Status);
                    break;
                case "Status":
                    orders = orders.OrderBy(s => s.Status);
                    break;
                default:
                    orders = orders.OrderBy(s => s.Date);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var pagedList = new IndexViewModel();
            pagedList.BootstrapUsersOrders = orders.ToPagedList(pageNumber, pageSize);
            return View("Unproccessed",pagedList);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Finished(string sortOrder,string searchString,string currentFilter, int? page)
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
            
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Date.ToString().Contains(searchString)
                                           || s.OrderId.ToString().Contains(searchString) 
                                           || s.User.Email.Contains(searchString)
                                           || s.Id.ToString().Contains(searchString));;
            }
            switch (sortOrder)
            {
                case "IdDesc":
                    orders = orders.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    orders = orders.OrderBy(s => s.Id);
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
                    orders = orders.OrderByDescending(s => s.Status);
                    break;
                case "Status":
                    orders = orders.OrderBy(s => s.Status);
                    break;
                default:
                    orders = orders.OrderBy(s => s.Date);
                    break;
            }
            int pageNumber = (page ?? 1);
            var pagedList = new IndexViewModel();
            pagedList.UsersOrders = await PaginatedList<UsersOrders>.CreateAsync(orders, pageNumber, 5);
            return View("Finished",pagedList);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(int itemId)
        {
            var order = await _order.GetOrder(itemId);
            var OrderView = new OrderViewModel()
            {
                Id = itemId,
                Status = order.Status
            };
            return View("Status", OrderView);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(UsersOrders orderDto)
        {
            var order = new UsersOrders()
            {
                Id = orderDto.Id,
                Date = orderDto.Date,
                Status = orderDto.Status,
                OrderId = orderDto.OrderId,
                UserId = orderDto.UserId
            };
            await _order.ChangeOrderStatus(order);
            return Redirect("/Order/GetUnproccessed");
        }
        }
   
    }
