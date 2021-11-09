using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore.Data.Migrations
{
    public partial class ItemsData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageName",
                value: "img_197352.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageName",
                value: "YAMAHA F310 (TBS).jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageName",
                value: "cello.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageName",
                value: "fender-american-ultra-jazz-bass-mn-txt.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageName",
                value: "drums.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageName",
                value: "roland-vad506.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageName",
                value: "kohala-3-4-size-steel-string-acoustic-guitar.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageName",
                value: "Taylor-K24ce-Koa-fr-v-class-2018.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageName",
                value: "original_0.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageName",
                value: "esp-vintage-plus-maple-137801.jpeg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageName",
                value: "cort-action-plus-tr.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageName",
                value: "MaestroCelloTop_400x1040_84cb4427-ca2b-4c9f-b40a-5621ad795ebb_800x.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageName",
                value: "Strumenti-105-cello-3-PNG.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageName",
                value: "stagg-vnc44-cello.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageName",
                value: "https://www.kombik.com/resources/img/000/001/973/img_197352.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageName",
                value: "https://dommuzyki.ua/img/cms/YAMAHA%20F310%20(TBS).jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageName",
                value: "https://static.turbosquid.com/Preview/2016/03/04__11_43_48/1_Basic.jpga2882106-ea2f-4c1b-9344-abe7da315485Large.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageName",
                value: "https://img.kytary.com/eshop_ie/velky_v2/na/637084818221100000/6244ea3a/64698345/fender-american-ultra-jazz-bass-mn-txt.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageName",
                value: "https://www.phasersintegrated.com/wp-content/uploads/2021/07/drums.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageName",
                value: "https://sc1.musik-produktiv.com/pic-010119430xxl/roland-vad506.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageName",
                value: "https://img.kytary.com/eshop_ie/velky_v2/na/637298488625630000/9df4c7ec/64762517/kohala-3-4-size-steel-string-acoustic-guitar.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageName",
                value: "https://www.taylorguitars.com/sites/default/files/styles/multi_column_guitar_dark/public/responsive-guitar-detail/Taylor-K24ce-Koa-fr-v-class-2018.png?itok=i4UaojWg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageName",
                value: "https://www.espguitars.ru/netcat_files/707/926/original_0.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageName",
                value: "https://img.audiofanzine.com/images/u/product/normal/esp-vintage-plus-maple-137801.jpeg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageName",
                value: "https://www.washburn.com/wp-content/uploads/2018/06/SB1PB.jpg");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageName",
                value: "https://cdn.shopify.com/s/files/1/0182/0563/products/MaestroCelloTop_400x1040_84cb4427-ca2b-4c9f-b40a-5621ad795ebb_800x.JPG?v=1569187496");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageName",
                value: "https://cdn.shopify.com/s/files/1/0182/0563/products/MaestroCelloTop_400x1040_84cb4427-ca2b-4c9f-b40a-5621ad795ebb_800x.JPG?v=1569187496");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageName",
                value: "https://static.keymusic.com/products/86616/340/stagg-vnc44-cello.jpg");
        }
    }
}
