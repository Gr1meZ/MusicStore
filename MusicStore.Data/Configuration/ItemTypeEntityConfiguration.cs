using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Data.Models;

namespace MusicStore.Data.Configuration
{
    public class ItemTypeEntityConfiguration : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasData(
                new ItemType[] 
                {    
                    new ItemType { Id=1, Type="Acoustic guitar", },
                    new ItemType { Id=2,Type="Electroguitar"},
                    new ItemType { Id=3,Type="Drums"},
                    new ItemType { Id=4,Type="Cello"},
                    new ItemType { Id=5,Type="Bass"}
                });
        }
    }
}