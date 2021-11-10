using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderRepository order, ICartRepository cart, UserManager<ApplicationUser> userManager)
        {
            _order = order;
            _cart = cart;
            _userManager = userManager;
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
        public IActionResult GetUnproccessedAnonymous(string sortOrder,string searchString,string currentFilter, int? page)
        {
            var orders =   _order.GetUnproccessedAnonymous();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Date = String.IsNullOrEmpty(sortOrder) ? "date" : "";
            ViewBag.Status = sortOrder == "Status" ? "StatusDesc" : "Status";
            ViewBag.OrderId = sortOrder == "OrderId"? "OrderIdDesc" : "OrderId";
            ViewBag.Email = sortOrder == "Email" ? "EmailDesc" : "Email";
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
                                           || s.Email.Contains(searchString)
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
                case "EmailDesc":
                    orders = orders.OrderByDescending(s => s.Email);
                    break;
                case "Email":
                    orders = orders.OrderBy(s => s.Email);
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
            pagedList.BootstrapAnonymousOrders = orders.ToPagedList(pageNumber, pageSize);
            return View("UnproccessedAnonymous",pagedList);
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
         [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> FinishedAnonymous(string sortOrder,string searchString,string currentFilter, int? page)
        {
            var orders =   _order.GetLogsAnonymous();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Date = String.IsNullOrEmpty(sortOrder) ? "date" : "";
            ViewBag.Status = sortOrder == "Status" ? "StatusDesc" : "Status";
            ViewBag.OrderId = sortOrder == "OrderId"? "OrderIdDesc" : "OrderId";
            ViewBag.Email = sortOrder == "Email" ? "EmailDesc" : "Email";
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
                                           || s.Email.Contains(searchString)
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
                case "EmailDesc":
                    orders = orders.OrderByDescending(s => s.Email);
                    break;
                case "Email":
                    orders = orders.OrderBy(s => s.Email);
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
            pagedList.AnonymousOrders = await PaginatedList<AnonymousOrders>.CreateAsync(orders, pageNumber, 5);
            return View("FinishedAnonymous",pagedList);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(int itemId, OrderType type)
        {
            if (type == OrderType.Authorized)
            {
                var order = await _order.GetOrder(itemId);
                var orderView = new OrderViewModel()
                {
                    Id = itemId,
                    Status = order.Status,
                    UserId = order.UserId,
                    OrderId = order.OrderId
                };
                return View("Status", orderView); 
            }
            else
            {
                var order = await _order.GetAnonymousOrder(itemId);
                var orderView = new OrderViewModel()
                {
                    Id = itemId,
                    Status = order.Status,
                    Email = order.Email,
                    OrderId = order.OrderId
                };
                return View("Status", orderView); 
            }
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(OrderViewModel orderDto)
        {
            if (orderDto.Type == OrderType.Authorized)
            {
                var order = new UsersOrders()
                {
                    Id = orderDto.Id,
                    Status = orderDto.Status,
                    UserId = orderDto.UserId
                };
                var user = await _userManager.FindByIdAsync(order.UserId);
                var email = user.Email;
                await _order.ChangeOrderStatus(order);
                await SendEmail.Send(email, "Status change",
                    $"Your order №{order.OrderId} has been changed to status {order.Status}");
                return Redirect("/Order/GetUnproccessed");
            }
            else
            {
                var order = new AnonymousOrders()
                {
                    Id = orderDto.Id,
                    Status = orderDto.Status,
                    Email = orderDto.Email
                };
                var email = order.Email;
                await _order.ChangeAnonymousOrderStatus(order);
                await SendEmail.Send(email, "Status change",
                    $"Your order №{order.OrderId} has been changed to status {order.Status}");
                return Redirect("/Order/GetUnproccessedAnonymous");
            }
        }
        }
   
    }
