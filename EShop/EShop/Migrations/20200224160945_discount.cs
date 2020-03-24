using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Migrations
{
    public partial class discount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStarted = table.Column<DateTime>(nullable: false),
                    DateEnded = table.Column<DateTime>(nullable: false),
                    ValueInPercentage = table.Column<double>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    ProducntId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDiscounts_Products_ProducntId",
                        column: x => x.ProducntId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscounts_ProducntId",
                table: "ProductDiscounts",
                column: "ProducntId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDiscounts");
        }
    }
}
