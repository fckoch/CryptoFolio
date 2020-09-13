using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoFolioAPI.Migrations
{
    public partial class Addednewcoinproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price_change",
                table: "Coin",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price_change_pct",
                table: "Coin",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Coin",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Coin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_change",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "Price_change_pct",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Coin");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Coin");
        }
    }
}
