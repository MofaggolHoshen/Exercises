using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Migrations
{
    public partial class remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscounts_Products_ProducntId",
                table: "ProductDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_ProductDiscounts_ProducntId",
                table: "ProductDiscounts");

            migrationBuilder.DropColumn(
                name: "ProducntId",
                table: "ProductDiscounts");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductDiscounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscounts_ProductId",
                table: "ProductDiscounts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscounts_Products_ProductId",
                table: "ProductDiscounts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscounts_Products_ProductId",
                table: "ProductDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_ProductDiscounts_ProductId",
                table: "ProductDiscounts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductDiscounts");

            migrationBuilder.AddColumn<int>(
                name: "ProducntId",
                table: "ProductDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscounts_ProducntId",
                table: "ProductDiscounts",
                column: "ProducntId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscounts_Products_ProducntId",
                table: "ProductDiscounts",
                column: "ProducntId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
