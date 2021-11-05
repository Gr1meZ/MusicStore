using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore.Data.Migrations
{
    public partial class ItemImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemTypes_TypeId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemTypes_TypeId",
                table: "Items",
                column: "TypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemTypes_TypeId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemTypes_TypeId",
                table: "Items",
                column: "TypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
