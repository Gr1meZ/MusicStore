using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Data.Models;

namespace MusicStore.Data.Configuration
{
    public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(item => item.item)
                .WithMany()
                .HasForeignKey(item => item.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
                
                builder.HasOne(item => item.price)
                .WithMany()
                .HasForeignKey(item => item.priceId)
                .OnDelete(DeleteBehavior.Cascade);;
                
                builder.HasOne(item => item.user)
                    .WithMany()
                    .HasForeignKey(item => item.UserId)
                    .OnDelete(DeleteBehavior.Cascade);;
        }
    }
}