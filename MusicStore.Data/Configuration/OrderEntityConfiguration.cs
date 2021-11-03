using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Data.Models;

namespace MusicStore.Data.Configuration
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(item => item.Item)
                .WithMany()
                .HasForeignKey(item => item.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(item => item.Price)
                .WithMany()
                .HasForeignKey(item => item.PriceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}