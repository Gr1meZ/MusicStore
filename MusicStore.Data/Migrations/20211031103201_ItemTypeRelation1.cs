using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore.Data.Migrations
{
    public partial class ItemTypeRelation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_TypeId",
                table: "Items",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemTypes_TypeId",
                table: "Items",
                column: "TypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemTypes_TypeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TypeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Items");
        }
    }
}
