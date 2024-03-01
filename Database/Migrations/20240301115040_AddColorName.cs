using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AddColorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCode_Categories_CategoryId",
                table: "PromoCode");

            migrationBuilder.DropIndex(
                name: "IX_PromoCode_CategoryId",
                table: "PromoCode");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PromoCode");

            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "ProductOrder",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "ProductOrder");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PromoCode",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_CategoryId",
                table: "PromoCode",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCode_Categories_CategoryId",
                table: "PromoCode",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
