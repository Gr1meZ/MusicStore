using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore.Data.Migrations
{
    public partial class ItemsData3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageName",
                value: "cort-action-plus-tr.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageName",
                value: "https://img.kytary.com/eshop_ie/velky_v2/na/637511426172030000/ffbf0ffb/64833141/cort-action-plus-tr.jpg");
        }
    }
}
