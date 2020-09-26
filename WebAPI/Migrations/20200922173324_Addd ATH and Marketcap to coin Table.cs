using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoFolioAPI.Migrations
{
    public partial class AdddATHandMarketcaptocoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_change",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "Price_change_pct",
                table: "Coin");

            migrationBuilder.AddColumn<decimal>(
                name: "AllTimeHigh",
                table: "Coin",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MarketCap",
                table: "Coin",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceChange",
                table: "Coin",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceChangePct",
                table: "Coin",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllTimeHigh",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "MarketCap",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "PriceChange",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "PriceChangePct",
                table: "Coin");

            migrationBuilder.AddColumn<decimal>(
                name: "Price_change",
                table: "Coin",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price_change_pct",
                table: "Coin",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
