using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Business.Service_Models;
using MusicStore.Data.Models;

namespace MusicStore.Business.Interfaces
{
    public interface ICartService
    {
        Task<HomeItemModel> GetItems(string type, string searchString);
        void AddToAnonymousCart(List<Cart> cart, Item itemDto, int id);
        Task AddToCart(int id, string userId);
        int CheckItemInCart(List<Cart> cart, int id);
        int CheckCartValue(List<Cart> cart, int id);
        IQueryable<Cart> GetUsersCart(string userId);
        List<int> SetQuantitiesForAnonymous(List<Cart> cart);
        List<int> SetQuantitiesForUser(IQueryable<Cart> cart);
        Task SubmitForAnonymous(List<Order> orders, string email);
        List<Order> AddCookieOrder(List<Cart> cart);
        Task SubmitOrder(List<Order> orders, string email, string userId);
        Task RemoveCartItem(int cartId);

    }
}