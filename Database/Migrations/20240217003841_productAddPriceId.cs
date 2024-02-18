using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class productAddPriceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripePriceId",
                table: "Products",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentUrl",
                table: "Order",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripePriceId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StripePaymentUrl",
                table: "Order");
        }
    }
}
