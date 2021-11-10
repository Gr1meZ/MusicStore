using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Data.Models;

namespace MusicStore.Data.Configuration
{
    public class AnonymousOrdersEntityConfiguration : IEntityTypeConfiguration<AnonymousOrders>
    {
        public void Configure(EntityTypeBuilder<AnonymousOrders> builder)
        {
            builder.HasOne(item => item.Order)
                .WithMany()
                .HasForeignKey(item => item.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasKey(k => k.Id);
        }
    }
}