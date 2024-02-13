using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class updatePromoCodeWithProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PromoCode_PromoCodeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PromoCodeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductPromoCode",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    PromoCodesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPromoCode", x => new { x.ProductsId, x.PromoCodesId });
                    table.ForeignKey(
                        name: "FK_ProductPromoCode_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPromoCode_PromoCode_PromoCodesId",
                        column: x => x.PromoCodesId,
                        principalTable: "PromoCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPromoCode_PromoCodesId",
                table: "ProductPromoCode",
                column: "PromoCodesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPromoCode");

            migrationBuilder.AddColumn<int>(
                name: "PromoCodeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PromoCodeId",
                table: "Products",
                column: "PromoCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PromoCode_PromoCodeId",
                table: "Products",
                column: "PromoCodeId",
                principalTable: "PromoCode",
                principalColumn: "Id");
        }
    }
}
