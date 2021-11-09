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
            builder.HasData(
                new Item[] 
                {
                    new Item { Id=1, Name="Fender Sqiuer Bullet", Price  = 600, TypeId = 2, 
                        ImageName = "img_197352.jpg",
                        Description = "Classic american fender guitar"
                    },
                    new Item {Id=2, Name="Yamaha f-310", Price  = 300, TypeId = 1, 
                        ImageName = "YAMAHA F310 (TBS).jpg",
                        Description = "Classic cheap guitar"
                    },
                    new Item {Id=3, Name="TurboSmooth", Price  = 500, TypeId = 4, 
                        ImageName = "cello.jpg",
                        Description = "This model is suitable for use in broadcast, high-res film close-up, advertising, design visualization, forensic presentation, etc"
                    },
                    new Item {Id=4, Name="Fender Jazz", Price  = 650, TypeId = 5, 
                        ImageName = "fender-american-ultra-jazz-bass-mn-txt.jpg",
                        Description = "Four-string, five-string, active and passive pickups, fretted, fretless... as we know that all of these terms can be confusing for beginning bass players, we have put together the following advice for choosing your bass guitar."
                    },
                    new Item {Id=5, Name="TBT5S22GR SK", Price  = 650, TypeId = 3, 
                        ImageName = "drums.jpg",
                        Description = "The T5 is our entry level TAMBURO galaxy option. But that’s not to say it’s a basic kit, because this set-up still boasts plenty of style and quality."
                    },
                    new Item {Id=6, Name="Roland VAD506", Price  = 1020, TypeId = 3, 
                        ImageName = "roland-vad506.jpg",
                        Description = "Playing drums is not just about playing the drums themselves - the look of the kit also plays an important role, especially in the genres of Rock, Blues, Country or Jazz. With its familiar acoustic look, Roland's V-Drums Acoustic Design marks the center of any stage."
                    },
                    new Item {Id=7, Name="Kohala 3/4", Price  = 540, TypeId = 1, 
                        ImageName = "kohala-3-4-size-steel-string-acoustic-guitar.jpg",
                        Description = "The Kohala KG75S 3/4 guitar is an all new student sized Steel string featuring an Adjustable Truss Rod for maximum playability, Properly Cured Woods and Bracing for increased durability, Special Fret Installation and Pressurized Gluing methods for enhanced performance. Each Kohala guitar comes with a 5mm Padded Gig Bag and a Limited Lifetime Warranty."
                    },
                    new Item {Id=8, Name="Taylor 54-G", Price  = 540, TypeId = 1, 
                        ImageName = "Taylor-K24ce-Koa-fr-v-class-2018.jpg",
                        Description = "From the expanded Builder’s Edition Collection to the compact GT body shape and the American Dream Series, there are plenty of fresh faces at Taylor this year."
                        },
                    new Item {Id=9, Name="ESP LTD BB-600", Price  = 610, TypeId = 2, 
                        ImageName = "original_0.jpg",
                        Description = "Designed with Ben Burnley, founder of Breaking Benjamin, to be one of the most versatile signature models ever made by ESP"
                    },
                    new Item { Id=10,Name="Fender Stratocaster", Price  = 500, TypeId = 2, 
                        ImageName = "esp-vintage-plus-maple-137801.jpeg",
                        Description = "Awesome guitar that was played by John Frusciante"
                    },
                    new Item {Id=11, Name="Cort Action Plus TR", Price  = 500, TypeId = 5, 
                        ImageName = "cort-action-plus-tr.jpg",
                        Description = "Affordable but loaded with quality materials, components and craftsmanship, the Action Series basses define value for the aspiring bass player"
                    },
                    new Item {Id=12, Name="WashBurn", Price  = 800, TypeId = 5, 
                        ImageName = "cort-action-plus-tr.jpg",
                        Description = "With features like Washburn’s reverse headstock, 80’s inspired body shape, and offset position markers, the Sonamaster SB1P is an instant classic aimed at the bass player beginning their musical journey"
                    },
                    new Item { Id=13,Name="WashBurn", Price  = 800, TypeId = 5, 
                        ImageName = "MaestroCelloTop_400x1040_84cb4427-ca2b-4c9f-b40a-5621ad795ebb_800x.jpg",
                        Description = "The tone of these celli is rich, resonant and full, with wonderfully deep bass registers and a strong and vibrant upper register. "
                    },
                    new Item {Id=14, Name="STRINGWORKS MAESTRO", Price  = 500, TypeId = 4, 
                        ImageName = "Strumenti-105-cello-3-PNG.jpg",
                        Description = "Improved once again, with even higher quality woods and varnish! One of the most popular cellos available today for the adult amateur and advanced cello student"
                    },
                    new Item { Id=15,Name="Stagg VNC44", Price  = 700, TypeId = 4, 
                        ImageName = "stagg-vnc44-cello.jpg",
                        Description = "The Stagg VNC-4/4 is a fully carved cello with a solid spruce top and a solid maple body. The neck and bridge are also made of maple, while the fingerboard and pegs are made of stained plywood."
                    }
                });
        }
    }
}