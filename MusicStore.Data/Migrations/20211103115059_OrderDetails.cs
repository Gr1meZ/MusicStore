using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore.Data.Migrations
{
    public partial class OrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Items_Price",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "PriceId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Price",
                table: "Orders",
                newName: "IX_Orders_PriceId");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "UsersOrders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "UsersOrders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Items_PriceId",
                table: "Orders",
                column: "PriceId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Items_PriceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "UsersOrders");

            migrationBuilder.DropColumn(
                name: "status",
                table: "UsersOrders");

            migrationBuilder.RenameColumn(
                name: "PriceId",
                table: "Orders",
                newName: "Price");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PriceId",
                table: "Orders",
                newName: "IX_Orders_Price");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Items_Price",
                table: "Orders",
                column: "Price",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
