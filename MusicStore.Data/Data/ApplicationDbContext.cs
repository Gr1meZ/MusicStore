using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Data.Configuration;
using MusicStore.Data.Models;

namespace MusicStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UsersOrders> UsersOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersOrdersEntityConfiguration).Assembly);
        }
    }
}