using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore.Data.Migrations
{
    public partial class Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Items_priceId",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "UsersOrders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UsersOrders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "priceId",
                table: "Cart",
                newName: "PriceId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_priceId",
                table: "Cart",
                newName: "IX_Cart_PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Items_PriceId",
                table: "Cart",
                column: "PriceId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Items_PriceId",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UsersOrders",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UsersOrders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PriceId",
                table: "Cart",
                newName: "priceId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_PriceId",
                table: "Cart",
                newName: "IX_Cart_priceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Items_priceId",
                table: "Cart",
                column: "priceId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
