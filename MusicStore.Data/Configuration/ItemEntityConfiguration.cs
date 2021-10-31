using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Data.Models;

namespace MusicStore.Data.Configuration
{
    public class ItemEntityConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasOne(item => item.type)
                .WithMany()
                .HasForeignKey(item => item.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}