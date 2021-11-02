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
        [Authorize]
        [HttpGet]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrders(int? page)
        {
            var list = _order.GetUsersOrders(5);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var paginatedList = await PaginatedList<Order>.CreateAsync(list, pageNumber, 5);
            return View("MyOrders",paginatedList);
            
        }
   
    }
}