using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore.Data.Migrations
{
    public partial class ItemsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Acoustic guitar" },
                    { 2, "Electroguitar" },
                    { 3, "Drums" },
                    { 4, "Cello" },
                    { 5, "Bass" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "ImageName", "Name", "Price", "TypeId" },
                values: new object[,]
                {
                    { 2, "Classic cheap guitar", "https://dommuzyki.ua/img/cms/YAMAHA%20F310%20(TBS).jpg", "Yamaha f-310", 300m, 1 },
                    { 7, "The Kohala KG75S 3/4 guitar is an all new student sized Steel string featuring an Adjustable Truss Rod for maximum playability, Properly Cured Woods and Bracing for increased durability, Special Fret Installation and Pressurized Gluing methods for enhanced performance. Each Kohala guitar comes with a 5mm Padded Gig Bag and a Limited Lifetime Warranty.", "https://img.kytary.com/eshop_ie/velky_v2/na/637298488625630000/9df4c7ec/64762517/kohala-3-4-size-steel-string-acoustic-guitar.jpg", "Kohala 3/4", 540m, 1 },
                    { 8, "From the expanded Builder’s Edition Collection to the compact GT body shape and the American Dream Series, there are plenty of fresh faces at Taylor this year.", "https://www.taylorguitars.com/sites/default/files/styles/multi_column_guitar_dark/public/responsive-guitar-detail/Taylor-K24ce-Koa-fr-v-class-2018.png?itok=i4UaojWg", "Taylor 54-G", 540m, 1 },
                    { 1, "Classic american fender guitar", "https://www.kombik.com/resources/img/000/001/973/img_197352.jpg", "Fender Sqiuer Bullet", 600m, 2 },
                    { 9, "Designed with Ben Burnley, founder of Breaking Benjamin, to be one of the most versatile signature models ever made by ESP", "https://www.espguitars.ru/netcat_files/707/926/original_0.png", "ESP LTD BB-600", 610m, 2 },
                    { 10, "Awesome guitar that was played by John Frusciante", "https://img.audiofanzine.com/images/u/product/normal/esp-vintage-plus-maple-137801.jpeg", "Fender Stratocaster", 500m, 2 },
                    { 5, "The T5 is our entry level TAMBURO galaxy option. But that’s not to say it’s a basic kit, because this set-up still boasts plenty of style and quality.", "https://www.phasersintegrated.com/wp-content/uploads/2021/07/drums.jpg", "TBT5S22GR SK", 650m, 3 },
                    { 6, "Playing drums is not just about playing the drums themselves - the look of the kit also plays an important role, especially in the genres of Rock, Blues, Country or Jazz. With its familiar acoustic look, Roland's V-Drums Acoustic Design marks the center of any stage.", "https://sc1.musik-produktiv.com/pic-010119430xxl/roland-vad506.jpg", "Roland VAD506", 1020m, 3 },
                    { 3, "This model is suitable for use in broadcast, high-res film close-up, advertising, design visualization, forensic presentation, etc", "https://static.turbosquid.com/Preview/2016/03/04__11_43_48/1_Basic.jpga2882106-ea2f-4c1b-9344-abe7da315485Large.jpg", "TurboSmooth", 500m, 4 },
                    { 14, "Improved once again, with even higher quality woods and varnish! One of the most popular cellos available today for the adult amateur and advanced cello student", "https://cdn.shopify.com/s/files/1/0182/0563/products/MaestroCelloTop_400x1040_84cb4427-ca2b-4c9f-b40a-5621ad795ebb_800x.JPG?v=1569187496", "STRINGWORKS MAESTRO", 500m, 4 },
                    { 15, "The Stagg VNC-4/4 is a fully carved cello with a solid spruce top and a solid maple body. The neck and bridge are also made of maple, while the fingerboard and pegs are made of stained plywood.", "https://static.keymusic.com/products/86616/340/stagg-vnc44-cello.jpg", "Stagg VNC44", 700m, 4 },
                    { 4, "Four-string, five-string, active and passive pickups, fretted, fretless... as we know that all of these terms can be confusing for beginning bass players, we have put together the following advice for choosing your bass guitar.", "https://img.kytary.com/eshop_ie/velky_v2/na/637084818221100000/6244ea3a/64698345/fender-american-ultra-jazz-bass-mn-txt.jpg", "Fender Jazz", 650m, 5 },
                    { 11, "Affordable but loaded with quality materials, components and craftsmanship, the Action Series basses define value for the aspiring bass player", "https://img.kytary.com/eshop_ie/velky_v2/na/637511426172030000/ffbf0ffb/64833141/cort-action-plus-tr.jpg", "Cort Action Plus TR", 500m, 5 },
                    { 12, "With features like Washburn’s reverse headstock, 80’s inspired body shape, and offset position markers, the Sonamaster SB1P is an instant classic aimed at the bass player beginning their musical journey", "https://www.washburn.com/wp-content/uploads/2018/06/SB1PB.jpg", "WashBurn", 800m, 5 },
                    { 13, "The tone of these celli is rich, resonant and full, with wonderfully deep bass registers and a strong and vibrant upper register. ", "https://cdn.shopify.com/s/files/1/0182/0563/products/MaestroCelloTop_400x1040_84cb4427-ca2b-4c9f-b40a-5621ad795ebb_800x.JPG?v=1569187496", "WashBurn", 800m, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ItemTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
