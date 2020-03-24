using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Migrations
{
    public partial class shopdiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStarted = table.Column<DateTime>(nullable: false),
                    DateEnded = table.Column<DateTime>(nullable: false),
                    ValueInPercentage = table.Column<double>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discount_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discount_ShopId",
                table: "Discount",
                column: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discount");
        }
    }
}
