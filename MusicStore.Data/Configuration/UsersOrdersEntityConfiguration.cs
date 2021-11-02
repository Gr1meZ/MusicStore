using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Data.Models;

namespace MusicStore.Data.Configuration
{
    public class UsersOrdersEntityConfiguration : IEntityTypeConfiguration<UsersOrders>
    {
        public void Configure(EntityTypeBuilder<UsersOrders> builder)
        {
            builder.HasOne(item => item.Order)
                .WithMany()
                .HasForeignKey(item => item.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(item => item.User)
                .WithMany()
                .HasForeignKey(item => item.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}