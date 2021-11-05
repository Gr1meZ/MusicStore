using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Data.Models;

namespace MusicStore.Data.Configuration
{
    public class ItemEntityConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasOne(item => item.Type)
                .WithMany()
                .HasForeignKey(item => item.TypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(k => k.Id);
            builder.Ignore(p => p.ImageFile);
        }
    }
}