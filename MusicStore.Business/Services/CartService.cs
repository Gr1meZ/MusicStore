using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Business.Interfaces;
using MusicStore.Business.Service_Models;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Models;
using X.PagedList;

namespace MusicStore.Business.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cart;
        private readonly IItemsRepository _item;
        private readonly IOrderRepository _order;
        private readonly IItemTypeRepository _typeRepository;

        public CartService(IItemsRepository item, ICartRepository cart, IOrderRepository order, IItemTypeRepository typeRepository)
        {
            _item = item;
            _cart = cart;
            _order = order;
            _typeRepository = typeRepository;
        }

        public async Task<HomeItemModel> GetItems(string type, string searchString)
        {
            var items =  _item.GetBind();
            var types = await _typeRepository.GetAll().ToListAsync();
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

            return new HomeItemModel()
            {
                Items = items,
                Types = types
            };
        }

        public void AddToAnonymousCart(List<Cart> cart, Item itemDto, int id)
        {
            cart.Add(new Cart
            {
                PriceId = id,
                ItemId = id,
                Item = itemDto,
                Price = itemDto,
                UserId = null
            });
        }

        public async Task AddToCart(int id, string userId)
        {
            var cartDto = new Cart();
            cartDto.PriceId = id;
            cartDto.ItemId = id;
            cartDto.UserId = userId;
            await _cart.AddToCart(cartDto);
        }

        public int CheckItemInCart(List<Cart> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ItemId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public int CheckCartValue(List<Cart> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public IQueryable<Cart> GetUsersCart(string userId)
        {
            return _cart.GetCart(userId);
        }

        public List<int> SetQuantitiesForAnonymous(List<Cart> cart)
        {
            var quantities = new List<int>();
            foreach (var item in cart)
                quantities.Add(1);
            return quantities;
        }

        public List<int> SetQuantitiesForUser(IQueryable<Cart> cart)
        {
            var quantities = new List<int>();
            foreach (var item in cart)
                quantities.Add(1);
            return quantities;
        }

        public async Task SubmitForAnonymous(List<Order> orders, string email)
        {
            var orderId =  await _order.SubmitAnonymousOrder(orders, email);
            await SendEmail.Send(email, "Order",
                $"Your order №{orderId} has been submitted. Wait for status change in few days");
        }

        public List<Order> AddCookieOrder(List<Cart> cart)
        {
            Order order;
            var guid = Guid.NewGuid();
            List<Order> orders = new List<Order>();
            foreach (var item in cart)
            {
                order = new Order();
                order.ItemId = item.ItemId;
                order.PriceId = item.ItemId;
                order.OrderId = guid;
                orders.Add(order); 
            }

            return orders;
        }

        public async Task SubmitOrder(List<Order> orders, string email, string userId)
        {
            await _order.SubmitOrder(orders, userId);
            await _cart.RemoveRange(userId);
            await SendEmail.Send(email, "Order",
                $"Your order №{orders.First().Id.ToString()} has been submitted. Wait for status change in few days");
        }

        public async Task RemoveCartItem(int cartId)
        {
            await _cart.Remove(cartId);
        }
    }
}