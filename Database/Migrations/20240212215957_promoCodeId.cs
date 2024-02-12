using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class promoCodeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromoCodeId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SingleTimeUsage",
                table: "PromoCode",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UsedPromoCode",
                table: "ProductOrder",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_User_PromoCodeId",
                table: "User",
                column: "PromoCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_PromoCode_PromoCodeId",
                table: "User",
                column: "PromoCodeId",
                principalTable: "PromoCode",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_PromoCode_PromoCodeId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PromoCodeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SingleTimeUsage",
                table: "PromoCode");

            migrationBuilder.DropColumn(
                name: "UsedPromoCode",
                table: "ProductOrder");
        }
    }
}
