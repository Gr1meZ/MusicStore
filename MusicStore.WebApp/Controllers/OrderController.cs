using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Business.Interfaces;
using MusicStore.Business.Service_Models;
using MusicStore.Data;
using MusicStore.Data.Models;
using MusicStore.WebApp.Models;
using X.PagedList;

namespace MusicStore.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
        }
    
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrders(int pageNumber=1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _orderService.GetUsersOrders(userId);
            var pagedList = new IndexViewModel();
            pagedList.UsersOrders = await PaginatedList<UsersOrders>.CreateAsync(orders, pageNumber, 5);
            return View("MyOrders",pagedList);

        }
        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOrder = _orderService.GetUsersOrders(userId).FirstOrDefault(i => i.OrderId == id);
            var orderIdKey = _orderService.GetUniqueOrderId(userOrder.OrderId);
            var items = _orderService.GetOrderDetails(orderIdKey);
            return PartialView(new OrderViewModel
            {
                Orders = items
            });
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUnproccessedAnonymous(string sortOrder,string searchString,string currentFilter, int? page)
        {
            
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
            var orders = _orderService.FiltrateAnonymousOrders( sortOrder, searchString, OrderFilterType.Unproccessed);
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
            var orders = _orderService.FiltrateUsersOrders(sortOrder, searchString, OrderFilterType.Unproccessed);
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
            var orders = _orderService.FiltrateUsersOrders(sortOrder, searchString, OrderFilterType.Logs);
            int pageNumber = (page ?? 1);
            var pagedList = new IndexViewModel();
            pagedList.UsersOrders = await PaginatedList<UsersOrders>.CreateAsync(orders, pageNumber, 5);
            return View("Finished",pagedList);
        }
         [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> FinishedAnonymous(string sortOrder,string searchString,string currentFilter, int? page)
        {
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
            var orders = _orderService.FiltrateAnonymousOrders(sortOrder, searchString, OrderFilterType.Logs);
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
                var order = await _orderService.GetFirstOrder(itemId, type);
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
                var order = await _orderService.GetFirstOrder(itemId, type);
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
                await _orderService.ChangeUsersOrderStatus(order, email);
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
                await _orderService.ChangeAnonymousOrderStatus(order, email);
               
                return Redirect("/Order/GetUnproccessedAnonymous");
            }
        }
        }
   
    }
