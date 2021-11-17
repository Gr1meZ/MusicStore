using Microsoft.Extensions.DependencyInjection;
using MusicStore.Business.Interfaces;
using MusicStore.Business.Services;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Repositories;

namespace MusicStore.WebApp.Services
{
    public static class CustomServicesExtension
    {
        //this method creates transient services of classes and their interfaces  
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IItemTypeRepository, ItemTypeRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IItemTypeService, ItemTypeService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}